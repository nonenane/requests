using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ViewSalesPromProducts
{
    public partial class frmAdd : Form
    {
        public string ean { set; private get; }
        public decimal Price { set; private get; }
        public decimal SalePrice { set; private get; }

        private int id_tovar, id_deps, nds, ntypeorg, id_grp1;
        private bool isLoadData, isValidate;

        public frmAdd()
        {
            InitializeComponent();
        }

        private void frmAdd_Load(object sender, EventArgs e)
        {
            isLoadData = true;
            if (ean != null && ean.Length == 13)
            {
                tbEan.Text = ean;
                isValidate = false;
                tbEan.Enabled = false;
                //tbEan.Focus();
                findGood();
                tbRealPrice.Text = Price.ToString("0.00");
                tbDiscountPrice.Text = SalePrice.ToString("0.00");
                //btSave.Enabled = true;
            }
            else isValidate = true;
            isLoadData = false;
        }

        private void frmAdd_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
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

        private void tbEan_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != '\b';
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

        private void tbEan_TextChanged(object sender, EventArgs e)
        {
            if (isLoadData) return;

            TextBox tb = (sender as TextBox);

            if (tb.Text.Trim().Length == 13)
            {
                Int64 inInt;
                if (Int64.TryParse(tb.Text, out inInt))
                    SendKeys.Send("{ENTER}");
                else
                {
                    id_tovar = -1;
                    btSave.Enabled = false;
                    tb.Clear();
                    tbName.Clear();
                }
            }
        }

        private void tbEan_KeyDown(object sender, KeyEventArgs e)
        {
            if (isLoadData) return;

            if (e.KeyCode == Keys.Enter)
            {
                id_tovar = -1;
                btSave.Enabled = false;
                if (tbEan.Text.Trim().Length == 13)
                {
                    Int64 inInt;
                    if (!Int64.TryParse(tbEan.Text, out inInt))
                    {
                        tbName.Clear();
                        tbEan.Clear();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(Config.centralText("Необходимо ввести EAN для штучного товара\n"), "Получение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbName.Clear();
                    tbEan.Clear();
                    return;
                }

                findGood();

            }
        }

        private void findGood()
        {

            DataTable dtTovar = Config.connectMain.getGoodToCatalog(tbEan.Text.Trim(), isValidate);

            if (dtTovar == null || dtTovar.Rows.Count == 0)
            {
                MessageBox.Show(Config.centralText("При получение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Получение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbName.Clear();
                tbEan.Clear();
                return;
            }

            if ((int)dtTovar.Rows[0]["id"] == -1)
            {
                MessageBox.Show(Config.centralText($"{dtTovar.Rows[0]["msg"].ToString().Replace("\\n", "\n")}\n"), "Получение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbName.Clear();
                tbEan.Clear();
                return;
            }

            tbName.Text = dtTovar.Rows[0]["cName"].ToString();
            id_tovar = (int)dtTovar.Rows[0]["id_tovar"];
            ntypeorg = (int)dtTovar.Rows[0]["ntypeorg"];
            id_deps = (int)dtTovar.Rows[0]["id_deps"];
            nds = (int)dtTovar.Rows[0]["nds"];
            id_grp1 = (int)dtTovar.Rows[0]["id_grp1"];
            btSave.Enabled = true;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            decimal price, discountPrice;

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

            if (!decimal.TryParse(tbDiscountPrice.Text, out discountPrice))
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{label4.Text}\"\n"), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                tbDiscountPrice.Focus();
                return;
            }

            if (discountPrice == 0)
            {
                MessageBox.Show(Config.centralText($"\"{label4.Text}\" должно быть больше 0\n"), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbRealPrice.Focus();
                return;
            }

            if (discountPrice > price)
            {
                MessageBox.Show(Config.centralText($"Акционная цена должна быть\nниже цены без ограничений\n"), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (id_tovar == -1)
            {
                MessageBox.Show(Config.centralText($"Необходимо выбрать товар\n"), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DialogResult.No == MessageBox.Show(Config.centralText($"Цена будет отправлена на кассы.\nПродолжить?\n"), "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                return;

            DataTable dtResult = Config.connectMain.setCatalogPromotionalTovars(id_tovar, price, discountPrice, false);

            dtResult = Config.connectMainKassRealiz.setGoodsUpdate(id_deps, nds, id_grp1, ntypeorg, decimal.ToInt32((price * 100)), tbName.Text.Trim(), tbEan.Text.Trim());

            MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }
    }
}
