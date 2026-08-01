# TileGuard AI - Endüstriyel Fayans Kalite Kontrol Sistemi

TileGuard AI, endüstriyel fayans üretim hatlarında yüzey kusurlarını (çatlak, leke, kırık, kenar hasarı vb.) gerçek zamanlı olarak tespit eden ve yapay zekâ destekli hibrit mimarisiyle detaylı analiz sunan uçtan uca bir kalite kontrol sistemidir.

Sistem; **YOLOv11 (ONNX Runtime)** ile yüksek hızlı nesne tespiti, **Moondream Vision-Language Model (VLM)** ile kusurların doğal dilde analiz edilmesi, **FastAPI WebSocket** tabanlı asenkron haberleşme ve **C# WinForms** tabanlı operatör panelini tek bir mimaride bir araya getirir.

---

## Proje Durumu

> **Mevcut Durum:** MVP / Erken Geliştirme Aşaması

TileGuard AI şu anda erken geliştirme aşamasındadır. Temel sistem mimarisi, yapay zekâ çıkarım boru hattı ve ana bileşenler çalışır durumdadır. Bununla birlikte kullanıcı arayüzü, performans optimizasyonları ve çeşitli sistem iyileştirmeleri üzerinde geliştirme çalışmaları devam etmektedir.

Bu depo, projenin geliştirme sürecini belgelemek ve ilerleyen sürümlerde yapılacak iyileştirmeleri takip edebilmek amacıyla aktif olarak güncellenmektedir. Yeni özellikler, hata düzeltmeleri ve mimari iyileştirmeler düzenli olarak eklenecektir.
---

#  Özellikler

- Gerçek zamanlı fayans yüzey kusuru tespiti
- Hibrit Yapay Zekâ Boru Hattı (YOLOv11 + Vision-Language Model)
- ONNX Runtime ile yüksek performanslı çıkarım
- FastAPI WebSocket tabanlı asenkron iletişim
- Bounding Box ile otomatik kusur konumlandırma
- Moondream modeli ile doğal dilde kusur analizi
- PostgreSQL üzerinde denetim geçmişi kaydı
- Modern C# WinForms operatör paneli
- Ölçeklenebilir backend mimarisi

---

#  Kullanılan Teknolojiler

| Kategori | Teknolojiler |
|----------|--------------|
| Yapay Zekâ | YOLOv11, ONNX Runtime, Moondream VLM |
| Backend | Python, FastAPI, WebSocket |
| Görüntü İşleme | OpenCV |
| Veritabanı | PostgreSQL |
| Masaüstü Arayüz | C# WinForms (.NET) |
| AI Runtime | Ollama |
| Dağıtım (Planlanan) | Docker, Docker Compose |

---

#  Sistem Mimarisi

```text
          Görüntü Kaynağı
    (Test Görselleri / Kamera)
                 │
                 ▼
      C# WinForms Operatör Paneli
                 │
   Base64 Görüntü Akışı (WebSocket)
                 │
                 ▼
       FastAPI Backend (Python)
                 │
                 ▼
      YOLOv11 (ONNX Runtime)
 Gerçek Zamanlı Kusur Tespiti
                 │
   ROI (Kusurlu Bölge) Kırpma
                 │
                 ▼
Moondream Vision-Language Model
      Ayrıntılı Kusur Analizi
                 │
                 ▼
 PostgreSQL Denetim Geçmişi
                 │
                 ▼
 Sonuçların Operatör Paneline Gönderilmesi
```

---

#  Yapay Zekâ Boru Hattı

## 1. Gerçek Zamanlı Kusur Tespiti

YOLOv11 modeli, ONNX Runtime kullanılarak kamera görüntülerinde yüksek FPS ile çalışır ve fayans yüzeyindeki kusurları tespit ederek konum bilgisi (Bounding Box) ve güven skorunu üretir.

## 2. ROI (Region of Interest) Oluşturma

Tespit edilen yalnızca kusurlu bölgeler kırpılarak Vision-Language Model'e gönderilir. Böylece gereksiz hesaplama yükü azaltılır ve sistemin genel performansı artırılır.

## 3. Vision-Language Analizi

Kırpılan kusur bölgesi, Ollama üzerinden çalışan **Moondream Vision-Language Model** tarafından analiz edilir ve kusurun yapısı doğal dilde ayrıntılı olarak açıklanır.

## 4. Sonuçların Kaydedilmesi

Her denetim sonucunda;

- Kusur türü
- Güven skoru
- Çıkarım süresi
- AI tarafından üretilen analiz
- Tarih bilgisi

