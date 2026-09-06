import time
import base64
import traceback
import cv2
import numpy as np
from PIL import Image
from fastapi import FastAPI, WebSocket, WebSocketDisconnect
from fastapi.middleware.cors import CORSMiddleware
from pipeline.infer import TileLocalDetector
from agents.vision_agent import VisionLLMAgent
from fastapi import Response, HTTPException
import io
from reportlab.lib.pagesizes import letter
from reportlab.pdfgen import canvas
import psycopg2
import os

app = FastAPI(title="TileGuard AI - Tile Inspection Engine")

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

detector = TileLocalDetector()
vision_agent = VisionLLMAgent(model_name="qwen2.5vl:7b")


@app.get("/api/reports/shift-pdf")
def generate_shift_report_pdf():
    try:
        db_host = os.getenv("DB_HOST", "host.docker.internal")
        conn = psycopg2.connect(
            dbname="tileguard_db",
            user="postgres",
            password="",
            host=db_host,
            port="5432"
        )
        cur = conn.cursor()

        cur.execute("""
            SELECT 
                COUNT(*) as total,
                SUM(CASE WHEN detected_class = 'good' THEN 1 ELSE 0 END) as good_count,
                SUM(CASE WHEN detected_class != 'good' THEN 1 ELSE 0 END) as defect_count,
                ROUND(AVG(inference_time_ms)::numeric, 2) as avg_inf
            FROM inspection_history;
        """)
        stats = cur.fetchone()
        cur.close()
        conn.close()

        total_inspected = stats[0] if stats and stats[0] is not None else 0
        good_count = stats[1] if stats and stats[1] is not None else 0
        defect_count = stats[2] if stats and stats[2] is not None else 0
        avg_inference = f"{stats[3]} ms" if stats and stats[3] is not None else "0 ms"

    except Exception as db_err:
        total_inspected = 0
        good_count = 0
        defect_count = 0
        avg_inference = "Veri Alinamadi"

    buffer = io.BytesIO()
    p = canvas.Canvas(buffer, pagesize=letter)

    p.setFont("Helvetica-Bold", 16)
    p.drawString(50, 750, "TileGuard AI - Vardiya Denetim Raporu")

    p.setFont("Helvetica", 11)
    p.drawString(50, 720, "Sistem: AI Kalite Kontrol ve Kusur Siniflandirma")
    p.drawString(50, 700, "Durum: Cevrimici / Aktif")

    p.line(50, 685, 550, 685)

    p.setFont("Helvetica-Bold", 12)
    p.drawString(50, 650, "Ozet Vardiya Istatistikleri:")

    p.setFont("Helvetica", 11)
    p.drawString(70, 625, f"- Toplam Denetlenen Urun: {total_inspected}")
    p.drawString(70, 605, f"- Saglam (OK) Urun Sayisi: {good_count}")
    p.drawString(70, 585, f"- Kusurlu (NOK) Urun Sayisi: {defect_count}")
    p.drawString(70, 565, f"- Ortalama Cikarim Suresi: {avg_inference}")

    p.showPage()
    p.save()

    buffer.seek(0)
    return Response(
        content=buffer.getvalue(),
        media_type="application/pdf",
        headers={"Content-Disposition": "attachment; filename=vardiya_raporu.pdf"}
    )

@app.get("/")
def read_root():
    return {"status": "online", "system": "TileGuard AI Local Inspection Engine"}

@app.websocket("/ws/inspect")
async def websocket_inspect(websocket: WebSocket):
    await websocket.accept()
    print("[WS] C# WinForms Operatör Paneli Bağlandı.")

    try:
        while True:
            data = await websocket.receive_text()
            start_time = time.time()

            img_bytes = base64.b64decode(data)
            nparr = np.frombuffer(img_bytes, np.uint8)
            frame = cv2.imdecode(nparr, cv2.IMREAD_COLOR)

            if frame is None:
                await websocket.send_json({"error": "Görsel çözümlenemedi."})
                continue

            frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
            pil_img = Image.fromarray(frame_rgb)
            img_w, img_h = pil_img.size

            results = detector.predict_frame(frame)
            enhanced_detections = []

            if results is not None and len(results.boxes) > 0:
                boxes = results.boxes

                for box in boxes:
                    cls_id = int(box.cls[0].item())
                    class_name = detector.class_names.get(cls_id, "defect")
                    conf = float(box.conf[0].item())

                    x1, y1, x2, y2 = box.xyxy[0].tolist()
                    x1, y1 = max(0, int(x1)), max(0, int(y1))
                    x2, y2 = min(img_w, int(x2)), min(img_h, int(y2))

                    status_str = "SAĞLAM" if class_name == "good" else "KUSURLU"

                    vision_text = "Yüzey temiz veya kusur tespit edilmedi."
                    if class_name != "good" and x2 > x1 and y2 > y1:
                        try:
                            cropped_defect = pil_img.crop((x1, y1, x2, y2))

                            llm_res = vision_agent.analyze_crop(cropped_defect, yolo_class_name=class_name)

                            vision_text = llm_res.get("llm_response", f"Yüzeyde {class_name.upper()} tespiti yapıldı.")
                        except Exception as err:
                            print(f"[WARN] VisionLLM Hatası: {err}")
                    elif class_name == "good":
                        vision_text = "Fayans yüzeyi temiz, kusur bulunmuyor."

                    enhanced_detections.append({
                        "class_name": class_name,
                        "status": status_str,
                        "confidence": round(conf, 4),
                        "bbox": [float(x1), float(y1), float(x2), float(y2)],
                        "vision_analysis": vision_text
                    })
            else:
                enhanced_detections.append({
                    "class_name": "good",
                    "status": "SAĞLAM",
                    "confidence": 0.99,
                    "bbox": [0.0, 0.0, float(img_w), float(img_h)],
                    "vision_analysis": "Fayans yüzeyi temiz, kırık veya leke tespit edilmedi."
                })

            inference_time = round((time.time() - start_time) * 1000, 2)

            response_payload = {
                "timestamp": time.strftime("%H:%M:%S"),
                "inference_time_ms": inference_time,
                "total_defects": len([d for d in enhanced_detections if d["class_name"] != "good"]),
                "detections": enhanced_detections
            }

            await websocket.send_json(response_payload)

    except WebSocketDisconnect:
        print("[WS] C# WinForms Bağlantısı Koparıldı.")
    except Exception as e:
        print(f"[ERROR] WebSocket Genel Hata: {e}")
        traceback.print_exc()


if __name__ == "__main__":
    import uvicorn
    uvicorn.run("server:app", host="127.0.0.1", port=8000, reload=True)