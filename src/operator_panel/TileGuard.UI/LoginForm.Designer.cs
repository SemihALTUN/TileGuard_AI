namespace TileGuard.UI
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            pnlCard = new Panel();
            panel1 = new Panel();
            lblError = new Label();
            cmbShift = new ComboBox();
            lblShift = new Label();
            btnLogin = new Button();
            btnExit = new Button();
            txtPassword = new TextBox();
            lblPassword = new Label();
            txtUsername = new TextBox();
            lblUsername = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            menuStrip1.SuspendLayout();
            pnlCard.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ButtonFace;
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2, toolStripMenuItem3 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1980, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
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
            // pnlCard
            // 
            pnlCard.Anchor = AnchorStyles.None;
            pnlCard.Controls.Add(panel1);
            pnlCard.Controls.Add(label1);
            pnlCard.Controls.Add(pictureBox1);
            pnlCard.Location = new Point(560, 178);
            pnlCard.Margin = new Padding(0);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(800, 700);
            pnlCard.TabIndex = 6;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblError);
            panel1.Controls.Add(cmbShift);
            panel1.Controls.Add(lblShift);
            panel1.Controls.Add(btnLogin);
            panel1.Controls.Add(btnExit);
            panel1.Controls.Add(txtPassword);
            panel1.Controls.Add(lblPassword);
            panel1.Controls.Add(txtUsername);
            panel1.Controls.Add(lblUsername);
            panel1.Location = new Point(109, 239);
            panel1.Name = "panel1";
            panel1.Size = new Size(575, 393);
            panel1.TabIndex = 3;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblError.ForeColor = Color.FromArgb(192, 0, 0);
            lblError.Location = new Point(256, 264);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 20);
            lblError.TabIndex = 10;
            // 
            // cmbShift
            // 
            cmbShift.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbShift.FlatStyle = FlatStyle.Flat;
            cmbShift.FormattingEnabled = true;
            cmbShift.Location = new Point(256, 225);
            cmbShift.Name = "cmbShift";
            cmbShift.Size = new Size(297, 23);
            cmbShift.TabIndex = 9;
            // 
            // lblShift
            // 
            lblShift.AutoSize = true;
            lblShift.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblShift.Location = new Point(28, 216);
            lblShift.Name = "lblShift";
            lblShift.Size = new Size(193, 37);
            lblShift.TabIndex = 8;
            lblShift.Text = "Aktif Vardiya  : ";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(0, 192, 0);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(413, 327);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(141, 48);
            btnLogin.TabIndex = 7;
            btnLogin.Text = "GİRİŞ YAP";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click_1;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(192, 0, 0);
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(28, 327);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(141, 48);
            btnExit.TabIndex = 6;
            btnExit.Text = "ÇIKIŞ YAP";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 18F);
            txtPassword.Location = new Point(256, 142);
            txtPassword.Multiline = true;
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(298, 37);
            txtPassword.TabIndex = 5;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblPassword.Location = new Point(132, 142);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(89, 37);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Şifre : ";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 18F);
            txtUsername.Location = new Point(256, 63);
            txtUsername.Multiline = true;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(298, 37);
            txtUsername.TabIndex = 3;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblUsername.Location = new Point(44, 63);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(177, 37);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Kullanıcı Adı :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.Location = new Point(7, 50);
            label1.Name = "label1";
            label1.Size = new Size(786, 54);
            label1.TabIndex = 1;
            label1.Text = "ENDÜSTRİYEL KALİTE KONTROL SİSTEMİ";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Image = Properties.Resources.Logo;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.AppWorkspace;
            ClientSize = new Size(1980, 1020);
            Controls.Add(pnlCard);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoginForm";
            Load += LoginForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private Panel pnlCard;
        private Label label1;
        private PictureBox pictureBox1;
        private Panel panel1;
        private Button btnLogin;
        private Button btnExit;
        private TextBox txtPassword;
        private Label lblPassword;
        private TextBox txtUsername;
        private Label lblUsername;
        private ComboBox cmbShift;
        private Label lblShift;
        private Label lblError;
    }
}