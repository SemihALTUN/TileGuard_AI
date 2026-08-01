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

app = FastAPI(title="TileGuard AI - Tile Inspection Engine")

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

detector = TileLocalDetector()
vision_agent = VisionLLMAgent(model_name="moondream")


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
                            llm_res = vision_agent.analyze_crop(cropped_defect)
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