using TileGuard.UI.Helpers;
using TileGuard.UI.Services;

namespace TileGuard.UI
{
    public partial class AddUserForm : BaseForm
    {
        private readonly DatabaseService _databaseService;
        private readonly LoggingService _logger = new LoggingService();
        public AddUserForm()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            RegisterDragging(menuStrip1);
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
    }
}
