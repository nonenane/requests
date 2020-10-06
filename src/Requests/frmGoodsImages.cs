using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IC = Nwuram.Framework.scan.ImageClass;

using System.IO;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.scan;

namespace Requests
{
    public partial class frmGoodsImages : Form
    {
        int id_tov;
        string ean, cName;

        Image image = null;
        DataTable dtIm;
        string ImagePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\images";
        //string ImagePath = @"C:\1" + "\\images";
        

        public frmGoodsImages(int _id_tovar, string _ean, string _cName)
        {
            id_tov = _id_tovar;
            ean = _ean;
            cName = _cName;            
            InitializeComponent();
        }

        private void frmGoodsImages_Load(object sender, EventArgs e)
        {
            btnDel.Visible =
                btnScan.Visible =
                btnUpload.Visible = 
                !(UserSettings.User.StatusCode == "КД");

            if (!Directory.Exists(ImagePath))
            {
                Directory.CreateDirectory(ImagePath);
            }

            InputLanguage.CurrentInputLanguage = GetInputLanguageByName("ru");
            tbEan.Text = ean.Trim();
            tbCname.Text = cName.Trim();

            GetImagesList();
        }

        /// <summary>
        /// Выбор языка из установленых
        /// </summary>
        /// <param name="inputName">язык</param>
        /// <returns></returns>
        public static InputLanguage GetInputLanguageByName(string inputName)
        {
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.EnglishName.ToLower().StartsWith(inputName))
                {
                    return lang;
                }
            }
            return null;
        }

        private void GetImagesList()
        {
            dtIm = new DataTable();
            dtIm = Config.hCntMain.GetImagesByIdTov(id_tov);                

            dgImages.DataSource = null;
            dgImages.AutoGenerateColumns = false;
            dgImages.DataSource = dtIm;

            GetImage();
        }

        private void GetImage()
        {
            if (dgImages.CurrentRow != null)
            {
                image = null;
                pbPhoto.Image = null;

                if ((dtIm != null) && (dtIm.Rows.Count > 0))
                {
                    byte[] ImageBytes = (byte[])dtIm.Select("id = " + dgImages.CurrentRow.Cells["id"].Value.ToString())[0]["Pic"];
                    image = IC.ByteArrayToImage(ImageBytes);
                }

                pbPhoto.Image = image;

                pbPhoto.Location = new Point(3, 3);
                pbPhoto.Size = new Size(panel1.Width - 2, panel1.Height - 2);


                if (pbPhoto.Image != null)
                {
                    pbPhoto.SizeMode = PictureBoxSizeMode.Zoom;
                }

                btPrint.Enabled = true;
                btnDel.Enabled = true;
                btnZoomIn.Enabled = true;
                btnZoomOut.Enabled = true;
                btnLeft.Enabled = !(dgImages.CurrentRow.Index == 0);
                btnRight.Enabled = !(dgImages.CurrentRow.Index == dgImages.Rows.Count - 1);
            }
            else
            {
                image = null;
                pbPhoto.Image = null;

                btPrint.Enabled = false;
                btnDel.Enabled = false;
                btnLeft.Enabled = false;
                btnRight.Enabled = false;
                btnZoomIn.Enabled = false;
                btnZoomOut.Enabled = false;
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            try
            {
                dgImages.CurrentCell = dgImages.Rows[dgImages.CurrentRow.Index - 1].Cells[0];
                dgImages.CurrentCell.Selected = true;
                GetImage();
            }
            catch { }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            try
            {
                dgImages.CurrentCell = dgImages.Rows[dgImages.CurrentRow.Index + 1].Cells[0];
                dgImages.CurrentCell.Selected = true;
                GetImage();
            }
            catch { }
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            pbPhoto.Height -= 20;
            pbPhoto.Width -= 20;
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            pbPhoto.Height += 20;
            pbPhoto.Width += 20;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgImages.CurrentRow != null)
            {
                DialogResult d = MessageBox.Show("Удалить выбранное \nизображение?", "Удаление изображения", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (d == DialogResult.Yes)
                {
                    int idImage = int.Parse(dgImages.CurrentRow.Cells["id"].Value.ToString());
                    Config.hCntMain.DelGoodsImage(idImage);

                    GetImagesList();
                }
            }
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            try
            {                
                string path = ImagePath + @"\" + "temp.jpg";

                SaveTempImage(path, pbPhoto.Image);

                System.Diagnostics.Process.Start(path);
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Ошибка открытия файла для печати! \n"
                    + "Возможно, отсутствие папки image в каталоге с программой \n"
                    + "или недостаточно прав для создания папки image в каталоге с программой \n\n"
                    + "Системное сообщение: \n"
                    + ex.Message); 
            }
        }

        private void SaveTempImage(string imPath, Image TempImage)
        {
            if (File.Exists(imPath))
            {
                File.Delete(imPath);
            }

            TempImage.Save(imPath);
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(ImagePath))
            {
                DelGarbage();
            }
            this.Close();
        }

        private void DelGarbage()
        {
            string[] aFiles = Directory.GetFiles(ImagePath, "*.jpg", SearchOption.TopDirectoryOnly);

            for (int i = 0; i < aFiles.Length; i++)
            {
                try
                {
                    File.Delete(aFiles[i].ToString());
                }
                catch { }
            }
        }

        private void dgImages_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                GetImage();
            }
            catch { }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Environment.SpecialFolder.MyComputer.ToString();            
            dlg.Filter = "Все рисунки (*.jpg, *.jpeg, *.jpe, *.png)|*.jpg; *.jpeg; *.jpe; *.png*";
            dlg.FilterIndex = 1;
            dlg.Multiselect = false;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (String filepath in dlg.FileNames)
                {
                    Image picForCheck = Image.FromFile(filepath);

                    if (picForCheck != null)
                    {
                        System.IO.FileInfo file = new System.IO.FileInfo(filepath);
                        CheckAndSaveImage(picForCheck, file);
                    }
                }

                GetImagesList();
            }
        }

        private void CheckAndSaveImage(Image pictureForCheck, FileInfo file)
        {
            decimal sizeBytes = Math.Round(decimal.Parse(file.Length.ToString()), 0);

            decimal maxSizeBytes = Config.hCntMain.GetSettings("psof", 1000000);

            if (sizeBytes > maxSizeBytes)
            {
                MessageBox.Show("Размер файла ("
                        + sizeBytes.ToString()
                        + " байт) превышает \nдопустимый размер (" + maxSizeBytes.ToString()
                        + " байт) \nСохранение файла невозможно.", "Сохранение файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int picWidth = pictureForCheck.Width;
            int picHeight = pictureForCheck.Height;

            decimal maxPicWidth = Config.hCntMain.GetSettings("pxlx", 800);
            decimal maxPicHeight = Config.hCntMain.GetSettings("pxly", 600);

            if ((picHeight > maxPicHeight)
                    ||
                 (picWidth > maxPicWidth))
            {
                MessageBox.Show("Разрешение файла ("
                        + picWidth.ToString() + "x" + picHeight.ToString() + ") превышает"
                        + "\nдопустимый размер ("
                        + maxPicWidth.ToString() + "x" + maxPicHeight.ToString() + ")."
                        + "\nСохранение файла невозможно.", "Сохранение файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                
                return;
            }

            byte[] pic = IC.ImageToByteArray(pictureForCheck);

            int id_CheckScan = Config.hCntMain.AddGoodsImage(id_tov, pic, file.Name);                
            
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                scanImg fImg = new scanImg();
                fImg.ShowDialog();
                Image Im = ImageClass.ByteArrayToImage(fImg.img_array);
                string filName = "";
                
                if (Im != null)
                {
                    frmAddFileName frmFileName = new frmAddFileName();
                    frmFileName.ShowDialog();
                    filName = frmFileName.ReturnedValue.Trim();

                    if (filName.Length > 0)
                    {
                        string path = ImagePath + @"\" + filName + ".jpg";
                        SaveTempImage(path, Im);

                        Image picForCheck = Image.FromFile(path);

                        if (picForCheck != null)
                        {
                            System.IO.FileInfo file = new System.IO.FileInfo(path);
                            CheckAndSaveImage(picForCheck, file);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сканирования! \n\n"
                    + "Системное сообщение: \n"
                    + ex.Message);
            }

						GetImagesList();
        }

        private void dgImages_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((dgImages.Rows.Count != 0) && (e.RowIndex >= 0))
            {
                int idImage = int.Parse(dgImages.CurrentRow.Cells["id"].Value.ToString());
                bool defaultIm = bool.Parse(dgImages.CurrentRow.Cells["V"].Value.ToString());

                //если текущее изображение уже не дефолтное
                if (!defaultIm)
                {
                    DialogResult d = MessageBox.Show("Установить выбранное изображение изображением по умолчанию?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (d == DialogResult.Yes)
                    {
                        Config.hCntMain.SetDefaultImage(id_tov, idImage);
                        GetImagesList();
                    }
                }
            }
        }

        private void dgImages_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            Color rowColow = Color.White;

            dgImages.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;

            bool defaultIm = false;

            try
            {
                defaultIm = (dgImages.Rows[e.RowIndex].Cells["V"].Value.ToString() == "True") ? true : false;
            }
            catch { }

            if (defaultIm)
            {
                rowColow = pnDefault.BackColor;
                dgImages.Rows[e.RowIndex].DefaultCellStyle.BackColor = pnDefault.BackColor;
            }

            dgImages.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                dgImages.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rowColow;
        }

        private void dgImages_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //Рисуем рамку для выделеной строки
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
                Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

                ControlPaint.DrawBorder(e.Graphics, rect,
                    Color.FromKnownColor(KnownColor.Black), 2, ButtonBorderStyle.Solid,
                    Color.FromKnownColor(KnownColor.Black), 2, ButtonBorderStyle.Solid,
                    Color.FromKnownColor(KnownColor.Black), 2, ButtonBorderStyle.Solid,
                    Color.FromKnownColor(KnownColor.Black), 2, ButtonBorderStyle.Solid);
            }
        }

    }
}
