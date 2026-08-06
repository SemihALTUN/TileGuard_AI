using TileGuard.UI.Helpers;
using TileGuard.UI.Services;

namespace TileGuard.UI
{
    public partial class HistoryForm : BaseForm
    {
        private readonly DatabaseService _databaseService;
        private readonly LoggingService _logger = new LoggingService();

        public HistoryForm()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            RegisterDragging(menuStrip1);
            UIHelper.StyleDataGridView(dgvAllHistory);
            dgvAllHistory.CellFormatting += dgvAllHistory_CellFormatting;
            dgvAllHistory.DataBindingComplete += dgvAllHistory_DataBindingComplete;
            this.Shown += HistoryForm_Shown;
        }

        private async void HistoryForm_Shown(object? sender, EventArgs e)
        {
            await LoadAllHistoryAsync();
        }

        private async Task LoadAllHistoryAsync()
        {
            try
            {
                var allHistory = await _databaseService.GetInspectionHistoryAsync(limit: 100000);

                dgvAllHistory.DataSource = null;
                dgvAllHistory.DataSource = allHistory;
            }
            catch (Exception ex)
            {
                await _logger.LogExceptionAsync("ERROR", $"Geçmiş verileri yüklenirken hata: {ex.Message}", ex.StackTrace);
                MessageBox.Show($"Tüm geçmiş yüklenirken hata oluştu: {ex.Message}", "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void dgvAllHistory_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvAllHistory.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString() ?? "";
                if (status.Contains("SAĞLAM"))
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                    e.CellStyle.Font = new Font(dgvAllHistory.Font, FontStyle.Bold);
                }
                else if (status.Contains("KUSURLU"))
                {
                    e.CellStyle.ForeColor = Color.DarkRed;
                    e.CellStyle.Font = new Font(dgvAllHistory.Font, FontStyle.Bold);
                }
            }

            if (dgvAllHistory.Columns[e.ColumnIndex].Name == "Confidence" && e.Value != null)
            {
                e.CellStyle.ForeColor = Color.DarkBlue;
                e.CellStyle.Font = new Font(dgvAllHistory.Font, FontStyle.Bold);
            }
        }

        private void dgvAllHistory_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvAllHistory.ClearSelection();
        }
    }
}
