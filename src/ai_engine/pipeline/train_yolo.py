import shutil
import torch
from pathlib import Path
from ultralytics import YOLO


def train_TileGuard_model():
    print("=" * 60)
    print("[INFO] TileGuard AI Model Eğitimi Başlatılıyor...")
    print("=" * 60)

    device = "0" if torch.cuda.is_available() else "cpu"
    print(f"[INFO] Kullanılacak Donanım: {'NVIDIA GPU (CUDA)' if device == '0' else 'CPU'}")

    BASE_DIR = Path(__file__).resolve().parent.parent
    yaml_path = (BASE_DIR / "data" / "TILE -Instance Segmentation-.v1i.yolov11" / "data.yaml").resolve()
    if not yaml_path.exists():
        print(f"[ERROR] '{yaml_path}' bulunamadı! Lütfen veri seti yolunu ve data.yaml dosyasını kontrol edin.")
        return

    print("[INFO] YOLOv11s Segmentation taban modeli yükleniyor...")
    model = YOLO("yolo11s-seg.pt")

    output_dir = (BASE_DIR / "models").resolve()
    output_dir.mkdir(parents=True, exist_ok=True)

    print("[INFO] Eğitim parametreleri ayarlandı. Eğitim başlıyor...")
    results = model.train(
        data=str(yaml_path),
        epochs=100,
        imgsz=640,
        batch=16,
        device=device,
        project=str(output_dir),
        name="TileGuard_yolo11_seg",
        exist_ok=True,
        save=True,
        save_period=5,
        workers=2,
        verbose=True
    )

    best_model_path = output_dir / "TileGuard_yolo11_seg" / "weights" / "best.pt"

    target_weights_dir = output_dir / "weights"
    target_weights_dir.mkdir(parents=True, exist_ok=True)
    target_model_path = target_weights_dir / "best.pt"

    if best_model_path.exists():
        shutil.copy(best_model_path, target_model_path)
        print("\n" + "=" * 60)
        print(f"[SUCCESS] Eğitim başarıyla tamamlandı!")
        print(f"[SUCCESS] Kullanıma hazır en iyi model kopyalandı: {target_model_path}")
        print("=" * 60)
    else:
        print("[WARNING] Eğitim bitti ancak best.pt dosyasına ulaşılamadı.")


if __name__ == "__main__":
    train_TileGuard_model()