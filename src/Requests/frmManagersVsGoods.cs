using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Settings.User;

namespace Requests
{
    public partial class frmManagersVsGoods : Form
    {
        public frmManagersVsGoods()
        {
            InitializeComponent();
        }

        DataTable dtGoods;
        DataTable dtManGoods;

        #region Methods

        /// <summary>
        /// Получение данных для комбика с менеджерами
        /// </summary>
        private void GetManagers()
        {
            if (!DepSelectedCorrectly())
            {
                return;
            }

            if (UserSettings.User.StatusCode != "МН" /*&& UserSettings.User.StatusCode != "ДМН"*/)
            {
                int id_dep = (int)cbDepartment.SelectedValue;
                
                DataTable dtManagers;
                if (id_dep == 6)
                    dtManagers = Config.hCntAdd.GetManagers(6);
                else
                {
                    dtManagers = Config.hCntMain.GetManagers(id_dep);
                }

                DataRow[] drMans = dtManagers.Select("id_Access < 1");

                foreach (DataRow drMn in drMans)
                {
                    dtManagers.Rows.Remove(drMn);
                }

                cbManager.DataSource = dtManagers;
                cbManager.DisplayMember = "FIO";
                cbManager.ValueMember = "id_Access";

                cbManager.SelectedIndex = 0;
                
            }
            //else
            //{
            //    lbManager.Visible =
            //        cbManager = false;
            //}

            cbManager.SelectedValueChanged += cbManager_SelectedValueChanged;
        }

        private void GetTUGrps()
        {
            if (!DepSelectedCorrectly()
                || !ManagerSelectedCorrectly())
            {
                return;
            }

            int id_dep = (int)cbDepartment.SelectedValue;

            DataTable dtTUMain = (id_dep != 6 ? Config.hCntMain.GetTUForManager(id_dep, (int)cbManager.SelectedValue) 
                                                : Config.hCntAdd.GetTUForManager(id_dep, (int)cbManager.SelectedValue));            
            cbTU.DataSource = dtTUMain;
            cbTU.DisplayMember = "cname";
            cbTU.ValueMember = "id";

            cbTU.SelectedValue = 0;
        }

        private void GetInvGrps()
        {
            if (!DepSelectedCorrectly()
                || !ManagerSelectedCorrectly())
            {
                return;
            }

            DataTable dtInvGrps;
            int id_dep = (int)cbDepartment.SelectedValue;

            dtInvGrps = (id_dep == 6 ? Config.hCntAdd.GetInvGrpForManager(id_dep, (int)cbManager.SelectedValue) 
                                        : Config.hCntMain.GetInvGrpForManager(id_dep, (int)cbManager.SelectedValue));
            
            cbInv.DataSource = dtInvGrps;
            cbInv.DisplayMember = "cname";
            cbInv.ValueMember = "id";

            cbInv.SelectedValue = 0;
        }

        private void GetDeps()
        {
            DataTable deps = Config.hCntMain.GetDeps();

            cbDepartment.DataSource = deps;
            cbDepartment.DisplayMember = "name";
            cbDepartment.ValueMember = "id";

            if (UserSettings.User.StatusCode == "КД"
                || UserSettings.User.StatusCode == "КНТ"
                || UserSettings.User.StatusCode == "ПР")
            {
                cbDepartment.SelectedIndex = 0;
            }

            if (UserSettings.User.Status == "МН"
                || UserSettings.User.Status == "РКВ")
            {
                cbDepartment.SelectedValue = UserSettings.User.Id;
            }

            cbDepartment.SelectedValueChanged += new EventHandler(cbDep_SelectedValueChanged);
        }

        private bool DepSelectedCorrectly()
        {
            return cbDepartment.SelectedValue is int;
        }

        private bool ManagerSelectedCorrectly()
        {
            return cbManager.SelectedValue is int;
        }

        private bool FiltersSelectedCorrectly()
        {
            return cbManager.SelectedValue is int
                    && cbDepartment.SelectedValue is int
                    && cbTU.SelectedValue is int
                    && cbInv.SelectedValue is int;
        }

        private void GetGoods()
        {
            if (FiltersSelectedCorrectly())
            {
                if ((int)cbDepartment.SelectedValue != 6)
                {
                    dtGoods = Config.hCntMain.GetGoodsForManager((int)cbManager.SelectedValue,
                                                                (int)cbDepartment.SelectedValue,
                                                                (int)cbTU.SelectedValue,
                                                                (int)cbInv.SelectedValue,
                                                                false);
                }
                else
                {
                    dtGoods = Config.hCntAdd.GetGoodsForManager((int)cbManager.SelectedValue,
                                                                (int)cbDepartment.SelectedValue,
                                                                (int)cbTU.SelectedValue,
                                                                (int)cbInv.SelectedValue,
                                                                false);
                }

                if (dtGoods != null)
                {
                    dgvGoods.AutoGenerateColumns = false;
                    dgvGoods.DataSource = dtGoods;

                    SetFilter();
                }
                else
                {
                    dgvGoods.DataSource = null;
                }
            }
        }

