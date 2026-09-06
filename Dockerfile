# Resmi Python imajını temel al
FROM python:3.10-slim

# Çalışma dizinini ayarla
WORKDIR /app

# Sistem bağımlılıkları (Gerekirse OpenCV veya diğer kütüphaneler için)
RUN apt-get update && apt-get install -y \
    build-essential \
    libgl1 \
    libglib2.0-0 \
    && rm -rf /var/lib/apt/lists/*

# Önce bağımlılık dosyasını kopyala ve yükle (Cache optimizasyonu için)
COPY src/ai_engine/requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

# Projenin kalan kaynak kodlarını imaja kopyala
COPY src/ /app/src/

# Çalışma dizinini ai_engine içine odakla
WORKDIR /app/src/ai_engine

# FastAPI veya ana uygulama portunu aç (Örn: 8000)
EXPOSE 8000

# Uygulamayı başlat
CMD ["python", "main.py"]