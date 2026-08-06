namespace TileGuard.UI
{
    partial class UserManagementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserManagementForm));
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            groupBox1 = new GroupBox();
            dgvUsers = new DataGridView();
            panel1 = new Panel();
            btnChangePassword = new Button();
            chkIsActive = new CheckBox();
            label3 = new Label();
            btnClose = new Button();
            btnUpdate = new Button();
            txtFullName = new TextBox();
            label1 = new Label();
            txtUsername = new TextBox();
            label2 = new Label();
            cmbRole = new ComboBox();
            lblShift = new Label();
            menuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ButtonFace;
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2, toolStripMenuItem3 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1964, 24);
            menuStrip1.TabIndex = 6;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Alignment = ToolStripItemAlignment.Right;
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(29, 20);
            toolStripMenuItem1.Text = "✕";
            toolStripMenuItem1.Click += closeToolStripMenuItem_Click;
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
            toolStripMenuItem3.Click += minimizeToolStripMenuItem_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvUsers);
            groupBox1.Dock = DockStyle.Left;
            groupBox1.Location = new Point(0, 24);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(951, 957);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Kullanıcı Listesi ";
            // 
            // dgvUsers
            // 
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.Location = new Point(3, 19);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.Size = new Size(945, 935);
            dgvUsers.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(btnChangePassword);
            panel1.Controls.Add(chkIsActive);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(btnClose);
            panel1.Controls.Add(btnUpdate);
            panel1.Controls.Add(txtFullName);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtUsername);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(cmbRole);
            panel1.Controls.Add(lblShift);
            panel1.Location = new Point(951, 24);
            panel1.Name = "panel1";
            panel1.Size = new Size(1013, 957);
            panel1.TabIndex = 8;
            // 
            // btnChangePassword
            // 
            btnChangePassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnChangePassword.BackColor = Color.FromArgb(0, 192, 0);
            btnChangePassword.FlatAppearance.BorderSize = 0;
            btnChangePassword.FlatStyle = FlatStyle.Flat;
            btnChangePassword.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnChangePassword.ForeColor = Color.White;
            btnChangePassword.Location = new Point(266, 718);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(495, 79);
            btnChangePassword.TabIndex = 38;
            btnChangePassword.Text = "ŞİFRE DEĞİŞTİR";
            btnChangePassword.UseVisualStyleBackColor = false;
            btnChangePassword.Click += btnChangePassword_Click;
            // 
            // chkIsActive
            // 
            chkIsActive.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            chkIsActive.AutoSize = true;
            chkIsActive.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            chkIsActive.Location = new Point(493, 455);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(15, 14);
            chkIsActive.TabIndex = 37;
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label3.Location = new Point(265, 443);
            label3.Name = "label3";
            label3.Size = new Size(91, 37);
            label3.TabIndex = 36;
            label3.Text = "Aktif : ";
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnClose.BackColor = Color.FromArgb(192, 0, 0);
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(266, 607);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(495, 79);
            btnClose.TabIndex = 35;
            btnClose.Text = "İPTAL \\ KAPAT";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnUpdate.BackColor = Color.FromArgb(192, 192, 0);
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(266, 504);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(495, 79);
            btnUpdate.TabIndex = 34;
            btnUpdate.Text = "GÜNCELLE";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // txtFullName
            // 
            txtFullName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFullName.Font = new Font("Segoe UI", 18F);
            txtFullName.Location = new Point(492, 299);
            txtFullName.Multiline = true;
            txtFullName.Name = "txtFullName";
            txtFullName.PasswordChar = '*';
            txtFullName.Size = new Size(269, 37);
            txtFullName.TabIndex = 33;
            txtFullName.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label1.Location = new Point(265, 299);
            label1.Name = "label1";
            label1.Size = new Size(143, 37);
            label1.TabIndex = 32;
            label1.Text = "Ad Soyad :";
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.Enabled = false;
            txtUsername.Font = new Font("Segoe UI", 18F);
            txtUsername.Location = new Point(492, 220);
            txtUsername.Multiline = true;
            txtUsername.Name = "txtUsername";
            txtUsername.ReadOnly = true;
            txtUsername.Size = new Size(269, 37);
            txtUsername.TabIndex = 31;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label2.Location = new Point(265, 220);
            label2.Name = "label2";
            label2.Size = new Size(177, 37);
            label2.TabIndex = 30;
            label2.Text = "Kullanıcı Adı :";
            // 
            // cmbRole
            // 
            cmbRole.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FlatStyle = FlatStyle.Flat;
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(493, 385);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(268, 23);
            cmbRole.TabIndex = 29;
            // 
            // lblShift
            // 
            lblShift.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblShift.AutoSize = true;
            lblShift.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblShift.Location = new Point(265, 376);
            lblShift.Name = "lblShift";
            lblShift.Size = new Size(100, 37);
            lblShift.TabIndex = 28;
            lblShift.Text = "Yetki  : ";
            // 
            // UserManagementForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.AppWorkspace;
            ClientSize = new Size(1964, 981);
            Controls.Add(panel1);
            Controls.Add(groupBox1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "UserManagementForm";
            Text = "UserManagementForm";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private GroupBox groupBox1;
        private DataGridView dgvUsers;
        private Panel panel1;
        private TextBox txtFullName;
        private Label label1;
        private TextBox txtUsername;
        private Label label2;
        private ComboBox cmbRole;
        private Label lblShift;
        private CheckBox chkIsActive;
        private Label label3;
        private Button btnClose;
        private Button btnUpdate;
        private Button btnChangePassword;
    }
}