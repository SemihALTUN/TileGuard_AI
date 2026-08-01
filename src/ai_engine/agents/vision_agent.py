import base64
from io import BytesIO
from PIL import Image
import httpx

class VisionLLMAgent:
    def __init__(self, model_name: str = "moondream"):
        self.model_name = model_name
        self.ollama_url = "http://localhost:11434/api/generate"

    def analyze_crop(self, image_crop: Image.Image) -> dict:
        """
        Kırpılmış kusurlu fayans bölgesini inceler ve Türkçe teknik rapor üretir.
        """
        try:
            buffered = BytesIO()
            image_crop.save(buffered, format="JPEG")
            img_str = base64.b64encode(buffered.getvalue()).decode("utf-8")

            prompt = (
                "You are an industrial quality control specialist inspecting ceramic/porcelain tiles. "
                "Describe the defect or anomaly in this image (e.g. oil stain, crack, scratch, glue strip). "
                "ALWAYS answer ONLY in Turkish language using 1-2 short technical sentences. "
                "Example: 'Fayans yüzeyinde yağ lekesi tespit edildi, temizlenmesi gerekiyor.'"
            )

            payload = {
                "model": self.model_name,
                "prompt": prompt,
                "images": [img_str],
                "stream": False
            }

            with httpx.Client(timeout=30.0) as client:
                response = client.post(self.ollama_url, json=payload)
                if response.status_code == 200:
                    res_text = response.json().get("response", "").strip()

                    is_defect = "STATUS: DEFECT" in res_text.upper() or "DEFECT" in res_text.upper()

                    return {
                        "llm_response": res_text,
                        "is_defect": is_defect,
                        "status": "success"
                    }
                else:
                    return {
                        "llm_response": "VisionLLM yanıt vermedi.",
                        "is_defect": False,
                        "status": "error"
                    }

        except Exception as e:
            return {
                "llm_response": f"Ollama Bağlantı Hatası: {str(e)}",
                "is_defect": False,
                "status": "error"
            }