        private void GetManagerGoods()
        {
            if (FiltersSelectedCorrectly())
            {
                if ((int)cbDepartment.SelectedValue != 6)
                {
                    dtManGoods = Config.hCntMain.GetGoodsForManager((int)cbManager.SelectedValue,
                                                                    (int)cbDepartment.SelectedValue,
                                                                    (int)cbTU.SelectedValue,
                                                                    (int)cbInv.SelectedValue,
                                                                    true);
                }
                else
                {
                    dtManGoods = Config.hCntAdd.GetGoodsForManager((int)cbManager.SelectedValue,
                                                                    (int)cbDepartment.SelectedValue,
                                                                    (int)cbTU.SelectedValue,
                                                                    (int)cbInv.SelectedValue,
                                                                    true);
                }

                if (dtManGoods != null)
                {
                    dgvManagerGoods.AutoGenerateColumns = false;
                    dgvManagerGoods.DataSource = dtManGoods;

                    SetFilter();
                }
                else
                {
                    dgvManagerGoods.DataSource = null;
                }
            }
        }

        private void SetManagerGoods(int idTovar, bool isDelete)
        {
            if (FiltersSelectedCorrectly())
            {
                if ((int)cbDepartment.SelectedValue != 6)
                {
                    Config.hCntMain.SetManagerGoods((int)cbManager.SelectedValue,
                                                        idTovar,
                                                        (int)cbDepartment.SelectedValue,
                                                        (int)cbTU.SelectedValue,
                                                        (int)cbInv.SelectedValue,
                                                        isDelete);
                }
                else
                {
                    Config.hCntAdd.SetManagerGoods((int)cbManager.SelectedValue,
                                                        idTovar,
                                                        (int)cbDepartment.SelectedValue,
                                                        (int)cbTU.SelectedValue,
                                                        (int)cbInv.SelectedValue,
                                                        isDelete);
                }

                GetGoods();
                GetManagerGoods();
            }
        }

        private void SetFilter()
        {
            string filter = "";

            if (dtGoods != null)
            {
                filter = "ean LIKE '%" + tbEan.Text.Trim() + "%'";
                filter += tbCname.Text.Trim().Length == 0 ? "" : (filter.Length > 0 ? " AND " : "") + "cname LIKE '%" + tbCname.Text.Trim() + "%'";

                dtGoods.DefaultView.RowFilter = filter;
            }

            if (dtManGoods != null)
            {
                filter = "ean LIKE '%" + tbEanMan.Text.Trim() + "%'";
                filter += tbCnameMan.Text.Trim().Length == 0 ? "" : (filter.Length > 0 ? " AND " : "") + "cname LIKE '%" + tbCnameMan.Text.Trim() + "%'";

                dtManGoods.DefaultView.RowFilter = filter;
            }
        }

        #endregion

        #region Events

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManagersVsGoods_Load(object sender, EventArgs e)
        {
            GetDeps();
            GetManagers();
            GetTUGrps();
            GetInvGrps();
            GetGoods();
            GetManagerGoods();
           
            btAdd.Enabled =
                btAddAll.Enabled =
                btDelete.Enabled =
                btDeleteAll.Enabled = UserSettings.User.StatusCode != "ПР";

        }

        private void cbDep_SelectedValueChanged(object sender, EventArgs e)
        {
            GetManagers();
            GetGoods();
            GetManagerGoods();
        }

        private void cbManager_SelectedValueChanged(object sender, EventArgs e)
        {
            GetTUGrps();
            GetInvGrps();
        }

        private void btAddAll_Click(object sender, EventArgs e)
        {
            SetManagerGoods(0, false);
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (dgvGoods.CurrentRow != null
                && dgvGoods.CurrentRow.Cells["id_tovar"].Value is int
                && (int)dgvGoods.CurrentRow.Cells["id_tovar"].Value != 0)
            {
                SetManagerGoods((int)dgvGoods.CurrentRow.Cells["id_tovar"].Value, false);
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (dgvManagerGoods.CurrentRow != null
               && dgvManagerGoods.CurrentRow.Cells["id_tovarMan"].Value is int
               && (int)dgvManagerGoods.CurrentRow.Cells["id_tovarMan"].Value != 0)
            {
                SetManagerGoods((int)dgvManagerGoods.CurrentRow.Cells["id_tovarMan"].Value, true);
            }
        }

        private void btDeleteAll_Click(object sender, EventArgs e)
        {
            SetManagerGoods(0, true);
        }
        
        private void tbEan_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar);
        }

        private void tbCname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsLetterOrDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && !"\"./\\ ".Contains(e.KeyChar);
        }

        private void tbEan_TextChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void cbManager_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetTUGrps();
            GetInvGrps();
            GetGoods();
            GetManagerGoods();
        } 

        private void cbTU_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbInv.SelectedValue = 0;
            GetGoods();
            GetManagerGoods();
        }

        private void cbInv_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbTU.SelectedValue = 0;
            GetGoods();
            GetManagerGoods();
        } 
       
        #endregion
    }
}
