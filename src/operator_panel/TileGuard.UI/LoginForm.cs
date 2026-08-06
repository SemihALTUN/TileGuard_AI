using TileGuard.UI.Helpers;
using TileGuard.UI.Models;
using TileGuard.UI.Services;

namespace TileGuard.UI
{
    public partial class LoginForm : BaseForm
    {
        private readonly DatabaseService _databaseService;
        private readonly LoggingService _logger = new LoggingService();
        public UserModel? CurrentUser { get; private set; }
        public ShiftModel? CurrentShift { get; private set; }
        public LoginForm()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            RegisterDragging(menuStrip1);
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