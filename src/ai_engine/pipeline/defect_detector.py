import cv2
import numpy as np

def inspect_tile_surface_quality(cropped_tile_bgr, sensitive_threshold=35.0, polygon_mask=None):
    """
    Kırpılmış fayans/kusur bölgesinin yüzey dokusunu Sobel ve Laplacian gradyanları
    ile analiz ederek pürüzlülük, çizik ve anomali skorunu hesaplar.
    """
    if cropped_tile_bgr is None or cropped_tile_bgr.size == 0:
        return False, 0.0, "Görsel geçersiz."

    h, w = cropped_tile_bgr.shape[:2]
    gray = cv2.cvtColor(cropped_tile_bgr, cv2.COLOR_BGR2GRAY)

    if polygon_mask is not None and polygon_mask.shape[:2] == (h, w):
        analysis_mask = polygon_mask.astype(np.uint8)
    else:
        analysis_mask = np.ones((h, w), dtype=np.uint8) * 255

    blurred = cv2.GaussianBlur(gray, (5, 5), 0)

    sobelx = cv2.Sobel(blurred, cv2.CV_64F, 1, 0, ksize=3)
    sobely = cv2.Sobel(blurred, cv2.CV_64F, 0, 1, ksize=3)
    magnitude = cv2.magnitude(sobelx, sobely)

    mean_val = cv2.mean(magnitude, mask=analysis_mask)[0]
    is_defect = mean_val > sensitive_threshold

    return is_defect, round(mean_val, 2), f"Fayans Anomali Skoru: {mean_val:.2f}"