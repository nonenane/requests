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
    public partial class frmFilter : Form
    {

        /// <summary>
        /// 0 - ТУ, 1 - Инв, 2 - Подгруппы, 3 - Менеджеры
        /// </summary>
        int type;
        int id_dep;
        string idVal = "";
        DataTable dtData;
        DataTable dtInitialData;

        public string SelectedIdList = "";

        /// <summary>
        ////Процедура инициализации формы для фильтрации формы Справочник товаров
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_dtData"></param>
        /// <param name="_id_dep"></param>
        public frmFilter(int _type, DataTable _dtData, int _id_dep)
        {
            InitializeComponent();
            type = _type;
            dtData = _dtData.Copy();
            id_dep = _id_dep;
        }

        #region Methods

        private void FilterGrid()
        {
            string filter = "";
            filter += tbSearch.Text.Trim().Length != 0 ? "cname LIKE '%" + tbSearch.Text.Trim() + "%'" : "";

            dtData.DefaultView.RowFilter = filter;            
        }

        private void SaveSettings()
        {
            this.DialogResult = DialogResult.None;
            foreach (DataRow dRow in dtData.Rows)
            {
                if ((bool)dRow["checked"])
                {
                    Config.hCntMain.SaveFilterSettings(idVal, id_dep, (int)dRow["id"], false);
                    SelectedIdList += dRow["id"].ToString() + ",";
                }
                else
                {
                    Config.hCntMain.SaveFilterSettings(idVal, id_dep, (int)dRow["id"], true);
                }
            }

            SelectedIdList = SelectedIdList.Length > 0 ? SelectedIdList.Substring(0, SelectedIdList.Length - 1) : "";

            if (dtData.Select("checked = True").Count() > 0)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                if (valuesChanged())
                {
                    this.DialogResult = DialogResult.No;
                }
            }

            this.Close();
        }

        private bool valuesChanged()
        {
            bool result = false;
            string currentSort = "";

            if (dtInitialData != null
                && dtData != null
                && dtInitialData.Columns.Count != 0
                && dtData.Columns.Count != 0)
            {
                dtData.AcceptChanges();
                if (dtInitialData.Rows.Count != dtData.Rows.Count
                    || dtInitialData.Columns.Count != dtData.Columns.Count)
                {
                    result = true;
                }
                else
                {
                    currentSort = dtData.DefaultView.Sort;
                    dtData.DefaultView.Sort = dtData.Columns[0].ColumnName + " ASC";
                    dtInitialData.DefaultView.Sort = dtInitialData.Columns[0].ColumnName + " ASC";

                    for (int i = 0; i < dtInitialData.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtInitialData.Columns.Count; j++)
                        {
                            if (dtData.Columns[j].DataType != dtInitialData.Columns[j].DataType
                                || !dtData.Rows[i][j].Equals(dtInitialData.Rows[i][j]))
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }

            dtData.DefaultView.Sort = currentSort;
            return result;
        }

        #endregion

        #region Events

        private void btExit_Click(object sender, EventArgs e)
        {
            if (valuesChanged()
                && MessageBox.Show("Закрыть форму без сохранения данных?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            
            this.Close();
        }

        private void frmFilter_Load(object sender, EventArgs e)
        {
            DataTable dtSettings;
            DataRow[] rowsToDel;
            rowsToDel = dtData.Select("id <= 0");
            if (rowsToDel.Count() > 0)
            {
                for (int i = 0; i < rowsToDel.Count(); i++)
                {
                    dtData.Rows.Remove(rowsToDel[i]);
                }
            }

            dtData.Columns.Add("checked", typeof(bool));

            idVal = (type == 0 ? "tugr" : (type == 1 ? "invg" : (type == 2 ? "subg" : "mngr")));

            dtSettings = Config.hCntMain.GetFilterSettings(idVal, id_dep);

            foreach (DataRow dRow in dtData.Rows)
            {
                try
                {
                    dRow["checked"] = dtSettings.Select("value = " + dRow["id"].ToString()).Count() > 0;
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Невозможно получить настройки фильтра.\nОбратитесь в ОЭЭС.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.Close();
                    return;
                }
            }

            dtInitialData = dtData.Copy();
            dgvFilter.AutoGenerateColumns = false;
            dgvFilter.DataSource = dtData;
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsControl(e.KeyChar) || char.IsLetterOrDigit(e.KeyChar) || ",./".Contains(e.KeyChar));
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }
        
        private void btSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }       
        
        #endregion
    }
}
