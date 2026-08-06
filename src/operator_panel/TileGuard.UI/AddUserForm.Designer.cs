namespace TileGuard.UI
{
    partial class AddUserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddUserForm));
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            pnlCard = new Panel();
            txtFullName = new TextBox();
            label1 = new Label();
            txtUsername = new TextBox();
            label2 = new Label();
            cmbRole = new ComboBox();
            lblShift = new Label();
            txtPassword = new TextBox();
            lblPassword = new Label();
            btnSaveUser = new Button();
            menuStrip1.SuspendLayout();
            pnlCard.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ButtonFace;
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2, toolStripMenuItem3 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(560, 24);
            menuStrip1.TabIndex = 6;
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
            pnlCard.Controls.Add(txtFullName);
            pnlCard.Controls.Add(label1);
            pnlCard.Controls.Add(txtUsername);
            pnlCard.Controls.Add(label2);
            pnlCard.Controls.Add(cmbRole);
            pnlCard.Controls.Add(lblShift);
            pnlCard.Controls.Add(txtPassword);
            pnlCard.Controls.Add(lblPassword);
            pnlCard.Controls.Add(btnSaveUser);
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.Location = new Point(0, 24);
            pnlCard.Margin = new Padding(0);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(560, 926);
            pnlCard.TabIndex = 7;
            // 
            // txtFullName
            // 
            txtFullName.Font = new Font("Segoe UI", 18F);
            txtFullName.Location = new Point(237, 264);
            txtFullName.Multiline = true;
            txtFullName.Name = "txtFullName";
            txtFullName.PasswordChar = '*';
            txtFullName.Size = new Size(269, 37);
            txtFullName.TabIndex = 27;
            txtFullName.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label1.Location = new Point(10, 264);
            label1.Name = "label1";
            label1.Size = new Size(143, 37);
            label1.TabIndex = 26;
            label1.Text = "Ad Soyad :";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 18F);
            txtUsername.Location = new Point(237, 185);
            txtUsername.Multiline = true;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(269, 37);
            txtUsername.TabIndex = 25;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label2.Location = new Point(10, 185);
            label2.Name = "label2";
            label2.Size = new Size(177, 37);
            label2.TabIndex = 24;
            label2.Text = "Kullanıcı Adı :";
            // 
            // cmbRole
            // 
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FlatStyle = FlatStyle.Flat;
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(238, 417);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(268, 23);
            cmbRole.TabIndex = 23;
            // 
            // lblShift
            // 
            lblShift.AutoSize = true;
            lblShift.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblShift.Location = new Point(10, 408);
            lblShift.Name = "lblShift";
            lblShift.Size = new Size(100, 37);
            lblShift.TabIndex = 22;
            lblShift.Text = "Yetki  : ";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 18F);
            txtPassword.Location = new Point(238, 334);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(269, 39);
            txtPassword.TabIndex = 21;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblPassword.Location = new Point(10, 334);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(89, 37);
            lblPassword.TabIndex = 20;
            lblPassword.Text = "Şifre : ";
            // 
            // btnSaveUser
            // 
            btnSaveUser.BackColor = Color.FromArgb(0, 192, 0);
            btnSaveUser.FlatAppearance.BorderSize = 0;
            btnSaveUser.FlatStyle = FlatStyle.Flat;
            btnSaveUser.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnSaveUser.ForeColor = Color.White;
            btnSaveUser.Location = new Point(152, 527);
            btnSaveUser.Name = "btnSaveUser";
            btnSaveUser.Size = new Size(242, 48);
            btnSaveUser.TabIndex = 17;
            btnSaveUser.Text = "KULLANICIYI KAYDET";
            btnSaveUser.UseVisualStyleBackColor = false;
            btnSaveUser.Click += btnSaveUser_Click;
            // 
            // AddUserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.AppWorkspace;
            ClientSize = new Size(560, 950);
            Controls.Add(pnlCard);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AddUserForm";
            Text = "AddUserForm";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private Panel pnlCard;
        private TextBox txtFullName;
        private Label label1;
        private TextBox txtUsername;
        private Label label2;
        private ComboBox cmbRole;
        private Label lblShift;
        private TextBox txtPassword;
        private Label lblPassword;
        private Button btnSaveUser;
    }
}