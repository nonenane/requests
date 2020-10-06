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
    public partial class frmChooseStatus : Form
    {
        public frmChooseStatus()
        {
            InitializeComponent();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        /// <summary>
        /// Сохранение настроек фильтра
        /// </summary>
        private void SaveSettings()
        {
            //заполняем строку фильтра
            Config.statusesFiltr = "";

            foreach (Control con in this.Controls)
            {
                if (con is CheckBox && ((CheckBox)con).Checked)
                {
                    Config.statusesFiltr += (Config.statusesFiltr.Length == 0 ? "" : ", ") + con.Tag.ToString();
                }
            }

            if (Config.statusesFiltr.Length == 0)
            {
                MessageBox.Show("Для сохранения необходимо выбрать\n хотя бы один статус", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //сохраняем на сервере
            Config.hCntMain.SetFilterSettings(true, Config.statusesFiltr);
            this.Close();
        }

        private void frmChooseStatus_Load(object sender, EventArgs e)
        {
            GetStatuses();
            if (Nwuram.Framework.Settings.User.UserSettings.User.StatusCode == "МН")
            {
                chkMan.Visible = true;
            }
        }

        /// <summary>
        /// Получение сохраненных настроек
        /// </summary>
        private void GetStatuses()
        {
            foreach (Control con in this.Controls)
            {
                if (con is CheckBox && (Config.statusesFiltr.Replace(" ", "").Split(',').Contains<string>(con.Tag.ToString()) || Config.statusesFiltr.Length == 0))
                {
                    ((CheckBox)con).Checked = true;
                }
            }
        }
    }

}
