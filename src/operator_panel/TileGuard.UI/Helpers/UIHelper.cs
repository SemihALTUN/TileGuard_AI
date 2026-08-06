using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileGuard.UI.Helpers
{
    public static class UIHelper
    {
        // Tüm DataGridView'ler için ortak stil metodu
        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            Color lightGrey = Color.FromArgb(160, 160, 160);
            Color headerGrey = Color.FromArgb(30, 30, 30);

            dgv.BackgroundColor = lightGrey;
            dgv.DefaultCellStyle.BackColor = lightGrey;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(50, 80, 120);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = headerGrey;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gainsboro;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerGrey;
            dgv.GridColor = Color.FromArgb(100, 100, 100);
            dgv.RowTemplate.Height = 26;
        }
    }
}
