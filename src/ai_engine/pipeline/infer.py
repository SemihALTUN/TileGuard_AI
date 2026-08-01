import os
from ultralytics import YOLO
from pathlib import Path

MODEL_PATH = str(Path(__file__).resolve().parent.parent / "models" / "weights" / "best.onnx")

class TileLocalDetector:
    def __init__(self, model_path: str = None):
        path = model_path or MODEL_PATH
        print(f"[INFO] Yerel ONNX Modeli yükleniyor: {path}")

        if not os.path.exists(path):
            raise FileNotFoundError(f"[ERROR] Model dosyası bulunamadı: {path}")

        self.model = YOLO(path, task="segment")
        self.class_names = self.model.names

    def predict_frame(self, frame_bgr, conf_threshold: float = 0.25):
        try:
            results = self.model.predict(
                source=frame_bgr,
                imgsz=640,
                conf=conf_threshold,
                verbose=False
            )[0]

            return results
        except Exception as e:
            print(f"[ERROR] Yerel Kestirim Hatası: {e}")
            return None