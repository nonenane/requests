using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Requests
{
    public delegate bool PreRemoveTab(int indx);
    public class TabControlEx : TabControl
    {
        public TabControlEx()
            : base()
        {
            PreRemoveTabPage = null;
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        public PreRemoveTab PreRemoveTabPage;

        //Рисуем вкладку
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index <= this.TabPages.Count -1)
            {
                Rectangle r = e.Bounds;
                r = GetTabRect(e.Index);

                Brush b = new SolidBrush(Color.Gray);
                Brush t = new SolidBrush(Color.Black);
                Pen p = new Pen(b);
                p.Width = 3;

                //Рисуем надпись на вкладке
                string titel = this.TabPages[e.Index].Text;
                Font f = this.Font;
                e.Graphics.DrawString(titel, f, t, new PointF(r.X, r.Y + 2));

                //Рисуем кнопку закрыть
                r.Offset(r.Width - 15, 2);
                r.Width = 10;
                r.Height = 10;
                e.Graphics.DrawLine(p, r.X, r.Y + 2, r.X + 10, r.Y + 12);
                e.Graphics.DrawLine(p, r.X + 10, r.Y + 2, r.X, r.Y + 12);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            Point p = e.Location;
            for (int i = 0; i < TabCount; i++)
            {
                Rectangle r = GetTabRect(i);
                r.Offset(r.Width - 14, 2);
                r.Width = 20;
                r.Height = 20;
                if (r.Contains(p))
                {
                    CloseTab(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Закрытие вкладки
        /// </summary>
        /// <param name="i">Порядковый номер вкладки</param>
        private void CloseTab(int i)
        {
            if (PreRemoveTabPage != null)
            {
                bool closeIt = PreRemoveTabPage(i);
                if (!closeIt)
                    return;
            }
            TabPages.Remove(TabPages[i]);
        }
    }
}
