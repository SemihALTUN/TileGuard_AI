using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TileGuard.UI.Services;

namespace TileGuard.UI
{
    public partial class AddUserForm : Form
    {
        private bool _dragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        private readonly DatabaseService _databaseService;
        private readonly LoggingService _logger = new LoggingService();
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;
                return handleParam;
            }
        }
        public AddUserForm()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();

            if (menuStrip1 != null)
            {
                menuStrip1.MouseDown += MenuStrip_MouseDown;
                menuStrip1.MouseMove += MenuStrip_MouseMove;
                menuStrip1.MouseUp += MenuStrip_MouseUp;
            }

            this.Shown += AddUserForm_Shown;
        }

        private void AddUserForm_Shown(object? sender, EventArgs e)
        {
            if (cmbRole != null)
            {
                cmbRole.Items.Clear();
                cmbRole.Items.Add("Admin");
                cmbRole.Items.Add("Operator");
                cmbRole.SelectedIndex = 1;
            }

            if (txtPassword != null)
            {
                txtPassword.PasswordChar = '*';
            }
        }

        private async void btnSaveUser_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cmbRole?.SelectedItem?.ToString() ?? "Operator";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Lütfen tüm alanları eksiksiz doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = await _databaseService.CreateUserAsync(username, password, fullName, role);

                if (success)
                {
                    await _logger.LogExceptionAsync("INFO", $"Yeni kullanıcı oluşturuldu: {username} ({role})", null);
                    MessageBox.Show("Yeni kullanıcı başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Kullanıcı eklenirken bir veritabanı hatası oluştu (Aynı kullanıcı adı kullanılıyor olabilir).", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogExceptionAsync("ERROR", $"AddUser Form Kayıt Hatası: {ex.Message}", ex.StackTrace);
                MessageBox.Show($"Beklenmeyen bir hata oluştu: {ex.Message}", "Sistem Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) => this.Close();
        private void toolStripMenuItem3_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;

        private void MenuStrip_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = true;
                _dragCursorPoint = Cursor.Position;
                _dragFormPoint = this.Location;
            }
        }

        private void MenuStrip_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(_dragCursorPoint));
                this.Location = Point.Add(_dragFormPoint, new Size(dif));
            }
        }

        private void MenuStrip_MouseUp(object sender, MouseEventArgs e) => _dragging = false;
    }
}
