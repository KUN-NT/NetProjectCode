using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;


namespace SimplePlayerApp.Controls
{
    public class SimpleTabControl : TabControl
    {

        private int iconWidth = 15;
        private int iconHeight = 13;
        private Bitmap icon = null;

        public SimpleTabControl()
            : base()
        {
            this.DrawMode = TabDrawMode.OwnerDrawFixed;

            icon =new Bitmap(@"E:\AllDocuments\ITClass\15year\ItcastCater\CaterComman\th.jpg");
            iconWidth = icon.Width-100;
            iconHeight = icon.Height-100;
        }


        protected override void OnDrawItem(DrawItemEventArgs e)
        {

            Graphics g = e.Graphics;
            Rectangle r = GetTabRect(e.Index);

            g.FillRectangle(Brushes.White, r);
            string title = this.TabPages[e.Index].Text;
            g.DrawString(title, this.Font, new SolidBrush(Color.Black), new PointF(r.X + 2, r.Y + 2));

            r.Offset(r.Width - iconWidth - 3, 2);
            g.DrawImage(icon, new Point(r.X, r.Y));


        }


        protected override void OnMouseClick(MouseEventArgs e)
        {
            Point p = e.Location;
            Rectangle r = GetTabRect(this.SelectedIndex);
            r.Offset(r.Width - iconWidth - 3, 2);
            r.Width = iconWidth;
            r.Height = iconHeight;
            if (r.Contains(p)) this.TabPages.RemoveAt(this.SelectedIndex);
        }


    }
}