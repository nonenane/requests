using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ViewSalesPromProducts
{
    public partial class frmDel : Form
    {
        public string ean { set; private get; }
        public frmDel()
        {
            InitializeComponent();
        }

        private void tbRealPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.ToString().Contains(e.KeyChar) || (sender as TextBox).Text.ToString().Length == 0))
            {
                e.Handled = true;
            }
            else
                if ((!Char.IsNumber(e.KeyChar) && (e.KeyChar != ',')))
            {
                if (e.KeyChar != '\b')
                { e.Handled = true; }
            }
        }

        private void frmDel_Load(object sender, EventArgs e)
        {

        }

        private void frmDel_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void tbRealPrice_Leave(object sender, EventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb.Text.Trim().Length == 0)
            { tb.Text = "0,00"; return; }

            decimal price;
            if (!decimal.TryParse(tb.Text, out price))
            {
                tb.Text = "0,00"; return;
            }

            tb.Text = price.ToString("0.00");
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            decimal price;

            if (!decimal.TryParse(tbRealPrice.Text, out price))
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{label3.Text}\"\n"), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbRealPrice.Focus();
                return;
            }

            if (price == 0)
            {
                MessageBox.Show(Config.centralText($"\"{label3.Text}\" должно быть больше 0\n"), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbRealPrice.Focus();
                return;
            }


            DataTable dtTovar = Config.connectMain.getGoodToCatalog(ean.Trim(), false);

            if (dtTovar == null || dtTovar.Rows.Count == 0)
            {
                MessageBox.Show(Config.centralText("При получение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Получение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((int)dtTovar.Rows[0]["id"] == -1)
            {
                MessageBox.Show(Config.centralText($"{dtTovar.Rows[0]["msg"].ToString().Replace("\\n", "\n")}\n"), "Получение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int id_tovar = (int)dtTovar.Rows[0]["id_tovar"];
            int ntypeorg = (int)dtTovar.Rows[0]["ntypeorg"];            
            int id_deps = (int)dtTovar.Rows[0]["id_deps"];
            int nds = (int)dtTovar.Rows[0]["nds"];
            int id_grp1 = (int)dtTovar.Rows[0]["id_grp1"];
            string cName = dtTovar.Rows[0]["cName"].ToString();


            if (DialogResult.No == MessageBox.Show(Config.centralText($"Цена будет отправлена на кассы.\nПродолжить?\n"), "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                return;

            DataTable dtResult = Config.connectMain.setCatalogPromotionalTovars(id_tovar, price, price, true);

            dtResult = Config.connectMainKassRealiz.setGoodsUpdate(id_deps, nds, id_grp1, ntypeorg, decimal.ToInt32((price * 100)), cName.Trim(), ean.Trim());

            MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show(Config.centralText($"Прервать удаление акционного товара?\n"), "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) Close();

        }
    }
}
