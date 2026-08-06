using TileGuard.UI.Helpers;
using TileGuard.UI.Models;
using TileGuard.UI.Services;

namespace TileGuard.UI
{
    public partial class UserManagementForm : BaseForm
    {
        private readonly DatabaseService _databaseService;
        private readonly LoggingService _logger;
        private List<UserModel> _userList;
        private int _selectedUserId = -1;
        public UserManagementForm()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            _logger = new LoggingService();
            _userList = new List<UserModel>();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            RegisterDragging(menuStrip1);
            UIHelper.StyleDataGridView(dgvUsers);
            this.Shown += UserManagementForm_Shown;
            dgvUsers.CellClick += dgvUsers_CellClick;
        }

        private async void UserManagementForm_Shown(object? sender, EventArgs e)
        {
            if (cmbRole != null)
            {
                cmbRole.Items.Clear();
                cmbRole.Items.Add("Admin");
                cmbRole.Items.Add("Operator");
                cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            if (txtUsername != null)
            {
                txtUsername.ReadOnly = true;
                txtUsername.BackColor = Color.FromArgb(50, 50, 50);
                txtUsername.ForeColor = Color.White;
            }
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                _userList = await _databaseService.GetAllUsersAsync();
                dgvUsers.DataSource = null;
                dgvUsers.DataSource = _userList;

                if (dgvUsers.Columns["PasswordHash"] != null)
                    dgvUsers.Columns["PasswordHash"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kullanıcılar yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUsers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < _userList.Count)
            {
                var selectedUser = _userList[e.RowIndex];
                _selectedUserId = selectedUser.UserId;

                txtUsername.Text = selectedUser.Username;
                txtFullName.Text = selectedUser.FullName;
                cmbRole.SelectedItem = selectedUser.Role;
                chkIsActive.Checked = selectedUser.IsActive;
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == -1)
            {
                MessageBox.Show("Lütfen listeden düzenlemek istediğiniz bir kullanıcıyı seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fullName = txtFullName.Text.Trim();
            string role = cmbRole.SelectedItem?.ToString() ?? "Operator";
            bool isActive = chkIsActive.Checked;

            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Ad Soyad alanı boş bırakılamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool success = await _databaseService.UpdateUserAsync(_selectedUserId, fullName, role, isActive);

            if (success)
            {
                await _logger.LogExceptionAsync("INFO", $"Kullanıcı güncellendi. ID: {_selectedUserId}, Ad: {fullName}", null);
                MessageBox.Show("Kullanıcı bilgileri başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadUsersAsync();
            }
            else
            {
                MessageBox.Show("Güncelleme sırasında bir veritabanı hatası oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == -1)
            {
                MessageBox.Show("Lütfen şifresini değiştirmek istediğiniz kullanıcıyı listeden seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newPassword = Microsoft.VisualBasic.Interaction.InputBox(
                "Lütfen yeni şifreyi giriniz:",
                "Şifre Değiştir",
                "",
                -1, -1);

            if (!string.IsNullOrEmpty(newPassword))
            {
                bool success = await _databaseService.UpdatePasswordAsync(_selectedUserId, newPassword);

                if (success)
                {
                    await _logger.LogExceptionAsync("INFO", $"Kullanıcı şifresi değiştirildi. ID: {_selectedUserId}", null);
                    MessageBox.Show("Kullanıcı şifresi başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Şifre güncellenirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;

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
    }
}
