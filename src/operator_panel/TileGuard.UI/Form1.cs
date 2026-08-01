using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TileGuard.UI.DTOs;
using TileGuard.UI.Models;
using TileGuard.UI.Services;

namespace TileGuard.UI
{
    public partial class Form1 : Form
    {
        private readonly WebSocketService _webSocketService;
        private readonly DatabaseService _databaseService;
        private readonly List<InspectionHistoryModel> _historyList;
        private Image? _currentImage;

        public Form1()
        {
            InitializeComponent();

            _webSocketService = new WebSocketService();
            _databaseService = new DatabaseService();
            _historyList = new List<InspectionHistoryModel>();

            _webSocketService.OnStatusChanged += WebSocketService_OnStatusChanged;
            _webSocketService.OnResultReceived += WebSocketService_OnResultReceived;

            StyleDataGridView();
            dgvHistory.CellClick += dgvHistory_CellClick;

            this.Load += Form1_Load;
        }
        private async void Form1_Load(object? sender, EventArgs e)
        {
            await LoadHistoryFromDatabaseAsync();
        }
        private async Task LoadHistoryFromDatabaseAsync()
        {
            try
            {
                var history = await _databaseService.GetInspectionHistoryAsync();
                _historyList.Clear();
                _historyList.AddRange(history);

                dgvHistory.DataSource = null;
                dgvHistory.DataSource = _historyList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabaný verileri yüklenirken hata oluþtu: {ex.Message}", "Veritabaný Hatasý", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void StyleDataGridView()
        {
            dgvHistory.RowHeadersVisible = false; // Sol baþtaki boþ oku/sütunu kaldýrýr
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Sütunlarý tam sýðdýrýr
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Tüm satýrý seçer
            dgvHistory.MultiSelect = false;
            dgvHistory.ReadOnly = true;

            dgvHistory.BackgroundColor = Color.FromArgb(30, 30, 30);
            dgvHistory.DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            dgvHistory.DefaultCellStyle.ForeColor = Color.White;
            dgvHistory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 122, 204);
            dgvHistory.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(28, 28, 28);
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.Cyan;
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvHistory.GridColor = Color.FromArgb(60, 60, 60);
        }
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (!_webSocketService.IsConnected)
            {
                await _webSocketService.ConnectAsync();
            }
            else
            {
                await _webSocketService.DisconnectAsync();
                lblStatus.Text = "Durum: Baðlantý Kapatýldý";
                lblStatus.ForeColor = Color.Orange;
            }
        }

