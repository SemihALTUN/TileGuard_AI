using TileGuard.UI.Helpers;
using TileGuard.UI.Services;

namespace TileGuard.UI
{
    public partial class SystemLogsForm : BaseForm
    {
        private readonly LoggingService _loggingService;

        public SystemLogsForm()
        {
            InitializeComponent();
            _loggingService = new LoggingService();
            RegisterDragging(menuStrip1);
            UIHelper.StyleDataGridView(dgvSystemLogs);
            dgvSystemLogs.CellFormatting += dgvSystemLogs_CellFormatting;
            dgvSystemLogs.DataBindingComplete += dgvSystemLogs_DataBindingComplete;

            this.Shown += SystemLogsForm_Shown;
        }

        private async void SystemLogsForm_Shown(object? sender, EventArgs e)
        {
            await LoadLogsToGridAsync();
        }

        private async Task LoadLogsToGridAsync()
        {
            try
            {
                var logs = await _loggingService.GetAllLogsAsync();

                if (dgvSystemLogs != null)
                {
                    dgvSystemLogs.DataSource = null;
                    dgvSystemLogs.DataSource = logs;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sistem logları yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvSystemLogs_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvSystemLogs.Columns[e.ColumnIndex].Name == "LogLevel" && e.Value != null)
            {
                string logLevel = e.Value.ToString() ?? "";
                if (logLevel.Contains("ERROR"))
                {
                    e.CellStyle.ForeColor = Color.DarkRed;
                    e.CellStyle.Font = new Font(dgvSystemLogs.Font, FontStyle.Bold);
                }
                else if (logLevel.Contains("WARNING"))
                {
                    e.CellStyle.ForeColor = Color.DarkOrange;
                    e.CellStyle.Font = new Font(dgvSystemLogs.Font, FontStyle.Bold);
                }
                else if (logLevel.Contains("INFO"))
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                    e.CellStyle.Font = new Font(dgvSystemLogs.Font, FontStyle.Bold);
                }
            }
        }

        private void dgvSystemLogs_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvSystemLogs.ClearSelection();
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
    }
}