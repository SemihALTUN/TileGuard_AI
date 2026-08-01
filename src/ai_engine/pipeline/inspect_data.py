from pathlib import Path
from PIL import Image

def analyze_yolo_dataset(data_dir):
    base_path = Path(data_dir).resolve()
    print("=" * 60)
    print(f"[INFO] YOLO Veri Seti Analizi Başlatıldı: {base_path.name}")
    print("=" * 60)

    if not base_path.exists():
        print(f"[ERROR] '{data_dir}' dizini bulunamadı! Klasör yolunu kontrol edin.")
        return

    splits = ['train', 'valid', 'val', 'test']
    for split in splits:
        split_path = base_path / split
        if not split_path.exists():
            continue

        images_path = split_path / 'images'
        labels_path = split_path / 'labels'

        image_files = []
        if images_path.exists():
            for ext in ('*.png', '*.jpg', '*.jpeg'):
                image_files.extend(list(images_path.glob(ext)))

        label_files = list(labels_path.glob('*.txt')) if labels_path.exists() else []

        img_count = len(image_files)
        lbl_count = len(label_files)

        sample_size = "N/A"
        if img_count > 0:
            with Image.open(image_files[0]) as img:
                sample_size = f"{img.size[0]}x{img.size[1]} ({img.mode})"

        print(f"\n[{split.upper()}] Kümesi:")
        print(f"  |-- Görsel Sayısı : {img_count:>4} adet | Örnek Çözünürlük: {sample_size}")
        print(f"  |-- Etiket Sayısı : {lbl_count:>4} adet (.txt)")

    print("\n" + "=" * 60)
    print("[SUCCESS] YOLO Veri seti analizi başarıyla tamamlandı.")
    print("=" * 60)

if __name__ == "__main__":
    DATASET_PATH = "../data/TILE -Instance Segmentation-.v1i.yolov11"
    analyze_yolo_dataset(DATASET_PATH)