        private async void btnSelectImage_Click(object sender, EventArgs e)
        {
            if (!_webSocketService.IsConnected)
            {
                MessageBox.Show("Lütfen önce Python AI Servisine baðlanýn!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Görsel Dosyalarý (*.jpg;*.png)|*.jpg;*.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _currentImage = Image.FromFile(ofd.FileName);
                picCamera.Image = new Bitmap(_currentImage);

                await _webSocketService.SendImageAsync(_currentImage);
            }
        }

        private void WebSocketService_OnStatusChanged(string statusMessage)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => WebSocketService_OnStatusChanged(statusMessage)));
                return;
            }

            lblStatus.Text = $"Durum: {statusMessage}";
            if (statusMessage.Contains("Baþarýlý"))
            {
                lblStatus.ForeColor = Color.LightGreen;
                btnConnect.Text = "Baðlantýyý Kes";
                btnConnect.BackColor = Color.Crimson;
            }
            else
            {
                lblStatus.ForeColor = Color.Yellow;
                btnConnect.Text = "AI Servisine Baðlan";
                btnConnect.BackColor = Color.FromArgb(0, 122, 204);
            }
        }

        private async void WebSocketService_OnResultReceived(InspectionResultDto result)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => WebSocketService_OnResultReceived(result)));
                return;
            }

            lblInferenceTime.Text = $"Ýþlem Süresi: {result.InferenceTimeMs} ms";
            lblDefectCount.Text = $"Tespit Edilen Nesne: {result.TotalDefects}";

            StringBuilder visionReportBuilder = new StringBuilder();

            if (result.Detections != null && result.Detections.Count > 0)
            {
                foreach (var det in result.Detections)
                {
                    if (!string.IsNullOrEmpty(det.VisionAnalysis))
                    {
                        visionReportBuilder.AppendLine($"[{det.ClassName.ToUpper()}]: {det.VisionAnalysis}");
                        visionReportBuilder.AppendLine(new string('-', 35));
                    }
                }
            }

            if (_currentImage != null)
            {
                Bitmap annotatedBitmap = new Bitmap(_currentImage);
                using (Graphics g = Graphics.FromImage(annotatedBitmap))
                {
                    foreach (var det in result.Detections)
                    {
                        if (det.BoundingBox != null && det.BoundingBox.Count == 4)
                        {
                            float x1 = det.BoundingBox[0];
                            float y1 = det.BoundingBox[1];
                            float x2 = det.BoundingBox[2];
                            float y2 = det.BoundingBox[3];
                            float width = x2 - x1;
                            float height = y2 - y1;

                            Color penColor = det.ClassName == "good" ? Color.LimeGreen : Color.Red;
                            using (Pen pen = new Pen(penColor, 3))
                            {
                                g.DrawRectangle(pen, x1, y1, width, height);
                            }

                            string labelStr = $"{det.ClassName} (%{det.Confidence * 100:F1})";
                            using (Font font = new Font("Segoe UI", 11, FontStyle.Bold))
                            using (SolidBrush penBrush = new SolidBrush(penColor))
                            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(180, 0, 0, 0)))
                            {
                                SizeF textSize = g.MeasureString(labelStr, font);
                                g.FillRectangle(bgBrush, x1, y1 - textSize.Height - 4, textSize.Width + 6, textSize.Height + 4);
                                g.DrawString(labelStr, font, Brushes.White, x1 + 3, y1 - textSize.Height - 2);
                            }

                            var historyModel = new InspectionHistoryModel
                            {
                                Timestamp = result.Timestamp,
                                Status = det.ClassName == "good" ? "SAÐLAM" : "KUSURLU",
                                DetectedClass = det.ClassName,
                                Confidence = Math.Round(det.Confidence * 100, 2),
                                InferenceTimeMs = result.InferenceTimeMs,
                                VisionAnalysis = det.VisionAnalysis
                            };

                            try
                            {
                                await _databaseService.SaveInspectionHistoryAsync(historyModel);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Veritabanýna kaydederken hata oluþtu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            _historyList.Add(historyModel);
                        }
                    }
                }

                picCamera.Image = annotatedBitmap;

                if (visionReportBuilder.Length > 0)
                {
                    txtVisionAnalysis.Text = visionReportBuilder.ToString();
                }
                else
                {
                    txtVisionAnalysis.Text = "Yüzey temiz veya VisionLLM analizi üretilemedi.";
                }

                dgvHistory.DataSource = null;
                dgvHistory.DataSource = _historyList;
                if (dgvHistory.Rows.Count > 0)
                {
                    dgvHistory.FirstDisplayedScrollingRowIndex = dgvHistory.Rows.Count - 1;
                }
            }
        }
        private void dgvHistory_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < _historyList.Count)
            {
                var selectedItem = _historyList[e.RowIndex];
                if (selectedItem != null && !string.IsNullOrEmpty(selectedItem.VisionAnalysis))
                {
                    txtVisionAnalysis.Text = $"Seçilen Tespit Analizi:\n" +
                                             $"Tür: {selectedItem.DetectedClass.ToUpper()}\n" +
                                             $"Durum: {selectedItem.Status}\n" +
                                             $"Doðruluk: %{selectedItem.Confidence}\n" +
                                             $"{new string('-', 35)}\n" +
                                             $"Rapor: {selectedItem.VisionAnalysis}";
                }
            }
        }
    }
}