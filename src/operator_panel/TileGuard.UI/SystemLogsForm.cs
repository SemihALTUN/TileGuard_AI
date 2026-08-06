using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using TileGuard.UI.Services;

namespace TileGuard.UI
{
    public partial class SystemLogsForm : Form
    {
        private bool _dragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        private readonly LoggingService _loggingService;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return handleParam;
            }
        }
        public SystemLogsForm()
        {
            InitializeComponent();
            _loggingService = new LoggingService();

            if (menuStrip1 != null)
            {
                menuStrip1.MouseDown += MenuStrip_MouseDown;
                menuStrip1.MouseMove += MenuStrip_MouseMove;
                menuStrip1.MouseUp += MenuStrip_MouseUp;
            }

            StyleDataGridView();

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

        private void StyleDataGridView()
        {
            dgvSystemLogs.RowHeadersVisible = false;
            dgvSystemLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSystemLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSystemLogs.MultiSelect = false;
            dgvSystemLogs.ReadOnly = true;

            dgvSystemLogs.AllowUserToResizeColumns = false;
            dgvSystemLogs.AllowUserToResizeRows = false;
            dgvSystemLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            Color lightGrey = Color.FromArgb(160, 160, 160);
            Color headerGrey = Color.FromArgb(30, 30, 30);

            dgvSystemLogs.BackgroundColor = lightGrey;
            dgvSystemLogs.DefaultCellStyle.BackColor = lightGrey;
            dgvSystemLogs.DefaultCellStyle.ForeColor = Color.Black;
            dgvSystemLogs.DefaultCellStyle.SelectionBackColor = Color.FromArgb(50, 80, 120);
            dgvSystemLogs.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvSystemLogs.EnableHeadersVisualStyles = false;
            dgvSystemLogs.ColumnHeadersDefaultCellStyle.BackColor = headerGrey;
            dgvSystemLogs.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gainsboro;
            dgvSystemLogs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvSystemLogs.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerGrey;
            dgvSystemLogs.GridColor = Color.FromArgb(100, 100, 100);
            dgvSystemLogs.RowTemplate.Height = 26;
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

        private void MenuStrip_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }
    }
}