PostgreSQL veritabanına otomatik olarak kaydedilir.

---

#  Asenkron Mimari

Vision-Language Model analizleri, FastAPI WebSocket döngüsünü durdurmaması için **asyncio.to_thread()** kullanılarak ayrı iş parçacığında çalıştırılır.

Bu sayede sistem;

- Canlı görüntü akışını kesmeden devam ettirir.
- Operatör panelinde gecikme oluşturmaz.
- Vision Model analizlerini arka planda gerçekleştirir.

---

#  Proje Yapısı

```text
TileGuard_AI/
├── docs/                      # Dokümantasyon ve mimari diyagramlar
├── src/
│   ├── ai_engine/
│   │   ├── agents/            # Moondream Vision Agent
│   │   ├── api/               # FastAPI REST & WebSocket uç noktaları
│   │   ├── models/            # ONNX modelleri
│   │   ├── pipeline/          # YOLO çıkarım boru hattı
│   │   ├── main.py            # FastAPI giriş noktası
│   │   └── requirements.txt
│   │
│   └── operator_panel/
│       └── TileGuard.UI/      # C# WinForms Operatör Arayüzü
│
├── .gitignore
└── README.md
```

---

## Yol Haritası

### Tamamlanan Özellikler

- [x] YOLOv11 ile gerçek zamanlı kusur tespiti
- [x] ONNX Runtime entegrasyonu
- [x] FastAPI backend
- [x] WebSocket tabanlı görüntü aktarımı
- [x] C# WinForms operatör paneli
- [x] PostgreSQL denetim kayıt sistemi
- [x] Moondream Vision-Language Model entegrasyonu
- [x] Asenkron AI Pipeline

### Planlanan Özellikler

#### Yapay Zekâ

- [ ] Daha geniş ve çeşitli veri setleri ile modelin yeniden eğitilmesi (Fine-Tuning)
- [ ] Çıkarım (Inference) performansının optimize edilmesi
- [ ] Kusur tespit doğruluğunun artırılması

#### Operatör Paneli

- [ ] Arayüz tasarımının modernleştirilmesi
- [ ] Kullanıcı giriş ve yetkilendirme sistemi
- [ ] Denetim geçmişi ve log görüntüleme ekranı
- [ ] Gelişmiş filtreleme ve arama özellikleri
- [ ] Gerçek zamanlı sistem durumu ve istatistik ekranı
- [ ] Operatör deneyimini iyileştirecek arayüz geliştirmeleri

#### Genel

- [ ] Docker desteğinin eklenmesi
- [ ] Proje dokümantasyonunun genişletilmesi
- [ ] Daha kapsamlı test senaryolarının hazırlanması

---

#  Kurulum

## Gereksinimler

- Python 3.10+
- .NET 6 veya üzeri
- PostgreSQL 16+
- Ollama

Moondream modelini yükleyin:

```bash
ollama run moondream
```

---

## Backend Kurulumu

```bash
cd src/ai_engine

python -m venv .venv

# Windows
.venv\Scripts\activate

pip install -r requirements.txt

python main.py
```

Sunucu varsayılan olarak aşağıdaki adreste çalışacaktır.

```
http://127.0.0.1:8000
```

## PostgreSQL Hazırlığı

```sql
CREATE TABLE IF NOT EXISTS inspection_history (
    id SERIAL PRIMARY KEY,
    timestamp VARCHAR(50),
    status VARCHAR(20),
    detected_class VARCHAR(50),
    confidence DOUBLE PRECISION,
    inference_time_ms DOUBLE PRECISION,
    vision_analysis TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

---

## Operatör Paneli

Visual Studio ile aşağıdaki çözüm dosyasını açın.

```
src/operator_panel/TileGuard.UI/TileGuard.UI.sln
```

Ardından;

1. **Build Solution** işlemini gerçekleştirin.
2. Uygulamayı çalıştırın.
3. WebSocket adresi olarak aşağıdaki uç noktaya bağlanın.

```
ws://127.0.0.1:8000/ws/inspect
```

---

#  Ekran Görüntüleri

> Operatör paneli ve örnek tespit görüntüleri proje geliştikçe eklenecektir.

---

## Sonuç

**TileGuard AI**, gerçek zamanlı nesne tespiti ile Vision-Language Model teknolojilerini tek bir sistemde birleştirerek endüstriyel fayans üretim hatlarında akıllı kalite kontrolü gerçekleştirmeyi hedefleyen ölçeklenebilir bir yapay zekâ platformudur.