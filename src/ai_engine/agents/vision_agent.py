import base64
from io import BytesIO
from PIL import Image
import httpx
import os


class VisionLLMAgent:
    def __init__(self, model_name: str = "qwen2.5vl:7b"):
        self.model_name = model_name
        base_host = os.getenv("OLLAMA_HOST", "http://localhost:11434").rstrip("/")
        self.ollama_url = f"{base_host}/api/generate"

    def analyze_crop(self, image_crop: Image.Image, yolo_class_name: str = "kusur") -> dict:
        try:
            buffered = BytesIO()
            image_crop.save(buffered, format="JPEG")
            img_str = base64.b64encode(buffered.getvalue()).decode("utf-8")

            class_translation = {
                "crack": "çatlak",
                "oil": "yağ lekesi / sıvı kalıntısı",
                "glue_strip": "tutkal izi / şeridi",
                "gray_stroke": "gri leke veya çizgi hatası",
                "rough": "yüzey pürüzlülüğü / bozukluğu",
                "good": "temiz yüzey"
            }

            translated_class = class_translation.get(yolo_class_name, yolo_class_name)

            prompt = (
                f"Görseldeki fayans bölgesinde YOLO modeli '{translated_class}' tespiti yaptı. "
                "Bu kusuru Türkçe olarak en fazla 6-8 kelimeyle, net ve keskin bir şekilde tanımla. "
                "Asla cümle kurma, paragraf yazma veya açıklama uzatma. Sadece teknik tanım yap. Örnek: 'Yoğun yağ lekesi kalıntısı' veya 'Derin kenar çatlağı'."
            )

            payload = {
                "model": self.model_name,
                "prompt": prompt,
                "images": [img_str],
                "stream": False,
                "options": {
                    "temperature": 0.1,
                    "num_predict": 80
                }
            }

            with httpx.Client(timeout=30.0) as client:
                response = client.post(self.ollama_url, json=payload)
                if response.status_code == 200:
                    res_text = response.json().get("response", "").strip()

                    invalid_responses = ["", ".", ",", "!", "?", "-", "...", "evet", "hayır"]
                    if not res_text or res_text.lower() in invalid_responses or len(res_text) < 5:
                        res_text = f"Fayans yüzeyinde {translated_class} tespit edildi."

                    return {
                        "llm_response": res_text,
                        "is_defect": True,
                        "status": "success"
                    }
                else:
                    return {
                        "llm_response": "VisionLLM yerel servisten yanıt alamadı.",
                        "is_defect": False,
                        "status": "error"
                    }

        except Exception as e:
            return {
                "llm_response": f"Ollama Bağlantı Hatası: {str(e)}",
                "is_defect": False,
                "status": "error"
            }