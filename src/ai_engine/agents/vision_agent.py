import base64
from io import BytesIO
from PIL import Image
import httpx


class VisionLLMAgent:
    def __init__(self, model_name: str = "llava"):
        self.model_name = model_name
        self.ollama_url = "http://localhost:11434/api/generate"

    def analyze_crop(self, image_crop: Image.Image) -> dict:
        try:
            buffered = BytesIO()
            image_crop.save(buffered, format="JPEG")
            img_str = base64.b64encode(buffered.getvalue()).decode("utf-8")

            prompt = (
                "Analyze this image of a ceramic tile defect. "
                "Output ONLY plain ASCII English text. "
                "Describe the defect in 3 words max. No special characters, no unicode."
            )

            payload = {
                "model": self.model_name,
                "prompt": prompt,
                "images": [img_str],
                "stream": False,
                "options": {
                    "temperature": 0.1,
                    "num_predict": 50
                }
            }

            with httpx.Client(timeout=30.0) as client:
                response = client.post(self.ollama_url, json=payload)
                if response.status_code == 200:
                    res_text = response.json().get("response", "").strip()

                    if not res_text or len(res_text) < 3:
                        res_text = "Fayans yüzeyinde kusur tespit edildi."

                    return {
                        "llm_response": res_text,
                        "is_defect": True,
                        "status": "success"
                    }
                else:
                    return {
                        "llm_response": "VisionLLM yanıt veremedi.",
                        "is_defect": False,
                        "status": "error"
                    }

        except Exception as e:
            return {
                "llm_response": f"Ollama Bağlantı Hatası: {str(e)}",
                "is_defect": False,
                "status": "error"
            }