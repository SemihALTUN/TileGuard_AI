using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using TileGuard.UI.Models;
using TileGuard.UI.Services;

namespace TileGuard.UI
{
    public partial class LoginForm : Form
    {
        private readonly DatabaseService _databaseService;
        private readonly LoggingService _logger = new LoggingService();
        private bool _dragging;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        public UserModel? CurrentUser { get; private set; }
        public ShiftModel? CurrentShift { get; private set; }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return handleParam;
            }
        }
        public LoginForm()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();

            SetupDragging();
            this.AcceptButton = btnLogin;

            this.Shown += LoginForm_Shown;
        }
        private async void LoginForm_Shown(object sender, EventArgs e)
        {
            await LoadShiftsFromDatabaseAsync();
        }
        private async Task LoadShiftsFromDatabaseAsync()
        {
            try
            {
                var shifts = await _databaseService.GetActiveShiftsAsync();
                cmbShift.DataSource = shifts;

                if (shifts.Count > 0)
                {
                    TimeSpan now = DateTime.Now.TimeOfDay;
                    int selectedIndex = 0;

                    for (int i = 0; i < shifts.Count; i++)
                    {
                        if (now >= shifts[i].StartTime && now < shifts[i].EndTime)
                        {
                            selectedIndex = i;
                            break;
                        }
                    }
                    cmbShift.SelectedIndex = selectedIndex;
                }
            }
            catch (Exception ex)
            {
                await _logger.LogExceptionAsync("ERROR", $"Vardiyalar yüklenemedi: {ex.Message}", ex.StackTrace);
                if (lblError != null)
                    lblError.Text = "Vardiyalar yüklenemedi: " + ex.Message;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SetupDragging()
        {
            this.MouseDown += Form_MouseDown;
            this.MouseMove += Form_MouseMove;
            this.MouseUp += Form_MouseUp;
        }

        private void Form_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = true;
                _dragCursorPoint = Cursor.Position;
                _dragFormPoint = this.Location;
            }
        }

        private void Form_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(_dragCursorPoint));
                this.Location = Point.Add(_dragFormPoint, new Size(dif));
            }
        }

        private void Form_MouseUp(object? sender, MouseEventArgs e) => _dragging = false;

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

        private async void btnLogin_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                if (lblError != null) lblError.Text = "Lütfen kullanıcı adı ve şifre girin!";
                return;
            }

            if (cmbShift.SelectedItem == null)
            {
                if (lblError != null) lblError.Text = "Lütfen aktif vardiyayı seçin!";
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "DOĞRULANIYOR...";
            if (lblError != null) lblError.Text = "";

            try
            {
                var user = await _databaseService.ValidateUserAsync(username, password);

                if (user != null)
                {
                    CurrentUser = user;
                    CurrentShift = (ShiftModel)cmbShift.SelectedItem;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    if (lblError != null) lblError.Text = "Kullanıcı adı ya da şifre hatalı!";
                    await _logger.LogExceptionAsync("WARNING", $"Başarısız giriş denemesi: {username}", null);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogExceptionAsync("ERROR", $"Login İşlem Hatası: {ex.Message}", ex.StackTrace);
                if (lblError != null)
                    lblError.Text = "Sistem Hatası: Veritabanı bağlantısı kurulamadı.";
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "GİRİŞ YAP";
            }
        }
    }
}