using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;
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
        private ShiftStatsModel _currentShiftStats = new ShiftStatsModel();
        private readonly DatabaseService _dbService = new DatabaseService();
        private readonly UserModel _currentUser;
        private readonly ShiftModel _currentShift;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;
                return handleParam;
            }
        }
        public Form1()
        {
            InitializeComponent();

            _webSocketService = new WebSocketService();
            _databaseService = new DatabaseService();
            _historyList = new List<InspectionHistoryModel>();
            _webSocketService.OnStatusChanged += WebSocketService_OnStatusChanged;
            _webSocketService.OnResultReceived += WebSocketService_OnResultReceived;
            dgvHistory.CellFormatting += dgvHistory_CellFormatting;
            dgvHistory.DataBindingComplete += dgvHistory_DataBindingComplete;
            StyleDataGridView();
            dgvHistory.CellClick += dgvHistory_CellClick;

            this.Shown += Form1_Shown;
        }
        public Form1(UserModel user, ShiftModel shift) : this()
        {
            _currentUser = user;
            _currentShift = shift;

            if (lblAktifOperator != null)
            {
                lblAktifOperator.Text = $"AKTİF OPERATÖR : {_currentUser.FullName}";
            }
        }
        private async void Form1_Shown(object? sender, EventArgs e)
        {
            await LoadHistoryFromDatabaseAsync();
            await LoadShiftStatsFromDatabaseAsync();
        }
        private async Task LoadHistoryFromDatabaseAsync()
        {
            try
            {
                var history = await _databaseService.GetInspectionHistoryAsync();

                _historyList.Clear();

                if (history != null)
                {
                    var top15History = history.Take(15).ToList();
                    _historyList.AddRange(top15History);
                }

                dgvHistory.DataSource = null;
                dgvHistory.DataSource = _historyList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabanı verileri yüklenirken hata oluştu: {ex.Message}", "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task LoadShiftStatsFromDatabaseAsync()
        {
            try
            {
                _currentShiftStats = await _dbService.GetCurrentShiftStatsAsync();
                UpdateStatisticsUI();
            }
            catch (Exception ex)
            {
            }
        }
        private void StyleDataGridView()
        {
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.MultiSelect = false;
            dgvHistory.ReadOnly = true;
            dgvHistory.AllowUserToResizeColumns = false;
            dgvHistory.AllowUserToResizeRows = false;
            dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            Color lightGrey = Color.FromArgb(160, 160, 160);
            Color headerGrey = Color.FromArgb(30, 30, 30);
            dgvHistory.BackgroundColor = lightGrey;
            dgvHistory.DefaultCellStyle.BackColor = lightGrey;
            dgvHistory.DefaultCellStyle.ForeColor = Color.Black;
            dgvHistory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(50, 80, 120);
            dgvHistory.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = headerGrey;
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gainsboro;
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvHistory.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerGrey;
            dgvHistory.GridColor = Color.FromArgb(100, 100, 100);
            dgvHistory.RowTemplate.Height = 26;
        }
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;

            try
            {
                if (_webSocketService.IsConnected)
                {
                    await _webSocketService.DisconnectAsync();
                }
                else
                {
                    await _webSocketService.ConnectAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bağlantı işlemi sırasında hata oluştu: {ex.Message}", "Bağlantı Uyarısı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                btnConnect.Enabled = true;
            }
        }

        private async void btnSelectImage_Click(object sender, EventArgs e)
        {
            if (!_webSocketService.IsConnected)
            {
                MessageBox.Show("Lütfen önce Python AI Servisine bağlanın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Görsel Dosyaları (*.jpg;*.png)|*.jpg;*.png";

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

            string msg = statusMessage.ToLower();

            if (msg.Contains("basarili") || msg.Contains("hazir") || msg.Contains("open") || msg.Contains("bağlandı") || msg.Contains("baglandi"))
            {
                WebSocketLbl.Text = "🟢 WEBSOCKET : BAĞLANDI";
                WebSocketLbl.ForeColor = Color.LightGreen;

                label5.Text = "🟢 SİSTEM : CANLI";
                label5.ForeColor = Color.LightGreen;

                OllamaLbl.Text = "🟢 OLLAMA : HAZIR";
                OllamaLbl.ForeColor = Color.LightGreen;

                btnConnect.Text = "🔌 BAĞLANTIYI KES";
                btnConnect.BackColor = Color.Crimson;
                btnConnect.ForeColor = Color.White;
            }
            else if (msg.Contains("baglaniliyor") || msg.Contains("bekleniyor"))
            {
                WebSocketLbl.Text = "🟡 WEBSOCKET : BAĞLANILIYOR...";
                WebSocketLbl.ForeColor = Color.Yellow;

                label5.Text = "🟡 SİSTEM : HAZIRLANIYOR";
                label5.ForeColor = Color.Yellow;

                OllamaLbl.Text = "🟡 OLLAMA : BEKLENİYOR";
                OllamaLbl.ForeColor = Color.Yellow;

                btnConnect.Text = "⏳ BAĞLANILIYOR...";
                btnConnect.BackColor = Color.DarkOrange;
                btnConnect.ForeColor = Color.White;
            }
            else
            {
                WebSocketLbl.Text = "🔴 WEBSOCKET : BAĞLANTI KOPUK";
                WebSocketLbl.ForeColor = Color.IndianRed;

                label5.Text = "🟢 SİSTEM : ÇEVRİMDIŞI";
                label5.ForeColor = Color.IndianRed;

                OllamaLbl.Text = "🔴 OLLAMA : ÇEVRİMDIŞI";
                OllamaLbl.ForeColor = Color.IndianRed;

                btnConnect.Text = "🔌 AI SERVİSİNE BAĞLAN";
                btnConnect.BackColor = Color.FromArgb(0, 122, 204);
                btnConnect.ForeColor = Color.White;
            }
        }

        private async void WebSocketService_OnResultReceived(InspectionResultDto result)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => WebSocketService_OnResultReceived(result)));
                return;
            }

            lblInferenceTime.Text = $"İşlem Süresi: {result.InferenceTimeMs} ms";
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

            txtVisionAnalysis.Text = visionReportBuilder.ToString();

            var defectItems = result.Detections?.Where(d => d.ClassName != "good").ToList() ?? new List<DetectionItemDto>();
            bool isDefective = defectItems.Count > 0;

            _currentShiftStats.TotalInspected++;
            if (isDefective)
                _currentShiftStats.NokCount++;
            else
                _currentShiftStats.OkCount++;

            UpdateStatisticsUI();

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
                        }
                    }
                }

                if (defectItems.Count > 0)
                {
                    List<Bitmap> croppedList = new List<Bitmap>();

                    foreach (var defect in defectItems)
                    {
                        if (defect.BoundingBox != null && defect.BoundingBox.Count == 4)
                        {
                            int cropX = (int)defect.BoundingBox[0];
                            int cropY = (int)defect.BoundingBox[1];
                            int cropW = (int)(defect.BoundingBox[2] - cropX);
                            int cropH = (int)(defect.BoundingBox[3] - cropY);

                            int margin = 15;
                            cropX = Math.Max(0, cropX - margin);
                            cropY = Math.Max(0, cropY - margin);
                            cropW = Math.Min(_currentImage.Width - cropX, cropW + (margin * 2));
                            cropH = Math.Min(_currentImage.Height - cropY, cropH + (margin * 2));

                            Bitmap? croppedBmp = CropImageRegion(_currentImage, new Rectangle(cropX, cropY, cropW, cropH));
                            if (croppedBmp != null)
                            {
                                croppedList.Add(croppedBmp);
                            }
                        }
                    }

                    Bitmap? combinedImage = CombineCroppedDefects(croppedList);

                    if (combinedImage != null)
                    {
                        CroppedPictureBox.Image?.Dispose();
                        CroppedPictureBox.Image = combinedImage;
                    }

                    foreach (var item in croppedList)
                    {
                        item.Dispose();
                    }
                }
                else
                {
                    CroppedPictureBox.Image?.Dispose();
                    CroppedPictureBox.Image = null;
                }

                picCamera.Image?.Dispose();
                picCamera.Image = annotatedBitmap;
            }
            string mainClass = isDefective ? string.Join(", ", defectItems.Select(d => d.ClassName).Distinct()) : "good";
            double mainConfidence = result.Detections.FirstOrDefault()?.Confidence ?? 0.0;
            string mainVisionAnalysis = result.Detections.FirstOrDefault()?.VisionAnalysis ?? string.Empty;

            var historyModel = new InspectionHistoryModel
            {
                Timestamp = result.Timestamp,
                Status = isDefective ? "KUSURLU" : "SAĞLAM",
                DetectedClass = mainClass,
                Confidence = Math.Round(mainConfidence * 100, 2),
                InferenceTimeMs = result.InferenceTimeMs,
                VisionAnalysis = mainVisionAnalysis
            };

            try
            {
                await _databaseService.SaveInspectionHistoryAsync(historyModel);
            }
            catch
            {
            }

            _historyList.Add(historyModel);

            var displayList = _historyList
                .OrderByDescending(x => x.Id)
                .Take(15)
                .ToList();

            dgvHistory.DataSource = null;
            dgvHistory.DataSource = displayList;
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
                                             $"Doğruluk: %{selectedItem.Confidence}\n" +
                                             $"{new string('-', 35)}\n" +
                                             $"Rapor: {selectedItem.VisionAnalysis}";
                }
            }
        }

        private void UpdateStatisticsUI()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateStatisticsUI));
                return;
            }

            label1.Text = $"TOPLAM İNCELENEN = {_currentShiftStats.TotalInspected:N0}";
            label2.Text = $"SAĞLAM (OK) = {_currentShiftStats.OkCount:N0}";
            label3.Text = $"HATALI (NOK) = {_currentShiftStats.NokCount:N0}";
            label4.Text = $"HATA ORANI = %{_currentShiftStats.DefectRate:F1}";
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private Bitmap? CropImageRegion(Image sourceImage, Rectangle cropArea)
        {
            try
            {
                int x = Math.Max(0, cropArea.X);
                int y = Math.Max(0, cropArea.Y);
                int width = Math.Min(sourceImage.Width - x, cropArea.Width);
                int height = Math.Min(sourceImage.Height - y, cropArea.Height);

                if (width <= 0 || height <= 0) return null;

                Rectangle validCropArea = new Rectangle(x, y, width, height);
                Bitmap bmpImage = new Bitmap(sourceImage);
                return bmpImage.Clone(validCropArea, bmpImage.PixelFormat);
            }
            catch
            {
                return null;
            }
        }
        private Bitmap CombineCroppedDefects(List<Bitmap> croppedList)
        {
            if (croppedList == null || croppedList.Count == 0) return null;
            if (croppedList.Count == 1)
            {
                return new Bitmap(croppedList[0]);
            }

            int targetHeight = 250;
            int totalWidth = 0;

            List<Bitmap> resizedList = new List<Bitmap>();
            foreach (var bmp in croppedList)
            {
                double scale = (double)targetHeight / bmp.Height;
                int newWidth = (int)(bmp.Width * scale);

                Bitmap resized = new Bitmap(bmp, newWidth, targetHeight);
                resizedList.Add(resized);

                totalWidth += newWidth + 6;
            }
            Bitmap combinedBmp = new Bitmap(totalWidth, targetHeight);
            using (Graphics g = Graphics.FromImage(combinedBmp))
            {
                g.Clear(Color.FromArgb(30, 30, 30));

                int currentX = 0;
                for (int i = 0; i < resizedList.Count; i++)
                {
                    var resizedBmp = resizedList[i];

                    g.DrawImage(resizedBmp, currentX, 0);

                    if (i < resizedList.Count - 1)
                    {
                        using (Pen pen = new Pen(Color.Red, 3))
                        {
                            int lineX = currentX + resizedBmp.Width + 3;
                            g.DrawLine(pen, lineX, 0, lineX, targetHeight);
                        }
                    }

                    currentX += resizedBmp.Width + 6;
                    resizedBmp.Dispose();
                }
            }

            return combinedBmp;
        }

        private void veritabanıKaydıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistoryForm historyForm = new HistoryForm();
            historyForm.StartPosition = FormStartPosition.CenterParent;
            historyForm.ShowDialog(this);
        }
        private void dgvHistory_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvHistory.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString() ?? "";
                if (status.Contains("SAĞLAM"))
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                    e.CellStyle.Font = new Font(dgvHistory.Font, FontStyle.Bold);
                }
                else if (status.Contains("KUSURLU"))
                {
                    e.CellStyle.ForeColor = Color.DarkRed;
                    e.CellStyle.Font = new Font(dgvHistory.Font, FontStyle.Bold);
                }
            }

            if (dgvHistory.Columns[e.ColumnIndex].Name == "Confidence" && e.Value != null)
            {
                e.CellStyle.ForeColor = Color.DarkBlue;
                e.CellStyle.Font = new Font(dgvHistory.Font, FontStyle.Bold);
            }
        }
        private void dgvHistory_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvHistory.ClearSelection();
        }

        private void çıkışYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Oturumu kapatıp giriş ekranına dönmek istediğinize emin misiniz?", "Oturumu Kapat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Retry;
                this.Close();
            }
        }

        private void hataListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemLogsForm systemlogsForm = new SystemLogsForm();
            systemlogsForm.StartPosition = FormStartPosition.CenterParent;
            systemlogsForm.ShowDialog(this);
        }

        private void kullanıcıEklemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentUser == null || _currentUser.Role?.Trim().ToLower() != "admin")
            {
                MessageBox.Show("Bu alanı yalnızca sistem yöneticileri (Admin) kullanabilir!", "Yetkisiz Erişim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AddUserForm addUserForm = new AddUserForm();
            addUserForm.StartPosition = FormStartPosition.CenterParent;
            addUserForm.ShowDialog(this);
        }

        private void kullanıcıBilgileriGüncellemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentUser == null || _currentUser.Role?.Trim().ToLower() != "admin")
            {
                MessageBox.Show("Bu alanı yalnızca sistem yöneticileri (Admin) kullanabilir!", "Yetkisiz Erişim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UserManagementForm managementForm = new UserManagementForm();
            managementForm.StartPosition = FormStartPosition.CenterParent;
            managementForm.ShowDialog(this);
        }
    }
}