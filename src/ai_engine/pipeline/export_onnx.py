from pathlib import Path
from ultralytics import YOLO

def export_to_onnx():
    print("=" * 60)
    print("[INFO] TileGuard AI Model ONNX Dönüşümü Başlatılıyor...")
    print("=" * 60)

    model_path = Path("../models/weights/best.pt").resolve()
    if not model_path.exists():
        print(f"[ERROR] '{model_path}' bulunamadı! Lütfen önce eğitimi tamamlayın veya ağırlık dosyasını klasöre koyun.")
        return

    print(f"[INFO] Model yükleniyor: {model_path.name}")
    model = YOLO(str(model_path))

    print("[INFO] ONNX formatına dönüştürülüyor (Dynamic Shapes = True)...")
    onnx_file_path = model.export(
        format="onnx",
        dynamic=True,
        simplify=True
    )

    print("\n" + "=" * 60)
    print(f"[SUCCESS] ONNX dönüşümü başarıyla tamamlandı!")
    print(f"[SUCCESS] Oluşturulan ONNX Dosyası: {onnx_file_path}")
    print("=" * 60)

if __name__ == "__main__":
    export_to_onnx()