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
    public partial class frmAddFileName : Form
    {
        public string ReturnedValue { get; set; }
				bool close = false;

        public frmAddFileName()
        {
            InitializeComponent();
            ReturnedValue = "";
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (tbPrimech.Text.Trim().Length == 0)
            {
                MessageBox.Show("Введите наименование изображения!");
                return;
            }
						
						close = true;
            ReturnedValue = tbPrimech.Text.Trim();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Отменить добавление изображения?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (d == DialogResult.Yes)
            {
								close = true;
                this.Close();
            }
				}

				private void frmAddFileName_FormClosing(object sender, FormClosingEventArgs e)
				{
					if(!close)
					{
						e.Cancel = true;
					}

				}
    }
}
