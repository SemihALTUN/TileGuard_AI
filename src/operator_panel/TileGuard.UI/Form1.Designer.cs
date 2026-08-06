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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            picCamera = new PictureBox();
            btnConnect = new Button();
            btnSelectImage = new Button();
            lblInferenceTime = new Label();
            lblDefectCount = new Label();
            dgvHistory = new DataGridView();
            panelTop = new Panel();
            tableLayoutPanel3 = new TableLayoutPanel();
            pictureBox1 = new PictureBox();
            panel11 = new Panel();
            lblAktifOperator = new Label();
            panel10 = new Panel();
            label5 = new Label();
            panelRight = new Panel();
            groupBox3 = new GroupBox();
            groupBox2 = new GroupBox();
            CroppedPictureBox = new PictureBox();
            groupBox1 = new GroupBox();
            txtVisionAnalysis = new RichTextBox();
            menuStrip1 = new MenuStrip();
            anaSayfaToolStripMenuItem = new ToolStripMenuItem();
            loglarToolStripMenuItem = new ToolStripMenuItem();
            veritabanıKaydıToolStripMenuItem = new ToolStripMenuItem();
            hataListesiToolStripMenuItem = new ToolStripMenuItem();
            ayarlarToolStripMenuItem = new ToolStripMenuItem();
            kullanıcıEklemeToolStripMenuItem = new ToolStripMenuItem();
            kullanıcıBilgileriGüncellemeToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            çıkışYapToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel5 = new Panel();
            label4 = new Label();
            panel4 = new Panel();
            label3 = new Label();
            panel3 = new Panel();
            label2 = new Label();
            panel2 = new Panel();
            label1 = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            panel6 = new Panel();
            button2 = new Button();
            panel7 = new Panel();
            button1 = new Button();
            panel8 = new Panel();
            OllamaLbl = new Label();
            panel9 = new Panel();
            WebSocketLbl = new Label();
            groupBox4 = new GroupBox();
            panel12 = new Panel();
            ((System.ComponentModel.ISupportInitialize)picCamera).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            panelTop.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel11.SuspendLayout();
            panel10.SuspendLayout();
            panelRight.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CroppedPictureBox).BeginInit();
            groupBox1.SuspendLayout();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            panel9.SuspendLayout();
            groupBox4.SuspendLayout();
            panel12.SuspendLayout();
            SuspendLayout();
            // 
            // picCamera
            // 
            picCamera.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            picCamera.BackColor = SystemColors.AppWorkspace;
            picCamera.Location = new Point(3, 19);
            picCamera.Name = "picCamera";
            picCamera.Size = new Size(910, 816);
            picCamera.SizeMode = PictureBoxSizeMode.Zoom;
            picCamera.TabIndex = 1;
            picCamera.TabStop = false;
            // 
            // btnConnect
            // 
            btnConnect.BackColor = Color.FromArgb(0, 122, 204);
            btnConnect.Dock = DockStyle.Fill;
            btnConnect.FlatStyle = FlatStyle.Flat;
            btnConnect.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnConnect.ForeColor = Color.White;
            btnConnect.Location = new Point(1487, 3);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(241, 36);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "AI Servisine Bağlan";
            btnConnect.UseVisualStyleBackColor = false;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnSelectImage
            // 
            btnSelectImage.BackColor = Color.FromArgb(40, 167, 69);
            btnSelectImage.Dock = DockStyle.Fill;
            btnSelectImage.FlatStyle = FlatStyle.Flat;
            btnSelectImage.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSelectImage.ForeColor = Color.White;
            btnSelectImage.Location = new Point(1734, 3);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.Size = new Size(243, 36);
            btnSelectImage.TabIndex = 1;
            btnSelectImage.Text = "Görsel Seç ve Test Et";
            btnSelectImage.UseVisualStyleBackColor = false;
            btnSelectImage.Click += btnSelectImage_Click;
            // 
            // lblInferenceTime
            // 
            lblInferenceTime.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblInferenceTime.AutoSize = true;
            lblInferenceTime.Font = new Font("Segoe UI", 11F);
            lblInferenceTime.ForeColor = SystemColors.AppWorkspace;
            lblInferenceTime.Location = new Point(15, 15);
            lblInferenceTime.Name = "lblInferenceTime";
            lblInferenceTime.Size = new Size(144, 20);
            lblInferenceTime.TabIndex = 0;
            lblInferenceTime.Text = "İşlem Süresi: 0.00 ms";
            // 
            // lblDefectCount
            // 
            lblDefectCount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblDefectCount.AutoSize = true;
            lblDefectCount.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDefectCount.ForeColor = SystemColors.AppWorkspace;
            lblDefectCount.Location = new Point(15, 45);
            lblDefectCount.Name = "lblDefectCount";
            lblDefectCount.Size = new Size(162, 20);
            lblDefectCount.TabIndex = 1;
            lblDefectCount.Text = "Tespit Edilen Nesne: 0";
            // 
            // dgvHistory
            // 
            dgvHistory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistory.Location = new Point(3, 19);
            dgvHistory.Name = "dgvHistory";
            dgvHistory.Size = new Size(1040, 309);
            dgvHistory.TabIndex = 2;
            // 
            // panelTop
            // 
            panelTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelTop.BackColor = Color.FromArgb(64, 64, 64);
            panelTop.Controls.Add(tableLayoutPanel3);
            panelTop.Location = new Point(0, 25);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1980, 60);
            panelTop.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel3.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel3.Controls.Add(panel11, 2, 0);
            tableLayoutPanel3.Controls.Add(panel10, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Top;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(1980, 60);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(786, 54);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // panel11
            // 
            panel11.BackColor = Color.Gray;
            panel11.Controls.Add(lblAktifOperator);
            panel11.Dock = DockStyle.Fill;
            panel11.Location = new Point(1389, 3);
            panel11.Name = "panel11";
            panel11.Padding = new Padding(5);
            panel11.Size = new Size(588, 54);
            panel11.TabIndex = 4;
            // 
            // lblAktifOperator
            // 
            lblAktifOperator.AutoSize = true;
            lblAktifOperator.Dock = DockStyle.Fill;
            lblAktifOperator.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblAktifOperator.ForeColor = Color.White;
            lblAktifOperator.Location = new Point(5, 5);
            lblAktifOperator.Margin = new Padding(0);
            lblAktifOperator.Name = "lblAktifOperator";
            lblAktifOperator.Size = new Size(293, 30);
            lblAktifOperator.TabIndex = 0;
            lblAktifOperator.Text = "AKTİF OPERATÖR : Semih A.";
            // 
            // panel10
            // 
            panel10.BackColor = Color.Gray;
            panel10.Controls.Add(label5);
            panel10.Dock = DockStyle.Fill;
            panel10.Location = new Point(795, 3);
            panel10.Name = "panel10";
            panel10.Padding = new Padding(5);
            panel10.Size = new Size(588, 54);
            panel10.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label5.ForeColor = Color.White;
            label5.Location = new Point(5, 5);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(204, 30);
            label5.TabIndex = 0;
            label5.Text = "\U0001f7e2 SİSTEM : CANLI";
            // 
            // panelRight
            // 
            panelRight.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelRight.BackColor = Color.FromArgb(30, 30, 30);
            panelRight.Controls.Add(groupBox3);
            panelRight.Controls.Add(groupBox2);
            panelRight.Controls.Add(groupBox1);
            panelRight.Controls.Add(lblDefectCount);
            panelRight.Controls.Add(lblInferenceTime);
            panelRight.Location = new Point(927, 127);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(1053, 853);
            panelRight.TabIndex = 2;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(dgvHistory);
            groupBox3.ForeColor = SystemColors.ButtonFace;
            groupBox3.Location = new Point(3, 68);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(1046, 331);
            groupBox3.TabIndex = 5;
            groupBox3.TabStop = false;
            groupBox3.Text = "VERİTABANI SON 15 KAYIT";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(CroppedPictureBox);
            groupBox2.ForeColor = SystemColors.ButtonFace;
            groupBox2.Location = new Point(515, 405);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(535, 442);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "KIRPILAN KARE";
            // 
            // CroppedPictureBox
            // 
            CroppedPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            CroppedPictureBox.BackColor = SystemColors.ControlDark;
            CroppedPictureBox.Location = new Point(6, 19);
            CroppedPictureBox.Name = "CroppedPictureBox";
            CroppedPictureBox.Size = new Size(523, 417);
            CroppedPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            CroppedPictureBox.TabIndex = 0;
            CroppedPictureBox.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(txtVisionAnalysis);
            groupBox1.ForeColor = SystemColors.ButtonFace;
            groupBox1.Location = new Point(3, 405);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(506, 442);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "YAPAY ZEKA KALİTE RAPORU";
            // 
            // txtVisionAnalysis
            // 
            txtVisionAnalysis.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtVisionAnalysis.BackColor = SystemColors.ControlDark;
            txtVisionAnalysis.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 162);
            txtVisionAnalysis.ForeColor = Color.Black;
            txtVisionAnalysis.Location = new Point(6, 22);
            txtVisionAnalysis.Name = "txtVisionAnalysis";
            txtVisionAnalysis.ReadOnly = true;
            txtVisionAnalysis.Size = new Size(494, 414);
            txtVisionAnalysis.TabIndex = 0;
            txtVisionAnalysis.Text = "";
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ButtonFace;
            menuStrip1.Items.AddRange(new ToolStripItem[] { anaSayfaToolStripMenuItem, loglarToolStripMenuItem, ayarlarToolStripMenuItem, toolStripMenuItem1, toolStripMenuItem2, toolStripMenuItem3, çıkışYapToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1980, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // anaSayfaToolStripMenuItem
            // 
            anaSayfaToolStripMenuItem.Name = "anaSayfaToolStripMenuItem";
            anaSayfaToolStripMenuItem.Size = new Size(71, 20);
            anaSayfaToolStripMenuItem.Text = "Ana Sayfa";
            // 
            // loglarToolStripMenuItem
            // 
            loglarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { veritabanıKaydıToolStripMenuItem, hataListesiToolStripMenuItem });
            loglarToolStripMenuItem.Name = "loglarToolStripMenuItem";
            loglarToolStripMenuItem.Size = new Size(52, 20);
            loglarToolStripMenuItem.Text = "Loglar";
            // 
            // veritabanıKaydıToolStripMenuItem
            // 
            veritabanıKaydıToolStripMenuItem.Name = "veritabanıKaydıToolStripMenuItem";
            veritabanıKaydıToolStripMenuItem.Size = new Size(158, 22);
            veritabanıKaydıToolStripMenuItem.Text = "Veritabanı Kaydı";
            veritabanıKaydıToolStripMenuItem.Click += veritabanıKaydıToolStripMenuItem_Click;
            // 
            // hataListesiToolStripMenuItem
            // 
            hataListesiToolStripMenuItem.Name = "hataListesiToolStripMenuItem";
            hataListesiToolStripMenuItem.Size = new Size(158, 22);
            hataListesiToolStripMenuItem.Text = "Hata Listesi";
            hataListesiToolStripMenuItem.Click += hataListesiToolStripMenuItem_Click;
            // 
            // ayarlarToolStripMenuItem
            // 
            ayarlarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { kullanıcıEklemeToolStripMenuItem, kullanıcıBilgileriGüncellemeToolStripMenuItem });
            ayarlarToolStripMenuItem.Name = "ayarlarToolStripMenuItem";
            ayarlarToolStripMenuItem.Size = new Size(56, 20);
            ayarlarToolStripMenuItem.Text = "Ayarlar";
            // 
            // kullanıcıEklemeToolStripMenuItem
            // 
            kullanıcıEklemeToolStripMenuItem.Name = "kullanıcıEklemeToolStripMenuItem";
            kullanıcıEklemeToolStripMenuItem.Size = new Size(227, 22);
            kullanıcıEklemeToolStripMenuItem.Text = "Kullanıcı Ekleme";
            kullanıcıEklemeToolStripMenuItem.Click += kullanıcıEklemeToolStripMenuItem_Click;
            // 
            // kullanıcıBilgileriGüncellemeToolStripMenuItem
            // 
            kullanıcıBilgileriGüncellemeToolStripMenuItem.Name = "kullanıcıBilgileriGüncellemeToolStripMenuItem";
            kullanıcıBilgileriGüncellemeToolStripMenuItem.Size = new Size(227, 22);
            kullanıcıBilgileriGüncellemeToolStripMenuItem.Text = "Kullanıcı Bilgileri Güncelleme";
            kullanıcıBilgileriGüncellemeToolStripMenuItem.Click += kullanıcıBilgileriGüncellemeToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Alignment = ToolStripItemAlignment.Right;
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(29, 20);
            toolStripMenuItem1.Text = "✕";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Alignment = ToolStripItemAlignment.Right;
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(31, 20);
            toolStripMenuItem2.Text = "🗖";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Alignment = ToolStripItemAlignment.Right;
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(31, 20);
            toolStripMenuItem3.Text = "—";
            toolStripMenuItem3.Click += toolStripMenuItem3_Click;
            // 
            // çıkışYapToolStripMenuItem
            // 
            çıkışYapToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            çıkışYapToolStripMenuItem.BackColor = Color.FromArgb(192, 0, 0);
            çıkışYapToolStripMenuItem.ForeColor = Color.White;
            çıkışYapToolStripMenuItem.Name = "çıkışYapToolStripMenuItem";
            çıkışYapToolStripMenuItem.Size = new Size(66, 20);
            çıkışYapToolStripMenuItem.Text = "Çıkış Yap";
            çıkışYapToolStripMenuItem.Click += çıkışYapToolStripMenuItem_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(45, 45, 48);
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Location = new Point(2, 86);
            panel1.Name = "panel1";
            panel1.Size = new Size(1977, 42);
            panel1.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(panel5, 3, 0);
            tableLayoutPanel1.Controls.Add(panel4, 2, 0);
            tableLayoutPanel1.Controls.Add(panel3, 1, 0);
            tableLayoutPanel1.Controls.Add(panel2, 0, 0);
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1977, 42);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Gray;
            panel5.Controls.Add(label4);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(1485, 3);
            panel5.Name = "panel5";
            panel5.Padding = new Padding(5);
            panel5.Size = new Size(489, 36);
            panel5.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label4.ForeColor = Color.White;
            label4.Location = new Point(5, 5);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(219, 30);
            label4.TabIndex = 0;
            label4.Text = "HATA ORANI = %4.0";
            // 
            // panel4
            // 
            panel4.BackColor = Color.Gray;
            panel4.Controls.Add(label3);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(991, 3);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(5);
            panel4.Size = new Size(488, 36);
            panel4.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label3.ForeColor = Color.White;
            label3.Location = new Point(5, 5);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(207, 30);
            label3.TabIndex = 0;
            label3.Text = "HATALI (NOK) = 50";
            // 
            // panel3
            // 
            panel3.BackColor = Color.Gray;
            panel3.Controls.Add(label2);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(497, 3);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(5);
            panel3.Size = new Size(488, 36);
            panel3.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label2.ForeColor = Color.White;
            label2.Location = new Point(5, 5);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(234, 30);
            label2.TabIndex = 0;
            label2.Text = "SAĞLAM (OK) = 1,190";
            // 
            // panel2
            // 
            panel2.BackColor = Color.Gray;
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(5);
            panel2.Size = new Size(488, 36);
            panel2.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.ForeColor = Color.White;
            label1.Location = new Point(5, 5);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(301, 30);
            label1.TabIndex = 0;
            label1.Text = "TOPLAM İNCELENEN = 1,240";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.FromArgb(64, 64, 64);
            tableLayoutPanel2.ColumnCount = 6;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel2.Controls.Add(panel6, 3, 0);
            tableLayoutPanel2.Controls.Add(panel7, 2, 0);
            tableLayoutPanel2.Controls.Add(btnSelectImage, 5, 0);
            tableLayoutPanel2.Controls.Add(btnConnect, 4, 0);
            tableLayoutPanel2.Controls.Add(panel8, 1, 0);
            tableLayoutPanel2.Controls.Add(panel9, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Bottom;
            tableLayoutPanel2.Location = new Point(0, 978);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1980, 42);
            tableLayoutPanel2.TabIndex = 5;
            // 
            // panel6
            // 
            panel6.BackColor = Color.Gray;
            panel6.Controls.Add(button2);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(1240, 3);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(5);
            panel6.Size = new Size(241, 36);
            panel6.TabIndex = 3;
            // 
            // button2
            // 
            button2.BackColor = Color.Gray;
            button2.Dock = DockStyle.Fill;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button2.ForeColor = Color.White;
            button2.Location = new Point(5, 5);
            button2.Name = "button2";
            button2.Size = new Size(231, 26);
            button2.TabIndex = 1;
            button2.Text = "VARDİYA RAPORU AL (PDF)";
            button2.UseVisualStyleBackColor = false;
            // 
            // panel7
            // 
            panel7.BackColor = Color.Gray;
            panel7.Controls.Add(button1);
            panel7.Dock = DockStyle.Fill;
            panel7.Location = new Point(993, 3);
            panel7.Name = "panel7";
            panel7.Padding = new Padding(5);
            panel7.Size = new Size(241, 36);
            panel7.TabIndex = 2;
            // 
            // button1
            // 
            button1.BackColor = Color.Gray;
            button1.Dock = DockStyle.Fill;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(5, 5);
            button1.Name = "button1";
            button1.Size = new Size(231, 26);
            button1.TabIndex = 0;
            button1.Text = "SİREN MUTE / SUSTUR";
            button1.UseVisualStyleBackColor = false;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Gray;
            panel8.Controls.Add(OllamaLbl);
            panel8.Dock = DockStyle.Fill;
            panel8.Location = new Point(498, 3);
            panel8.Name = "panel8";
            panel8.Padding = new Padding(5);
            panel8.Size = new Size(489, 36);
            panel8.TabIndex = 1;
            // 
            // OllamaLbl
            // 
            OllamaLbl.AutoSize = true;
            OllamaLbl.Dock = DockStyle.Fill;
            OllamaLbl.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            OllamaLbl.ForeColor = Color.White;
            OllamaLbl.Location = new Point(5, 5);
            OllamaLbl.Margin = new Padding(0);
            OllamaLbl.Name = "OllamaLbl";
            OllamaLbl.Size = new Size(220, 30);
            OllamaLbl.TabIndex = 0;
            OllamaLbl.Text = "\U0001f7e2 OLLAMA : HAZIR";
            // 
            // panel9
            // 
            panel9.BackColor = Color.Gray;
            panel9.Controls.Add(WebSocketLbl);
            panel9.Dock = DockStyle.Fill;
            panel9.Location = new Point(3, 3);
            panel9.Name = "panel9";
            panel9.Padding = new Padding(5);
            panel9.Size = new Size(489, 36);
            panel9.TabIndex = 0;
            // 
            // WebSocketLbl
            // 
            WebSocketLbl.AutoSize = true;
            WebSocketLbl.Dock = DockStyle.Fill;
            WebSocketLbl.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            WebSocketLbl.ForeColor = Color.White;
            WebSocketLbl.Location = new Point(5, 5);
            WebSocketLbl.Margin = new Padding(0);
            WebSocketLbl.Name = "WebSocketLbl";
            WebSocketLbl.Size = new Size(251, 30);
            WebSocketLbl.TabIndex = 0;
            WebSocketLbl.Text = "\U0001f7e2 WEBSOCKET : BAĞLI";
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox4.BackColor = SystemColors.ActiveCaptionText;
            groupBox4.Controls.Add(picCamera);
            groupBox4.ForeColor = SystemColors.ButtonFace;
            groupBox4.Location = new Point(0, 0);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(916, 838);
            groupBox4.TabIndex = 6;
            groupBox4.TabStop = false;
            groupBox4.Text = "SEÇİLEN GÖRSEL";
            // 
            // panel12
            // 
            panel12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel12.BackColor = SystemColors.AppWorkspace;
            panel12.Controls.Add(groupBox4);
            panel12.Location = new Point(5, 134);
            panel12.Name = "panel12";
            panel12.Size = new Size(916, 838);
            panel12.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1980, 1020);
            Controls.Add(panel12);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(panel1);
            Controls.Add(panelRight);
            Controls.Add(panelTop);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TileGuard AI - Endüstriyel Anomali Tespiti Operatör Paneli";
            ((System.ComponentModel.ISupportInitialize)picCamera).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            panelTop.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            panelRight.ResumeLayout(false);
            panelRight.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)CroppedPictureBox).EndInit();
            groupBox1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            groupBox4.ResumeLayout(false);
            panel12.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
        private GroupBox groupBox1;
        private RichTextBox txtVisionAnalysis;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem anaSayfaToolStripMenuItem;
        private ToolStripMenuItem loglarToolStripMenuItem;
        private ToolStripMenuItem ayarlarToolStripMenuItem;
        private ToolStripMenuItem kullanıcıEklemeToolStripMenuItem;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel2;
        private Label label1;
        private Panel panel5;
        private Label label4;
        private Panel panel4;
        private Label label3;
        private Panel panel3;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel6;
        private Panel panel7;
        private Button button1;
        private Panel panel8;
        private Label OllamaLbl;
        private Panel panel9;
        private Label WebSocketLbl;
        private GroupBox groupBox2;
        private Button button2;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel panel11;
        private Label lblAktifOperator;
        private Panel panel10;
        private Label label5;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private PictureBox pictureBox1;
        private PictureBox CroppedPictureBox;
        private ToolStripMenuItem veritabanıKaydıToolStripMenuItem;
        private ToolStripMenuItem hataListesiToolStripMenuItem;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Panel panel12;
        private ToolStripMenuItem çıkışYapToolStripMenuItem;
        private ToolStripMenuItem kullanıcıBilgileriGüncellemeToolStripMenuItem;
    }
}