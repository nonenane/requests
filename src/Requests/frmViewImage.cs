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
    public partial class frmViewImage : Form
    {
        Image img;

        /// <summary>
        /// процедура инициализации формы
        /// </summary>
        /// <param name="_img">изображение</param>
        public frmViewImage(Image _img)
        {
            img = _img;
            InitializeComponent();
        }

        private void frmViewImage_Load(object sender, EventArgs e)
        {
            pbImage.Image = img;
            pbImage.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void frmViewImage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
