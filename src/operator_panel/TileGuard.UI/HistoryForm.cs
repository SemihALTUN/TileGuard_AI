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
    public partial class HistoryForm : Form
    {
        private bool _dragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        private readonly DatabaseService _databaseService;
        private readonly LoggingService _logger = new LoggingService();

        public HistoryForm()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();

            menuStrip1.MouseDown += MenuStrip_MouseDown;
            menuStrip1.MouseMove += MenuStrip_MouseMove;
            menuStrip1.MouseUp += MenuStrip_MouseUp;

            StyleDataGridView();
            dgvAllHistory.CellFormatting += dgvAllHistory_CellFormatting;
            dgvAllHistory.DataBindingComplete += dgvAllHistory_DataBindingComplete;
            this.Load += HistoryForm_Load;
        }

        private async void HistoryForm_Load(object? sender, EventArgs e)
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

        private void StyleDataGridView()
        {
            dgvAllHistory.RowHeadersVisible = false;
            dgvAllHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAllHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAllHistory.MultiSelect = false;
            dgvAllHistory.ReadOnly = true;

            dgvAllHistory.AllowUserToResizeColumns = false;
            dgvAllHistory.AllowUserToResizeRows = false;
            dgvAllHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            Color lightGrey = Color.FromArgb(160, 160, 160);
            Color headerGrey = Color.FromArgb(30, 30, 30);

            dgvAllHistory.BackgroundColor = lightGrey;
            dgvAllHistory.DefaultCellStyle.BackColor = lightGrey;
            dgvAllHistory.DefaultCellStyle.ForeColor = Color.Black;
            dgvAllHistory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(50, 80, 120);
            dgvAllHistory.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvAllHistory.EnableHeadersVisualStyles = false;
            dgvAllHistory.ColumnHeadersDefaultCellStyle.BackColor = headerGrey;
            dgvAllHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gainsboro;
            dgvAllHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvAllHistory.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerGrey;
            dgvAllHistory.GridColor = Color.FromArgb(100, 100, 100);
            dgvAllHistory.RowTemplate.Height = 26;
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
