namespace TileGuard.UI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox picCamera;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblInferenceTime;
        private System.Windows.Forms.Label lblDefectCount;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelRight;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            picCamera = new PictureBox();
            btnConnect = new Button();
            btnSelectImage = new Button();
            lblStatus = new Label();
            lblInferenceTime = new Label();
            lblDefectCount = new Label();
            dgvHistory = new DataGridView();
            panelTop = new Panel();
            panelRight = new Panel();
            groupBox1 = new GroupBox();
            txtVisionAnalysis = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)picCamera).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            panelTop.SuspendLayout();
            panelRight.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // picCamera
            // 
            picCamera.BackColor = Color.Black;
            picCamera.Dock = DockStyle.Fill;
            picCamera.Location = new Point(0, 60);
            picCamera.Name = "picCamera";
            picCamera.Size = new Size(1380, 981);
            picCamera.SizeMode = PictureBoxSizeMode.Zoom;
            picCamera.TabIndex = 1;
            picCamera.TabStop = false;
            // 
            // btnConnect
            // 
            btnConnect.BackColor = Color.FromArgb(0, 122, 204);
            btnConnect.FlatStyle = FlatStyle.Flat;
            btnConnect.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnConnect.ForeColor = Color.White;
            btnConnect.Location = new Point(15, 12);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(160, 35);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "AI Servisine Bağlan";
            btnConnect.UseVisualStyleBackColor = false;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnSelectImage
            // 
            btnSelectImage.BackColor = Color.FromArgb(40, 167, 69);
            btnSelectImage.FlatStyle = FlatStyle.Flat;
            btnSelectImage.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSelectImage.ForeColor = Color.White;
            btnSelectImage.Location = new Point(185, 12);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.Size = new Size(160, 35);
            btnSelectImage.TabIndex = 1;
            btnSelectImage.Text = "Görsel Seç ve Test Et";
            btnSelectImage.UseVisualStyleBackColor = false;
            btnSelectImage.Click += btnSelectImage_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatus.ForeColor = Color.Yellow;
            lblStatus.Location = new Point(360, 20);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(193, 19);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Durum: Bağlantı Bekleniyor";
            // 
            // lblInferenceTime
            // 
            lblInferenceTime.AutoSize = true;
            lblInferenceTime.Font = new Font("Segoe UI", 11F);
            lblInferenceTime.ForeColor = Color.White;
            lblInferenceTime.Location = new Point(15, 15);
            lblInferenceTime.Name = "lblInferenceTime";
            lblInferenceTime.Size = new Size(144, 20);
            lblInferenceTime.TabIndex = 0;
            lblInferenceTime.Text = "İşlem Süresi: 0.00 ms";
            // 
            // lblDefectCount
            // 
            lblDefectCount.AutoSize = true;
            lblDefectCount.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDefectCount.ForeColor = Color.Cyan;
            lblDefectCount.Location = new Point(15, 45);
            lblDefectCount.Name = "lblDefectCount";
            lblDefectCount.Size = new Size(162, 20);
            lblDefectCount.TabIndex = 1;
            lblDefectCount.Text = "Tespit Edilen Nesne: 0";
            // 
            // dgvHistory
            // 
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistory.Location = new Point(10, 80);
            dgvHistory.Name = "dgvHistory";
            dgvHistory.Size = new Size(502, 495);
            dgvHistory.TabIndex = 2;
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(45, 45, 48);
            panelTop.Controls.Add(lblStatus);
            panelTop.Controls.Add(btnConnect);
            panelTop.Controls.Add(btnSelectImage);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1904, 60);
            panelTop.TabIndex = 0;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.FromArgb(30, 30, 30);
            panelRight.Controls.Add(groupBox1);
            panelRight.Controls.Add(dgvHistory);
            panelRight.Controls.Add(lblDefectCount);
            panelRight.Controls.Add(lblInferenceTime);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(1380, 60);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(524, 981);
            panelRight.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtVisionAnalysis);
            groupBox1.ForeColor = SystemColors.ButtonFace;
            groupBox1.Location = new Point(13, 583);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(499, 386);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Yapay Zeka (VisionLLM) Kalite Kontrol Raporu";
            // 
            // txtVisionAnalysis
            // 
            txtVisionAnalysis.BackColor = SystemColors.ControlDark;
            txtVisionAnalysis.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 162);
            txtVisionAnalysis.ForeColor = Color.Black;
            txtVisionAnalysis.Location = new Point(6, 22);
            txtVisionAnalysis.Name = "txtVisionAnalysis";
            txtVisionAnalysis.ReadOnly = true;
            txtVisionAnalysis.Size = new Size(487, 358);
            txtVisionAnalysis.TabIndex = 0;
            txtVisionAnalysis.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(picCamera);
            Controls.Add(panelRight);
            Controls.Add(panelTop);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TileGuard AI - Endüstriyel Anomali Tespiti Operatör Paneli";
            ((System.ComponentModel.ISupportInitialize)picCamera).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelRight.ResumeLayout(false);
            panelRight.PerformLayout();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }
        private GroupBox groupBox1;
        private RichTextBox txtVisionAnalysis;
    }
}