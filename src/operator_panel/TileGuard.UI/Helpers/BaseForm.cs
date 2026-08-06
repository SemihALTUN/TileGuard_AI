using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileGuard.UI.Helpers
{
    public class BaseForm : Form
    {
        private bool _dragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;
                return handleParam;
            }
        }
        public void RegisterDragging(MenuStrip menuStrip)
        {
            if (menuStrip != null)
            {
                menuStrip.MouseDown += MenuStrip_MouseDown;
                menuStrip.MouseMove += MenuStrip_MouseMove;
                menuStrip.MouseUp += MenuStrip_MouseUp;
            }
        }

        private void MenuStrip_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = true;
                _dragCursorPoint = Cursor.Position;
                _dragFormPoint = this.Location;
            }
        }

        private void MenuStrip_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(_dragCursorPoint));
                this.Location = Point.Add(_dragFormPoint, new Size(dif));
            }
        }

        private void MenuStrip_MouseUp(object? sender, MouseEventArgs e) => _dragging = false;
    }
}
