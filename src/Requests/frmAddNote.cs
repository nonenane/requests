using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Requests
{
    public partial class frmAddNote : Form
    {
        public string ReturnedValue { get; set; }

        /// <summary>
        /// Форма добавления комментария
        /// </summary>
        /// <param name="mode"> 1 - ввод примечания
        ///                     2 - ввод причины добавления                   
        ///                    </param>
        public frmAddNote(int mode)
        {
            InitializeComponent();
            this.Text = (mode == 1 ? "Ввод примечания" : "Ввод причины добавления");

            if (mode == 2)
            {
                btCancel.Visible = false;
                btOK.Location = new Point((int)(this.Width / 2) - btOK.Width / 2, btOK.Location.Y);
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            ReturnedValue = tbPrimech.Text;
        }
    }
}
