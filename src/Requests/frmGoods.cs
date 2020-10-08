using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.ToExcel;
using IC = Nwuram.Framework.scan.ImageClass;

using Nwuram.Framework.Logging;

namespace Requests
{
    public partial class frmGoods : Form
    {
        
        DataTable deps;
        private static DataTable dtGoods, dtGoods2;
        BindingSource bsGoods = new BindingSource();
        Color rowColor; //Цвет строки грида
        DataRow curRow;
        int idPost = 0;
        int idPost2 = 0;
        int selColumn = -1;
        string SortString = "";
        string direction = "DESC";
        bool isWeInOut = false;

        Task task_GetGoodsFromShop2 = null;
       
        public frmGoods()
        {
            InitializeComponent();
        }

        private void frmGoods_Load(object sender, EventArgs e)
        {
            if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
                && Config.linkToCurrentRequest != null
                && Config.dtProperties != null
                && Config.dtProperties.Rows.Count > 0
                && Config.dtProperties.Select("id_val = 'use samePost' AND TRIM(val) = '1'").Count() > 0)
            {
                string postName = "";
                Config.linkToCurrentRequest.GetPost(ref idPost, ref postName);
                if (postName.Trim().Length != 0)
                {
                    tbSupplier.Text = postName.Trim();
                }
            }

            pMatrix.Visible = chbMatrix.Visible = new List<string>(new string[] { "РКВ", "КД" }).Contains(UserSettings.User.StatusCode);
            tsmiAddGoodMatrix.Visible = tsmiDelGoodMatrix.Visible = new List<string>(new string[] { "РКВ", "КД" }).Contains(UserSettings.User.StatusCode);

            //Console.WriteLine(new List<string>(new string[] { "РКВ", "КД" }).Contains(UserSettings.User.StatusCode));

            GetDeps();
            GetTUGrps();
            GetInvGrps();
            GetSubGrp();
            GetManagers();
            ostOnDate.HeaderText = "Остаток на " + Config.curDate.ToString("dd.MM.yyyy");
            ostOnline.HeaderText = "Остаток on-line на " + Config.curDate.ToString("dd.MM.yyyy");
            ostOnDate2.HeaderText = "Остаток на " + Config.curDate.ToString("dd.MM.yyyy") + "(М2)";
            ostOnline2.HeaderText = "Остаток on-line на " + Config.curDate.ToString("dd.MM.yyyy") + "(М2)";
            SetControlsAvalible();
            SetControlsEnabled();
            //SetColumnsVisible();

            GetGoods();
            Config.SetDoubleBuffered(grdGoods);           
        }

        private void GetDeps()
        {
            deps = Config.hCntMain.GetDeps();
            cbDep.DataSource = deps;
            cbDep.DisplayMember = "name";
            cbDep.ValueMember = "id";
            
            if (UserSettings.User.StatusCode == "КД"
                || UserSettings.User.StatusCode == "КНТ"
                || UserSettings.User.StatusCode == "ПР")
            {
                cbDep.SelectedIndex = 0;
            }
            
            cbDep.SelectedValueChanged += new EventHandler(cbDep_SelectedValueChanged);
        }

        private void GetTUGrps()
        {
            int selectedDep;
            if(int.TryParse(cbDep.SelectedValue.ToString(), out selectedDep))
            {
                DataTable dtTUMain;
                if (selectedDep == 0)
                {
                    dtTUMain = Config.hCntMain.GetTU(0, true);
                    if (deps.Select("id = 6").Count() > 0)
                    {
                        dtTUMain = DataTableExtensions.CopyToDataTable(dtTUMain.AsEnumerable().Union(Config.hCntAdd.GetTU(6).AsEnumerable(), new comparerTU()));
                    }
                }
                else
                {
                    dtTUMain = (selectedDep != 6 ? Config.hCntMain.GetTU(selectedDep, true) : Config.hCntAdd.GetTU(selectedDep, true));
                }
                
                cbTU.DataSource = dtTUMain;
                cbTU.DisplayMember = "cname";
                cbTU.ValueMember = "id";

                cbTU.SelectedValue = isFilterSettingsExists("tugr") ? -1 : 0;
            }
        }

        private void GetInvGrps()
        {
            DataTable dtInvGrps;
            int depTU = ((DataRowView)cbTU.SelectedItem).Row.Field<int>("id_otdel");
            int idTU = ((DataRowView)cbTU.SelectedItem).Row.Field<int>("id");

            if (depTU == 0)
            {
                depTU = (int)cbDep.SelectedValue;
            }

            if (depTU == 0)
            {
                dtInvGrps = Config.hCntMain.GetInvGrp(depTU, true);
                if (deps.Select("id = 6").Count() > 0)
                {
                    dtInvGrps = DataTableExtensions.CopyToDataTable(dtInvGrps.AsEnumerable().Union(Config.hCntAdd.GetInvGrp(6, true).AsEnumerable(), new comparerTU()));
                }
            }
            else
            {
                dtInvGrps = (depTU == 6 ? Config.hCntAdd.GetInvGrp(depTU, true) : Config.hCntMain.GetInvGrp(depTU, true));
            }

            cbInv.DataSource = dtInvGrps;
            cbInv.DisplayMember = "cname";
            cbInv.ValueMember = "id";

            cbInv.SelectedValue = cbTU.SelectedValue is int 
                                    && (int)cbTU.SelectedValue != -1
                                    && isFilterSettingsExists("invg") ? -1 : 0;
        }

        private void GetSubGrp()
        {
            
            int selectedDep;
            if (int.TryParse(cbDep.SelectedValue.ToString(), out selectedDep))
            {
                DataTable dtSubGrp;
                if (selectedDep == 0)
                {
                    dtSubGrp = Config.hCntMain.GetSubGrp(0, true);
                    if (deps.Select("id = 6").Count() > 0)
                    {
                        dtSubGrp = DataTableExtensions.CopyToDataTable(dtSubGrp.AsEnumerable().Union(Config.hCntAdd.GetSubGrp(6, true).AsEnumerable(), new comparerTU()));
                    }
                }
                else
                {
                    dtSubGrp = (selectedDep != 6 ? Config.hCntMain.GetSubGrp(selectedDep, true) : Config.hCntAdd.GetSubGrp(selectedDep, true));
                }
                
                cbSub.DataSource = dtSubGrp;
                cbSub.DisplayMember = "cname";
                cbSub.ValueMember = "id";

                cbSub.SelectedValue = cbTU.SelectedValue is int
                                        && cbInv.SelectedValue is int
                                        && (int)cbTU.SelectedValue != -1
                                        && (int)cbInv.SelectedValue != -1
                                        && isFilterSettingsExists("subg") ? -1 : 0;
            }
        }

        private void GetGoods()
        {
            SetColumnsVisible();
            Config.ChangeFormEnabled(this, false);
            pbGoods.Visible = true;
            selColumn = -1;

            viewDataWeInOut();
            int tovarPost = idPost;
            if (isWeInOut)
            {
                tovarPost = idPost2;
            }
            else
            {
                idPost2 = 0;
                tbSupplier2.Text = "";
            }
                if (!bwGoods.IsBusy)
                {
                    object[] args = new object[6] { (int)cbDep.SelectedValue, 
                                                (int)cbTU.SelectedValue, 
                                                (int)cbInv.SelectedValue, 
                                                (int)cbSub.SelectedValue, 
                                                tovarPost, 
                                                (UserSettings.User.StatusCode == "МН" ? 0 : (int)cbManager.SelectedValue) };
                    bwGoods.RunWorkerAsync(args);
                }
        }

        private void SetControlsAvalible()
        {
            btSelectAll.Visible = (UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН");
            tsAddToProh.Visible = (UserSettings.User.StatusCode == "КД" || UserSettings.User.StatusCode == "РКВ");
            tsCreateSale.Visible = (UserSettings.User.StatusCode == "КД") ;
            btCreatePereoc.Visible = (UserSettings.User.StatusCode != "КНТ" && UserSettings.User.StatusCode != "ПР");
        }

        private void SetControlsEnabled()
        {
            btExcel.Enabled = 
                btSelectAll.Enabled =
                btImages.Enabled = 
                (dtGoods != null && dtGoods.DefaultView.Count > 0);

            bool hasChoosenRows = false;
//btCreatePereoc.Enabled = (dtGoods.Select("choose = true").Count() > 0);
            if (dtGoods != null && dtGoods.Rows.Count > 0)
                for (int i = 0; i < dtGoods.Rows.Count; i++)
                {
                    if ((bool)dtGoods.Rows[i]["choose"] == true)
                    {
                        hasChoosenRows = true;
                        break;
                    }
                }
            btCreatePereoc.Enabled = hasChoosenRows;
                //(dtGoods != null && dtGoods.Rows.Count > 0 && dtGoods.DefaultView.ToTable().Select("choose = true").Count() > 0);
            if (UserSettings.User.StatusCode == "МН"
                || UserSettings.User.StatusCode == "ДМН"
               || UserSettings.User.StatusCode == "РКВ")
            {
                cbDep.Enabled = false;
                cbDep.SelectedValue = UserSettings.User.IdDepartment;
            }

            chbReqGoods.Visible = ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
                                    && Config.linkToCurrentRequest != null
                                    && Config.linkToCurrentRequest.getGoodsData() != null);
        }

        private void CalculZakaz()
        {
            if(dtGoods != null)
            {
                tbResultAvgReal.Text = dtGoods.DefaultView.ToTable().Select().Sum(rrr => rrr.Field<decimal>("avgReal")).ToString("0.000");
            }
        }

        private void SetFilter()
        {
            string filter = "";

            filter += (tbEAN.Text.Trim().Length > 0 ? "ean like '%" + tbEAN.Text.Trim() + "%'" : "");
            filter += (tbGoodName.Text.Trim().Length > 0 ? (filter.Trim().Length > 0 ? " AND " : "") + "cName like '%" + tbGoodName.Text.Trim() + "%'" : "");
            filter += (chbCat.Checked ? (filter.Trim().Length > 0 ? " AND " : "") + "isCatGood = true" 
                                      : (Config.CheckProperty("id_val = 'show zeroStrings' AND val = '1'") ? ""
                                                            : (filter.Trim().Length > 0 ? " AND " : "") + "CatFullZero = 0"));
            filter += (chbReqGoods.Checked ? (filter.Trim().Length > 0 ? " AND " : "") + "id_tovar IN (" + Config.GetStringFromRow(Config.linkToCurrentRequest.getGoodsData(), "id_tovar") + ")" : "");

            if (chbMatrix.Checked)
                filter += (filter.Trim().Length > 0 ? " AND " : "") + " idMaxrixGood is not null";
            try
            {
                bsGoods.Filter = filter;
            }
            catch (EvaluateException)
            {
                MessageBox.Show("Некорректное значение фильтра!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            SetControlsEnabled();
            CalculZakaz();
            SetColumnsWidth();
        }

        private void CheckAllGoods()
        {
            if (dtGoods != null)
            {
                if (dtGoods.DefaultView.ToTable().Select("choose = true").Count() == 0)
                {
                    for(int i = 0; i < dtGoods.DefaultView.Count; i++)
                    {
                        dtGoods.DefaultView[i]["choose"] = true;
                    }
                }
                else
                {
                    for (int i = 0; i < dtGoods.DefaultView.Count; i++)
                    {
                        dtGoods.DefaultView[i]["choose"] = false;
                    }
                }

                dtGoods.AcceptChanges();
            }
        }

        private void ChooseAllGoods()
        {
            if ((int)cbTU.SelectedValue == 0
               && (int)cbInv.SelectedValue == 0
               && tbSupplier.Text.Length == 0)
            {
                MessageBox.Show("Все товары по выбранным условиям не могут быть добавлены\n"+
                                "в заявку!\n".PadLeft(50) + "Необходимо выбрать одну из групп товара или поставщика.".PadLeft(58), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int id_type;
                if (Config.linkToCurrentRequest == null || Config.linkToCurrentRequest.curIdType == 0)
                {

                    frmChooseType frmType = new frmChooseType();
                    frmType.ShowDialog();
                    id_type = frmType.choosedType;
                }
                else
                {
                    id_type = Config.linkToCurrentRequest.curIdType;
                }

               
                DataTable dtCurrentGoods = dtGoods.DefaultView.ToTable().Copy(); 
                if (id_type != 2 && id_type != 8)
                {
                    DataRow[] drProh = dtCurrentGoods.Select("isProh = true");
                    if (drProh.Count() > 0)
                    {
                        DataTable dtProhGoods = drProh.CopyToDataTable();
                        dtCurrentGoods = dtCurrentGoods.Select("isProh <> true").CopyToDataTable();

                        frmProhMessage frmProh = new frmProhMessage(dtProhGoods);
                        frmProh.ShowDialog();
                    }
                }

                if (dtCurrentGoods.Rows.Count != 0)
                {
                    if (Config.linkToCurrentRequest == null)
                    {
                        ((Main)this.MdiParent).SetTab("Создание заявки.", "frmEditRequest", new object[] { 1, 0, false }, 0);
                        if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
                            && Config.linkToCurrentRequest != null)
                            //&& Config.dtProperties != null
                            //&& Config.dtProperties.Rows.Count > 0
                            //&& Config.dtProperties.Select("id_val = 'use samePost' AND TRIM(val) = '1'").Count() > 0)
                        {
                            Config.linkToCurrentRequest.SetPost(this.idPost, tbSupplier.Text);
                        }
                        this.MdiParent.Refresh();
                    }

                    if (Config.linkToCurrentRequest.curIdType == 0)
                    {
                        Config.linkToCurrentRequest.curIdType = id_type;
                    }

                    Config.linkToCurrentRequest.AddGoodsMassive(dtCurrentGoods);
                    
                    ((Main)this.MdiParent).SetTab("", Config.linkToCurrentRequest.Tag.ToString(), null, null);
                    
                    grdGoods.Refresh();
                }
            }
        }

        private void CreateExcelReport()
        {
            Config.curDate = Config.hCntMain.GetCurDate(true);
            string cellformat;
            //HandmadeReport report = new HandmadeReport();
            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();
            DataTable dtRep = Config.GetDataTableFromGrid(grdGoods);
            DataTable dtGrdGoods = dtGoods.DefaultView.ToTable();
            if(dtRep.Columns.Contains("V"))
                dtRep.Columns.Remove(dtRep.Columns["V"]);
            //int posOfZcena = dtRep.Columns.IndexOf(dtRep.Columns[grdGoods.Columns["rcena"].HeaderText]);

            //DataTable dtZcena = (int)cbDep.SelectedValue != 6 ? Config.hCntMain.GetZcena(Config.GetStringFromRow(dtGrdGoods, "id_tovar"))
            //                                                  : Config.hCntAdd.GetZcena(Config.GetStringFromRow(dtGrdGoods, "id_tovar"));
            //DataColumn dcZcena = dtRep.Columns.Add("zcena", typeof(string));
            //dcZcena.SetOrdinal(posOfZcena);
            

            //dtRep.Rows[0]["zcena"] = "Цена з.";
            //DataRow[] drSelZcena;
            
            //for (int j = 0; j < dtGrdGoods.Rows.Count; j++)
            //{
            //    drSelZcena = dtZcena.Select("id = " + dtGrdGoods.Rows[j]["id_tovar"].ToString());

            //    if (drSelZcena.Count() != 0)
            //    {
            //        dtRep.Rows[j + 1]["zcena"] = drSelZcena[0]["zcena"].ToString();
            //    }
            //}

            int rowStart = 5;

            report.AddSingleValue("Список товаров", 1, 1);

            report.AddSingleValue("Выгрузил: " + UserSettings.User.FullUsername, 3, 1);
            report.AddSingleValue("Дата: " + Config.curDate, 3, 3);

            report.AddSingleValue("Отдел: " + cbDep.Text,5,1);

            if (idPost != 0 && tbSupplier.Text.Trim().Length != 0)
            {
                report.AddSingleValue("Поставщик: " + tbSupplier.Text.Trim(), 5, 3);
            }

            if ((int)cbTU.SelectedValue != 0)
            {
                report.AddSingleValue("Т/У группа: " + cbTU.Text, rowStart, 1);
                rowStart += 2;
            }

            if ((int)cbInv.SelectedValue != 0)
            {
                report.AddSingleValue("Инв. группа: " + cbInv.Text, rowStart, 1);
                rowStart += 2;
            }

            if ((int)cbSub.SelectedValue != 0)
            {
                report.AddSingleValue("Подгруппа: " + cbSub.Text, rowStart, 1);
                rowStart += 2;
            }

            report.AddMultiValue(dtRep, rowStart, 1);
            report.SetFontBold(1, 1, rowStart, dtRep.Columns.Count);
            report.SetRowHeight(rowStart, 1, rowStart, dtRep.Columns.Count, 40);

            int i = 1;
            int columnIndex = 0;
            foreach (DataGridViewColumn dgvCol in grdGoods.Columns)
            {
                columnIndex = Config.getExcelColIndex(grdGoods, dgvCol.Name);
                //if (columnIndex >= posOfZcena + 1)
                //    columnIndex++;
                if (dgvCol.Visible && !dgvCol.GetType().Equals(typeof(DataGridViewCheckBoxColumn)))
                {
                    report.SetColumnWidth(1, i, 1, i + 1, dgvCol.Width / 6);
                    i++;

                    if (dgvCol.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)
                    {
                        report.SetCellAlignmentToCenter(rowStart + 1, columnIndex, dtRep.Rows.Count + rowStart - 1, columnIndex);
                    }

                    if (dgvCol.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
                    {
                        report.SetCellAlignmentToRight(rowStart + 1, columnIndex, dtRep.Rows.Count + rowStart - 1, columnIndex);
                    }

                    if (dgvCol.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleLeft
                       || dgvCol.DefaultCellStyle.Alignment == DataGridViewContentAlignment.NotSet)
                    {
                        report.SetCellAlignmentToLeft(rowStart + 1, columnIndex, dtRep.Rows.Count + rowStart - 1, columnIndex);
                    }

                    cellformat = dgvCol.DefaultCellStyle.Format;
                    if (cellformat.Length != 0
                        && cellformat.Substring(0, 1) == "N")
                    {
                        report.SetFormat(rowStart + 1, columnIndex, dtRep.Rows.Count + rowStart - 1, columnIndex, "0,".PadRight(2 + int.Parse(cellformat.Substring(1, cellformat.Length - 1)), '0').Trim(','));
                    }
                }
            }

            //формат колонки зцена
            //report.SetFormat(rowStart + 1, posOfZcena + 1, dtRep.Rows.Count + rowStart - 1, posOfZcena + 1, "0,0000");
            //report.SetCellAlignmentToRight(rowStart + 1, posOfZcena + 1, dtRep.Rows.Count + rowStart - 1, posOfZcena + 1);

            report.SetCellAlignmentToCenter(rowStart, 1, rowStart, dtRep.Columns.Count);
            report.SetWrapText(rowStart, 1, rowStart, dtRep.Columns.Count);
            Config.setColumnFormat(report, rowStart, Config.getExcelColIndex(grdGoods, "ean"), dtRep.Rows.Count + rowStart, Config.getExcelColIndex(grdGoods, "ean"), "#############", grdGoods.Columns["ean"].Visible);
            report.SetBorders(rowStart, 1, dtRep.Rows.Count + rowStart - 1, dtRep.Columns.Count);
            report.Show();
            Config.curDate = Config.curDate.Date;
        }

        private void CreatePereoc()
        {
            bool isNewPereoc = false;
            DataTable dtZcena;
            DataTable dtSelectedGoods = dtGoods.Select("choose = true").CopyToDataTable().DefaultView.ToTable(false, new string[5] { "id_tovar", "cName", "ean", "ostOnDate", "rcena" });
                //dtGoods.Select("choose = true").CopyToDataTable().DefaultView.ToTable(false, new string[6] { "choose", "id_tovar", "cName", "ean", "ostOnDate", "rcena" });
                //.CopyToDataTable();
            //dtSelectedGoods.Columns.Remove("choose");

            if (Config.dtPereocGoods == null
                 && (Config.curPereocFrmName == null || Config.curPereocFrmName.Length == 0)
                //|| Config.dtPereocGoods.Rows.Count == 0
                )
            {
                isNewPereoc = true;
                Config.dtPereocGoods = dtSelectedGoods.Clone();
                Config.dtPereocGoods.Columns.Add("id_dep", typeof(int)).DefaultValue = (int)cbDep.SelectedValue;
                Config.dtPereocGoods.Columns.Add("zcena", typeof(decimal)).DefaultValue = 0;
                Config.dtPereocGoods.Columns.Add("zcenabnds", typeof(decimal)).DefaultValue = 0;
                Config.dtPereocGoods.Columns.Add("cprimech", typeof(int)).DefaultValue = 1;
                Config.dtPereocGoods.Columns.Add("idReq", typeof(int)).DefaultValue = 0;
                dtSelectedGoods.Select().CopyToDataTable(Config.dtPereocGoods, LoadOption.OverwriteChanges);
                //dtGoods.DefaultView.ToTable().Select("choose = true").CopyToDataTable(dtPereocGoods, LoadOption.OverwriteChanges);
                Config.curPereocFrmName = "frmPereoc";
            }
            else
            {
                Config.dtPereocGoods.Columns["id_dep"].DefaultValue = (int)cbDep.SelectedValue;
                Config.dtPereocGoods.Columns["zcena"].DefaultValue = 0;
                Config.dtPereocGoods.Columns["zcenabnds"].DefaultValue = 0;
                Config.dtPereocGoods.Columns["cprimech"].DefaultValue = 1;
                Config.dtPereocGoods.Columns["idReq"].DefaultValue = 0;
                dtSelectedGoods.Select("id_tovar not in(" + Config.GetStringFromRow(Config.dtPereocGoods, "id_tovar") + ")").CopyToDataTable(Config.dtPereocGoods, LoadOption.OverwriteChanges);
                //foreach (DataRow delRow in Config.dtPereocGoods.Select("id_tovar not in (" + Config.GetSelectedGoods(dtSelectedGoods) + ") AND id_dep = " + cbDep.SelectedValue.ToString()))
                //{
                //    Config.dtPereocGoods.Rows.Remove(delRow);
                //}                     
            }

            dtZcena = (int)cbDep.SelectedValue != 6 ? Config.hCntMain.GetZcena(Config.GetStringFromRow(Config.dtPereocGoods, "id_tovar"))
                                                    : Config.hCntAdd.GetZcena(Config.GetStringFromRow(Config.dtPereocGoods, "id_tovar"));

            foreach (DataRow dRow in Config.dtPereocGoods.Rows)
            {
                dRow["zcena"] = dtZcena.Select("id = " + dRow["id_tovar"])[0]["zcena"];
            }

            if (isNewPereoc)
            {
                ((Main)this.MdiParent).SetTab("Переоценка. Создание.", Config.curPereocFrmName, new object[] { 2, 0, (int)cbDep.SelectedValue }, -2);
            }
            else
            {
                ((Main)this.MdiParent).SetTab("", Config.curPereocFrmName, null, null);
            }
        }
        
        private void ChoosePereocGoods()
        {
            if (dtGoods != null)
            {
                foreach (DataRow row in dtGoods.Select("choose = true"))
                {
                    row["choose"] = false;
                }
            }

            if (Config.dtPereocGoods != null 
                //&& Config.dtPereocGoods.Rows.Count > 0
                && Config.curPereocFrmName != null && Config.curPereocFrmName.Length != 0)
            {
                if (grdGoods.DataSource == null || dtGoods == null /*|| dtGoods.Rows.Count == 0*/)
                {                    
                    GetGoods();
                }
                else
                {

                    //for (int i = 0; i < dtGoods.DefaultView.Count; i++)
                    //{
                    //    if (Config.dtPereocGoods.Select("id_tovar = " + dtGoods.DefaultView[i]["id_tovar"]).Count() != 0)
                    //    {
                    //        dtGoods.DefaultView[i]["choose"] = true;
                    //    }
                    //    else
                    //    {
                    //        dtGoods.DefaultView[i]["choose"] = false;
                    //    }
                    //}
                    foreach (DataRow row in dtGoods.Select("id_tovar in (" + Config.GetStringFromRow(Config.dtPereocGoods.Select(), "id_tovar") + ")"))
                    {
                        row["choose"] = true;
                    }
                    dtGoods.AcceptChanges();
                    grdGoods.Refresh();
                }
            }
            //else
            //{
            //    if (dtGoods != null)
            //    {
            //        foreach (DataRow row in dtGoods.Select("choose = true"))
            //        {
            //            row["choose"] = false;
            //        }
            //    }
            //}
        }

        private void SetColumnsVisible()
        {
            choose.Visible = (UserSettings.User.StatusCode != "КНТ" && UserSettings.User.StatusCode != "ПР");
            if (dtGoods != null && dtGoods.Rows.Count > 0)
            {
                avgRealWeek.Visible = dtGoods.Rows[0]["avgRealWeek"] != DBNull.Value && ((decimal)dtGoods.Rows[0]["avgRealWeek"] > -1 && Config.CheckProperty("id_val = 'srrd' AND val = '1'"));
                avgRealWeek2.Visible = dtGoods.Columns.Contains("avgRealWeek2") && dtGoods.Rows[0]["avgRealWeek2"] != DBNull.Value && ((decimal)dtGoods.Rows[0]["avgRealWeek2"] > -1 && Config.CheckProperty("id_val = 'srrd 2' AND val = '1'"));
            }
            else
            {
                avgRealWeek.Visible = Config.CheckProperty("id_val = 'srrd' AND val = '1'");
                avgRealWeek2.Visible = Config.CheckProperty("id_val = 'srrd 2' AND val = '1'");
            }
            ost.Visible = Config.CheckProperty("id_val = 'show ost' AND val = '1'");
            prihod.Visible = Config.CheckProperty("id_val = 'show prihod' AND val = '1'");
            rashod.Visible = Config.CheckProperty("id_val = 'show rashod' AND val = '1'");
            ostOnDate.Visible = Config.CheckProperty("id_val = 'show ostOnDate' AND val = '1'");
            ostOnline.Visible = 
                prihodOnline.Visible = Config.CheckProperty("id_val = 'on-line rests' AND val = '1'");
            avgReal.Visible = Config.CheckProperty("id_val = 'show avgReal' AND val = '1'");
            zapas.Visible = Config.CheckProperty("id_val = 'show zapas' AND val = '1'");
            rcena.Visible = Config.CheckProperty("id_val = 'show rcena' AND val = '1'");
            morningNacen.Visible = Config.CheckProperty("id_val = 'show extra' AND val = '1'");
            vozvr.Visible = Config.CheckProperty("id_val = 'show return' AND val = '1'");
            uVozvr.Visible = Config.CheckProperty("id_val = 'show returnU' AND val = '1'");
            zakaz.Visible = Config.CheckProperty("id_val = 'show zakaz' AND val = '1'");
            zakazUnconf.Visible = Config.CheckProperty("id_val = 'show zakazU' AND val = '1'");
            shelfSpace.Visible = Config.CheckProperty("id_val = 'show shelf space' AND val = '1'");
            zcena.Visible = Config.CheckProperty("id_val = 'show zcena' AND val = '1'");
            rn.Visible = Config.CheckProperty("id_val = 'show rn' AND val = '1'");
            OOVozvr.Visible = Config.CheckProperty("id_val = 'show oov' AND val = '1'");
            //shop 2 columns
            ost2.Visible = Config.CheckProperty("id_val = 'show ost 2' AND val = '1'");
            prihod2.Visible = Config.CheckProperty("id_val = 'show prihod 2' AND val = '1'");
            rashod2.Visible = Config.CheckProperty("id_val = 'show rashod 2' AND val = '1'");
            ostOnDate2.Visible = Config.CheckProperty("id_val = 'show ostOnDate 2' AND val = '1'");
            ostOnline2.Visible =
                prihodOnline2.Visible = Config.CheckProperty("id_val = 'on-line rests 2' AND val = '1'");
            avgReal2.Visible = Config.CheckProperty("id_val = 'show avgReal 2' AND val = '1'");
            zapas2.Visible = Config.CheckProperty("id_val = 'show zapas 2' AND val = '1'");
            rcena2.Visible = Config.CheckProperty("id_val = 'show rcena 2' AND val = '1'");
            morningNacen2.Visible = Config.CheckProperty("id_val = 'show extra 2' AND val = '1'");
            vozvr2.Visible = Config.CheckProperty("id_val = 'show return 2' AND val = '1'");
            uVozvr2.Visible = Config.CheckProperty("id_val = 'show returnU 2' AND val = '1'");
            zakaz2.Visible = Config.CheckProperty("id_val = 'show zakaz 2' AND val = '1'");
            zakazUnconf2.Visible = Config.CheckProperty("id_val = 'show zakazU 2' AND val = '1'");
            shelfSpace2.Visible = Config.CheckProperty("id_val = 'show shelfspace2' AND val = '1'");
            zcena2.Visible = Config.CheckProperty("id_val = 'show zcena 2' AND val = '1'");
            rn2.Visible = Config.CheckProperty("id_val = 'show rn 2' AND val = '1'");
            OOVozvr2.Visible = Config.CheckProperty("id_val = 'show oov 2' AND val = '1'");
            SetColumnsWidth(); 
        }

        private bool SomeColumnShop2Visible()
        {
            return Config.CheckProperty("(id_val = 'show ost 2' OR id_val = 'show prihod 2' OR id_val = 'show rashod 2' " +
                                          "OR id_val = 'show ostOnDate 2' OR id_val = 'on-line rests 2' OR id_val = 'show avgReal 2' " +
                                          "OR id_val = 'shop zapas 2' OR id_val = 'show rcena 2' OR id_val = 'show extra 2' " +
                                          "OR id_val = 'shop return 2' OR id_val = 'show returnU 2' OR id_val = 'zakaz 2' " +
                                          "OR id_val = 'shop zakazU 2' OR id_val = 'show shelfspace2' OR id_val = 'show zcena 2' " +
                                          "OR id_val = 'shop rn 2' OR id_val = 'show oov 2' OR id_val = 'show srrd 2') " + " AND val = '1'");
        }

        private void SetColumnsWidth()
        {
            int colWidth = 0;
            int colsCount = 0;
            int scrollWidth = 0;

            IEnumerable<VScrollBar> vScrollbarCol = grdGoods.Controls.OfType<VScrollBar>();
            if (vScrollbarCol.Count() != 0)
            {
                VScrollBar vScrollbar = vScrollbarCol.First();
                if (vScrollbar != null && vScrollbar.Visible)
                {
                    scrollWidth = SystemInformation.VerticalScrollBarWidth + 2;
                }
            }

            foreach (DataGridViewColumn grdCol in grdGoods.Columns)
            {
                if(grdCol.Visible)
                    colWidth += grdCol.Width;
            }

            foreach (DataGridViewColumn grdCol in grdGoods.Columns)
            {
                if (grdCol != ean
                    && grdCol != cname
                    && grdCol != choose
                    && grdCol.Visible)
                {
                    colsCount++;
                }
            }

            if (grdGoods.Width >= colWidth)
            {
                if (colsCount == 0)
                {
                    cname.Width = grdGoods.Width - (choose.Visible ? choose.Width : 0 ) - ean.Width - scrollWidth;
                }
                else
                {
                    colWidth = (int)((grdGoods.Width - ((choose.Visible ? choose.Width : 0) + ean.Width + cname.Width + scrollWidth)) / colsCount);

                    foreach (DataGridViewColumn grdCol in grdGoods.Columns)
                    {
                         if (grdCol != ean
                                && grdCol != cname
                                && grdCol != choose
                                && grdCol.Visible)
                            grdCol.Width = colWidth;
                    }

                }
            }            
        }

        private void cbDep_SelectedValueChanged(object sender, EventArgs e)
        {
            GetTUGrps();
            GetInvGrps();
            GetSubGrp();
            GetManagers();
            GetGoods();
        }

        private void tbEAN_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !"0123456789\b".Contains(e.KeyChar);
        }

        private void tbGoodName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = "!@#$%^*()_+-=;:?'{}[]|/\\<>".Contains(e.KeyChar);
        }

        private void cbTU_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbInv.SelectedIndex = 0;
            cbSub.SelectedIndex = 0;
            GetGoods();
        }

        private void cbInv_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbTU.SelectedIndex = 0;
            cbSub.SelectedIndex = 0;
            GetGoods();
        }

        private void cbSub_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbTU.SelectedIndex = 0;
            cbInv.SelectedIndex = 0;
            GetGoods();
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            GetGoods();
        }

        private void bwGoods_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] args = e.Argument as object[];

            dtGoods2 = null;
            if (SomeColumnShop2Visible())
            {
                task_GetGoodsFromShop2 = new Task(() => dtGoods2 = Config.hCntShop2.GetGoods((int)args[0], (int)args[1], (int)args[2], (int)args[3], (int)args[4], (int)args[5]));
                task_GetGoodsFromShop2.Start();
            }

            dtGoods = (int)args[0] != 6 ? Config.hCntMain.GetGoods((int)args[0], (int)args[1], (int)args[2], (int)args[3], (int)args[4], (int)args[5])
                                                    : Config.hCntAdd.GetGoods((int)args[0], (int)args[1], (int)args[2], (int)args[3], (int)args[4], (int)args[5]);
            if (task_GetGoodsFromShop2 != null && task_GetGoodsFromShop2.Status == TaskStatus.Running)
            {
                task_GetGoodsFromShop2.Wait();
            }

            if (dtGoods2 != null)
            {
                AddGoodsFromShop2(false);
            }

            // Оставленно на будущее
            
            //// Нарощенная функция для получения списка из второго магазина
            //// v0.1 Пока что будет не правильные числа (числа будут наоборот)
            //DataTable dtTemp = dtGoods;

            //dtGoods2 = null;
            //if (SomeColumnShop2Visible())
            //{
            //    task_GetGoodsFromShop2 = new Task(() => dtGoods2 = Config.hCntMain.GetGoods((int)args[0], (int)args[1], (int)args[2], (int)args[3], (int)args[4], (int)args[5]));
            //    task_GetGoodsFromShop2.Start();
            //}

            //dtGoods = (int)args[0] != 6 ? Config.hCntShop2.GetGoods((int)args[0], (int)args[1], (int)args[2], (int)args[3], (int)args[4], (int)args[5])
            //                                        : Config.hCntShop2.GetGoods((int)args[0], (int)args[1], (int)args[2], (int)args[3], (int)args[4], (int)args[5]);
            //if (task_GetGoodsFromShop2 != null && task_GetGoodsFromShop2.Status == TaskStatus.Running)
            //{
            //    task_GetGoodsFromShop2.Wait();
            //}

            //if (dtGoods2 != null)
            //{
            //    AddGoodsFromShop2(true);
            //}

            //DataTable dtTemp2 = dtGoods;

            //dtGoods = null;
            //// Обеденим 2 таблицы.
            //dtTemp.Merge(dtTemp2);
            //dtGoods = dtTemp;
            //// Сравним 2 базы по ean если ean совпадает то удаляем из второго списка
            

        }

        /// <summary>
        /// Функция добавления данных со второго магазина
        /// </summary>
        /// <param name="reverse">Переменная для смены основного магазина</param>
        private void AddGoodsFromShop2(bool reverse)
        {
            
            if (!reverse)
            {
                string[] column_names = new string[] { "ost2", "prihod2", "rash2", "ostOnDate2", "prihodOnline2", "ostOnline2", "avgReal2", "rcena2", "morningNacen2", "vozvrat2",
                                                   "zakaz2", "zakazUnconf2", "zapas2", "ShelfSpace2", "avgRealWeek2", "uVozvr2", "zcena2", "rn2", "OOVozvr2" };
                foreach (string column_name in column_names)
                {
                    dtGoods.Columns.Add(column_name, typeof(decimal));
                }

                if (SomeColumnShop2Visible())
                {
                    foreach (DataRow row2 in dtGoods2.Rows)
                    {
                        DataRow row1 = dtGoods.AsEnumerable().FirstOrDefault(dr => dr["ean"].ToString() == row2["ean"].ToString());
                        if (row1 != null)
                        {
                            foreach (string column_name in column_names)
                            {
                                row1[column_name] = row2[column_name.Replace("2", "")];
                            }
                        }
                        else
                        {
                            dtGoods.Rows.Add(row2.ItemArray);

                            foreach (string column_name in column_names)
                            {
                                object value = dtGoods.Rows[dtGoods.Rows.Count - 1][column_name.Replace("2", "")];
                                dtGoods.Rows[dtGoods.Rows.Count - 1][column_name.Replace("2", "")] = 0;
                                dtGoods.Rows[dtGoods.Rows.Count - 1][column_name] = value;
                            }
                        }
                    }
                }
            }
            else
            {
                string[] column_names = new string[] { "ost2", "prihod2", "rash2", "ostOnDate2", "prihodOnline2", "ostOnline2", "avgReal2", "rcena2", "morningNacen2", "vozvrat2",
                                                   "zakaz2", "zakazUnconf2", "zapas2", "ShelfSpace2", "avgRealWeek2", "uVozvr2", "zcena2", "rn2", "OOVozvr2" };
                string[] column_names2 = new string[] { "ost", "prihod", "rash", "ostOnDate", "prihodOnline", "ostOnline", "avgReal", "rcena", "morningNacen", "vozvrat",
                                                   "zakaz", "zakazUnconf", "zapas", "ShelfSpace", "avgRealWeek", "uVozvr", "zcena", "rn", "OOVozvr" };

                foreach (string column_name in column_names2)
                {
                    dtGoods2.Columns[column_name].ColumnName = column_name + "2";
                }

                foreach (string column_name in column_names)
                {
                    dtGoods2.Columns.Add(column_name, typeof(decimal));
                }

                if (SomeColumnShop2Visible())
                {
                    foreach (DataRow row2 in dtGoods.Rows)
                    {
                        DataRow row1 = dtGoods2.AsEnumerable().FirstOrDefault(dr => dr["ean"].ToString() == row2["ean"].ToString());
                        if (row1 != null)
                        {
                            foreach (string column_name in column_names)
                            {
                                row1[column_name] = row2[column_name.Replace("2", "")];
                            }
                        }
                        else
                        {
                            dtGoods2.Rows.Add(row2.ItemArray);

                            foreach (string column_name in column_names)
                            {
                                object value = dtGoods2.Rows[dtGoods.Rows.Count - 1][column_name.Replace("2", "")];
                                dtGoods2.Rows[dtGoods.Rows.Count - 1][column_name.Replace("2", "")] = 0;
                                dtGoods2.Rows[dtGoods.Rows.Count - 1][column_name] = value;
                            }
                        }
                    }
                }
                dtGoods = dtGoods2;
            }
        }

        private void bwGoods_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Config.ChangeFormEnabled(this, true);
            bsGoods.DataSource = dtGoods.DefaultView;
            grdGoods.AutoGenerateColumns = false;
            grdGoods.DataSource = bsGoods;
            pbGoods.Visible = false;
            //SetColumnsVisible();
            SetFilter();
            SetColumnsWidth();
            ChoosePereocGoods();

            if (isWeInOut)
            {
                tbSupplier2.Enabled = true;
                btClearPost2.Enabled = true;
            }
            else
            {
                tbSupplier2.Enabled = false;
                btClearPost2.Enabled = false;
            }

            //btSelectAll.Enabled = false;
        }

        private void grdGoods_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            rowColor = Color.White;
            grdGoods.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;

            if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
                && Config.linkToCurrentRequest != null
                && Config.linkToCurrentRequest.getGoodsData() != null
                && Config.linkToCurrentRequest.getGoodsData().AsEnumerable().Select(x => x.Field<int>("id_tovar")).ToArray().Contains((int)(grdGoods.Rows[e.RowIndex].Cells["id_tovar"].Value)))
            {
                grdGoods.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                    grdGoods.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor =
                grdGoods.Rows[e.RowIndex].Cells["ean"].Style.BackColor =
                    //grdGoods.Rows[e.RowIndex].Cells["cName"].Style.BackColor =
                    Color.FromArgb(173, 255, 135);
            }
            else
            {
                if ((decimal)grdGoods.Rows[e.RowIndex].Cells["ostOnDate"].Value <= 0 ||
                    (int)grdGoods.Rows[e.RowIndex].Cells["zapas"].Value <= 0)
                {
                    rowColor = Color.FromArgb(255, 190, 190);
                }
                grdGoods.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor =
                grdGoods.Rows[e.RowIndex].DefaultCellStyle.BackColor = rowColor;

                if ((bool)grdGoods.Rows[e.RowIndex].Cells["isCatGood"].Value)
                {
                    rowColor  = Color.FromArgb(255, 233, 120);
                }
                grdGoods.Rows[e.RowIndex].Cells["ean"].Style.SelectionBackColor=
                grdGoods.Rows[e.RowIndex].Cells["ean"].Style.BackColor =
                    //grdGoods.Rows[e.RowIndex].Cells["cName"].Style.BackColor = 
                    rowColor;

                if (dtGoods.DefaultView[e.RowIndex]["idMaxrixGood"] != DBNull.Value)
                {
                    grdGoods.Rows[e.RowIndex].Cells["cName"].Style.SelectionBackColor =
                       grdGoods.Rows[e.RowIndex].Cells["cName"].Style.BackColor = pMatrix.BackColor;
                }
            }            
        }

        private void grdGoods_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            SetControlsEnabled();
        }

        private void grdGoods_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == grdGoods.Columns["choose"].Index)
            {
                grdGoods.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //    dtGoods.AcceptChanges();
                SetControlsEnabled();
            }
        }

        private void grdGoods_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == grdGoods.Columns["choose"].Index)
            {
                CheckAllGoods();
            }
        }
       
        private void grdGoods_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.Button == MouseButtons.Right)
            {
                grdGoods.CurrentCell = grdGoods[e.ColumnIndex, e.RowIndex];
                curRow = dtGoods.DefaultView[e.RowIndex].Row;
                if (e.ColumnIndex == grdGoods.Columns["cName"].Index)
                {
                    ttGoods.Show((int)cbDep.SelectedValue != 6 ? Config.hCntMain.GetFullNameTovar((int)curRow["id_tovar"]) : Config.hCntAdd.GetFullNameTovar((int)curRow["id_tovar"]), this, Cursor.Position.X - this.MdiParent.Location.X, Cursor.Position.Y - this.MdiParent.Location.Y - 40);
                }

                if (e.ColumnIndex == grdGoods.Columns["prihod"].Index || e.ColumnIndex == grdGoods.Columns["rashod"].Index)
                {
                    //Проверка на наличие библиотеки с формами
                    if (!System.IO.File.Exists(Application.StartupPath + "\\PrihodRealizForms.dll"))
                    {
                        MessageBox.Show("Библиотека \"Движение и реализация товара\" не найденна.\n Обратитесь в ОЭЭС.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        Config.ShowPrihodRealiz(int.Parse(curRow["id_tovar"].ToString()), curRow["ean"].ToString(), curRow["cName"].ToString(), (int)cbDep.SelectedValue, (e.ColumnIndex == grdGoods.Columns["prihod"].Index));
                    }
                }

                if (e.ColumnIndex == grdGoods.Columns["ean"].Index && UserSettings.User.StatusCode != "КНТ" && UserSettings.User.StatusCode != "ПР")
                {
                    cmsGoods.Show(Cursor.Position);
                }

                if (e.ColumnIndex == grdGoods.Columns["ostOnDate"].Index)
                {
                    DLLRequestTovarOst.FormView frmOst = new DLLRequestTovarOst.FormView(int.Parse(curRow["id_tovar"].ToString()), (int)cbDep.SelectedValue);
                    frmOst.ShowDialog();
                }

            }

            if (e.RowIndex == -1 && e.Button == MouseButtons.Right)
            {
                selColumn = (selColumn == e.ColumnIndex ? -1 : e.ColumnIndex);
                grdGoods.Refresh();
            }

        }
        
        private void grdGoods_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            ttGoods.Hide(this);
        }

        private void tbEAN_TextChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void tbGoodName_TextChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void chbCat_CheckedChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void btSelectAll_Click(object sender, EventArgs e)
        {
            ChooseAllGoods();
        }

        private void tsAddToCatalog_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Добавить товар в каталог?", "Запрос.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Config.hCntMain.SetGoodsPosition(1, (int)curRow["id_tovar"]);
                Logging.StartFirstLevel(551);
                Logging.Comment("Добавление товара в каталог");
                Logging.Comment("Отдел: " + cbDep.Text + ", Номер: " + cbDep.SelectedValue.ToString());
                Logging.Comment("ID товара: " + curRow["id_tovar"].ToString() + ", EAN: " + curRow["ean"].ToString().Trim() + ", Наименование: " + curRow["cName"].ToString().Trim());
                Logging.Comment("Т/У группа: " + ((DataTable)cbTU.DataSource).Select("id = " + curRow["id_grp1"].ToString())[0]["cname"].ToString().Trim() + ", ID: " + curRow["id_grp1"].ToString() +
                            ", Инв. группа: " + ((DataTable)cbInv.DataSource).Select("id = " + curRow["id_grp2"].ToString())[0]["cname"].ToString().Trim() + ", ID: " + curRow["id_grp2"].ToString());
                Logging.Comment("Цена пр.: " + curRow["rcena"].ToString());
                curRow.SetField<bool>("isCatGood", true);
                grdGoods.Refresh();
                Logging.Comment("Завершение операции \"Добавление товара в каталог\"");
                Logging.StopFirstLevel();
            }
        }

        private void tsDelFromCatalog_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить товар из каталога?", "Запрос.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Config.hCntMain.SetGoodsPosition(2, (int)curRow["id_tovar"]);
                Logging.StartFirstLevel(552);
                Logging.Comment("Удаление товара из каталога");
                Logging.Comment("Отдел: " + cbDep.Text + ", Номер: " + cbDep.SelectedValue.ToString());
                Logging.Comment("ID товара: " + curRow["id_tovar"].ToString() + ", EAN: " + curRow["ean"].ToString().Trim() + ", Наименование: " + curRow["cName"].ToString().Trim());
                Logging.Comment("Т/У группа: " + ((DataTable)cbTU.DataSource).Select("id = " + curRow["id_grp1"].ToString())[0]["cname"].ToString().Trim() + ", ID: " + curRow["id_grp1"].ToString() +
                             ", Инв. группа: " + ((DataTable)cbInv.DataSource).Select("id = " + curRow["id_grp2"].ToString())[0]["cname"].ToString().Trim() + ", ID: " + curRow["id_grp2"].ToString());
                Logging.Comment("Цена пр.: " + curRow["rcena"].ToString());
                curRow.SetField<bool>("isCatGood", false);
                grdGoods.Refresh();
                Logging.Comment("Завершение операции \"Удаление товара из каталога\"");
                Logging.StopFirstLevel();
            }
        }

        private void tsAddToProh_Click(object sender, EventArgs e)
        {
            frmAddNote frmComment = new frmAddNote(2);
            frmComment.ShowDialog();
            if((int)cbDep.SelectedValue != 6)
                Config.hCntMain.SetGoodsPosition(3, (int)curRow["id_tovar"],frmComment.ReturnedValue);
            else
                Config.hCntAdd.SetGoodsPosition(3, (int)curRow["id_tovar"], frmComment.ReturnedValue);
            Logging.StartFirstLevel(549);
            Logging.Comment("Добавление товара в список запрещенных к добавлению в заявки товаров");
            Logging.Comment("Отдел: " + cbDep.Text + ", Номер: " + cbDep.SelectedValue.ToString());
            Logging.Comment("ID товара: " + curRow["id_tovar"].ToString() + ", EAN: " + curRow["ean"].ToString().Trim() + ", Наименование: " + curRow["cName"].ToString().Trim());
            Logging.Comment("Т/У группа: " + ((DataTable)cbTU.DataSource).Select("id = " + curRow["id_grp1"].ToString())[0]["cname"].ToString().Trim() + ", ID: " + curRow["id_grp1"].ToString() +
                            ", Инв. группа: " + ((DataTable)cbInv.DataSource).Select("id = " + curRow["id_grp2"].ToString())[0]["cname"].ToString().Trim() + ", ID: " + curRow["id_grp2"].ToString());
            Logging.Comment("Цена пр.: " + curRow["rcena"].ToString());
            Logging.Comment("Причина добавления: " + frmComment.ReturnedValue.Trim());
            curRow.SetField<bool>("isProh", true);
            grdGoods.Refresh();
            Logging.Comment("Завершение операции \"Добавление товара в список запрещенных к добавлению в заявки товаров\"");
            Logging.StopFirstLevel();
        }

        private void cmsGoods_Opening(object sender, CancelEventArgs e)
        {
            tsAddToCatalog.Enabled = !(bool)curRow["isCatGood"];
            tsDelFromCatalog.Enabled = (bool)curRow["isCatGood"];
            tsAddToProh.Enabled = !(bool)curRow["isProh"];

            tsmiAddGoodMatrix.Enabled = curRow["idMaxrixGood"] == DBNull.Value;
            tsmiDelGoodMatrix.Enabled = curRow["idMaxrixGood"] != DBNull.Value;


        }

        private void btExcel_Click(object sender, EventArgs e)
        {
            CreateExcelReport();
        }

        private void tsCreateSale_Click(object sender, EventArgs e)
        {
            AddEditPeriod.frmAddEditPeriod frmSale = new AddEditPeriod.frmAddEditPeriod(int.Parse(curRow["id_tovar"].ToString()), curRow["ean"].ToString(), curRow["cName"].ToString(), (int)cbDep.SelectedValue);
            frmSale.ShowDialog();
        }

        private void btCreatePereoc_Click(object sender, EventArgs e)
        {
            CreatePereoc();
        }

        private void frmGoods_Activated(object sender, EventArgs e)
        {
            ChoosePereocGoods();
            SetControlsEnabled();

            if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
                && Config.linkToCurrentRequest != null
                && Config.dtProperties != null
                && Config.dtProperties.Rows.Count > 0
                && Config.dtProperties.Select("id_val = 'use samePost' AND TRIM(val) = '1'").Count() > 0
                && !bwGoods.IsBusy)
            {
                int oldIdPost = idPost;
                string postName = "";
                Config.linkToCurrentRequest.GetPost(ref idPost, ref postName);
               
                tbSupplier.Text = postName.Trim();
                if (oldIdPost != idPost)
                {
                    GetGoods();
                }
            }

            SetColumnsVisible();
        }
        
        private void btSettings_Click(object sender, EventArgs e)
        {
            GeneralSettings.frmSettings frmGoodsSettings = new GeneralSettings.frmSettings();
            frmGoodsSettings.ShowDialog();
            Config.dtProperties = Config.hCntMain.GetProperties();
            GetGoods();
        }

        private void tbSupplier_MouseClick(object sender, MouseEventArgs e)
        {
            Config.dtProperties = Config.hCntMain.GetProperties();
            Posts.frmPosts frmPosts = new Posts.frmPosts((int)cbDep.SelectedValue);
            frmPosts.ShowDialog();
            tbSupplier.Text = frmPosts.PostName;
            idPost = frmPosts.PostId;

            //SetPost(frmPosts.PostId, frmPosts.PostName);

            if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
                && Config.linkToCurrentRequest != null)
                //&& Config.dtProperties != null
                //&& Config.dtProperties.Rows.Count > 0
                //&& Config.dtProperties.Select("id_val = 'use samePost' AND TRIM(val) = '1'").Count() > 0)
            {
                Config.linkToCurrentRequest.SetPost(frmPosts.PostId, frmPosts.PostName);
            }

            GetGoods();
        }

        private void grdGoods_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 
                && grdGoods.Columns[e.ColumnIndex].Name == "choose"
                && Config.dtPereocGoods != null
                && Config.dtPereocGoods.Rows.Count > 0
                && (bool)dtGoods.DefaultView.ToTable().Rows[e.RowIndex]["choose"] == false
                && Config.dtPereocGoods.Select("id_tovar = " + dtGoods.DefaultView.ToTable().Rows[e.RowIndex]["id_tovar"].ToString()).Count() > 0)
            {
                Config.dtPereocGoods.Rows.Remove(Config.dtPereocGoods.Select("id_tovar = " + dtGoods.DefaultView.ToTable().Rows[e.RowIndex]["id_tovar"].ToString())[0]);
            }
        }

        private void grdGoods_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1
                && e.ColumnIndex != choose.Index
                && (UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН"))
            {
                int id_type;
                if (Config.linkToCurrentRequest == null || Config.linkToCurrentRequest.curIdType == 0)
                {

                    frmChooseType frmType = new frmChooseType();
                    frmType.ShowDialog();
                    id_type = frmType.choosedType;
                }
                else
                {
                    id_type = Config.linkToCurrentRequest.curIdType;
                }

                if (id_type != 2 && id_type != 8 && bool.Parse(grdGoods.Rows[e.RowIndex].Cells["isProh"].Value.ToString()))
                {
                    MessageBox.Show("Товар: " + grdGoods.Rows[e.RowIndex].Cells["cname"].Value.ToString() + "\n присутствует в списке запрещенных товаров для\nдобавления в заявку\nДобавление указанного товара в заявку невозможно.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                bool showRequest = true;
                string id_good = dtGoods.DefaultView.ToTable().Rows[e.RowIndex]["id_tovar"].ToString();

                if (Config.linkToCurrentRequest == null)
                {
                    ((Main)this.MdiParent).SetTab("Создание заявки.", "frmEditRequest", new object[] { 1, 0, false }, 0);
                    if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
                    && Config.linkToCurrentRequest != null)
                    //&& Config.dtProperties != null
                    //&& Config.dtProperties.Rows.Count > 0
                    //&& Config.dtProperties.Select("id_val = 'use samePost' AND TRIM(val) = '1'").Count() > 0)
                    {
                        Config.linkToCurrentRequest.SetPost(this.idPost, tbSupplier.Text);
                    }
                    ((Main)this.MdiParent).SetTab("", this.Tag.ToString(), null, null);
                    
                }
                else
                {
                    DataRow[] drCurrentGood = Config.linkToCurrentRequest.getGoodsData().Select("id_tovar = " + id_good);
                    Config.linkToCurrentRequest.SetPost(this.idPost, tbSupplier.Text);
                    if (drCurrentGood.Count() > 0)
                    {
                        if (MessageBox.Show("Удалить товар из заявки?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            foreach (DataRow drRowToDel in drCurrentGood)
                            {
                                Config.linkToCurrentRequest.DelGoods(drRowToDel);
                            }
                        }
                        showRequest = false;
                    }
                }

                if (Config.linkToCurrentRequest.curIdType == 0)
                {
                    Config.linkToCurrentRequest.curIdType = id_type;
                }

                viewDataWeInOut();

                if (showRequest)
                    Config.linkToCurrentRequest.AddGoodsFromGoodList(grdGoods.Rows[e.RowIndex].Cells["ean"].Value.ToString(), isWeInOut);
                
                //)
                    //((Main)this.MdiParent).SetTab("", this.Tag.ToString(), null, null);  
                    // ((Main)this.MdiParent).SetTab("", Config.linkToCurrentRequest.Tag.ToString(), null, null);
            }
        }

        private void grdGoods_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                e.Handled = true;
                Clipboard.SetText(grdGoods.CurrentCell.Value.ToString().Trim());
            }
        }

        private void grdGoods_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == selColumn)
            {
                e.CellStyle.BackColor = SystemColors.Highlight;
            }
        }

        
        private void grdGoods_Sorted(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                //direction = grdGoods.SortedColumn.HeaderCell.SortGlyphDirection != SortOrder.Ascending ? "ASC" : "DESC";
                direction = direction == "ASC" ? "DESC" : "ASC";
                if (SortString.Contains(grdGoods.SortedColumn.DataPropertyName))
                {
                    SortString = SortString.Replace(grdGoods.SortedColumn.DataPropertyName + " ASC", "").Replace(grdGoods.SortedColumn.DataPropertyName + " DESC", "");
                }
                SortString += ", " + grdGoods.SortedColumn.DataPropertyName + " " + direction;
                SortString = SortString.Trim().Trim(',').Trim().Replace(", , ", ", ");
            }
            else
            {
                direction = grdGoods.SortedColumn.HeaderCell.SortGlyphDirection == SortOrder.Ascending ? "ASC" : "DESC";
                SortString = grdGoods.SortedColumn.DataPropertyName + " " + direction;
            }

            bsGoods.Sort = SortString;
           // MessageBox.Show(SortString);
        }

        private void btClearPost_Click(object sender, EventArgs e)
        {
            if (idPost != 0)
            {
                Config.dtProperties = Config.hCntMain.GetProperties();
                tbSupplier.Text = "";
                idPost = 0;

                if ((UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН")
                    && Config.linkToCurrentRequest != null
                    && Config.dtProperties != null
                    && Config.dtProperties.Rows.Count > 0
                    && Config.dtProperties.Select("id_val = 'use samePost' AND TRIM(val) = '1'").Count() > 0)
                {
                    Config.linkToCurrentRequest.SetPost(0, "");
                }

                GetGoods();
            }
        }

        private void grdGoods_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == grdGoods.Columns["choose"].Index)
            {
                grdGoods.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dtGoods.AcceptChanges();
                SetControlsEnabled();
            }
        }

        private void cbReqGoods_CheckedChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void chbReqGoods_VisibleChanged(object sender, EventArgs e)
        {
            if (!chbReqGoods.Visible)
                chbReqGoods.Checked = false;
        }

        private void btFilter_Click(object sender, EventArgs e)
        {
            int type = -1;
            DataTable dtData = null;

            if(!(sender is Button))
            {
                return;
            }

            if (((Button)sender).Equals(btFilterTU)
                && cbTU.DataSource is DataTable)
            {
                type = 0;
                dtData = (DataTable)cbTU.DataSource; 
            }

            if (((Button)sender).Equals(btFilterInv)
                && cbInv.DataSource is DataTable)
            {
                type = 1;
                dtData = (DataTable)cbInv.DataSource; 
            }

            if (((Button)sender).Equals(btFilterSub))
            {
                type = 2;
                dtData = (DataTable)cbSub.DataSource;
            }

            if (((Button)sender).Equals(btFilterManager))
            {
                type = 3;
                dtData = (DataTable)cbManager.DataSource;
            }

            if (type != -1 && dtData != null && cbDep.SelectedValue is int)
            {
                Form filterChoosing = new frmFilter(type, dtData, (int)cbDep.SelectedValue);
                filterChoosing.ShowDialog();

                int newSelectedIndex = (filterChoosing.DialogResult == DialogResult.OK ? -1 
                                : filterChoosing.DialogResult == DialogResult.No ? 0 
                                : 1);
                switch (type)
                {
                    case 0:
                        cbTU.SelectedValue = newSelectedIndex <= 0
                                                ? newSelectedIndex 
                                                : cbTU.SelectedValue;

                        if (newSelectedIndex == -1)
                        {
                            cbInv.SelectedIndex = 0;
                            cbSub.SelectedIndex = 0;
                        }

                        break;
                    case 1:
                        cbInv.SelectedValue = newSelectedIndex <= 0 
                                                ? newSelectedIndex 
                                                : cbInv.SelectedValue;

                        if (newSelectedIndex == -1)
                        {
                            cbTU.SelectedIndex = 0;
                            cbSub.SelectedIndex = 0;
                        }

                        break;
                    case 2:
                        cbSub.SelectedValue = newSelectedIndex <= 0 
                                                ? newSelectedIndex 
                                                : cbSub.SelectedValue;

                        if (newSelectedIndex == -1)
                        {
                            cbTU.SelectedIndex = 0;
                            cbInv.SelectedIndex = 0;
                        }

                        break;
                    case 3:
                        cbManager.SelectedValue = newSelectedIndex <= 0 
                                                ? newSelectedIndex 
                                                : cbManager.SelectedValue;
                        break;
                }

                if(newSelectedIndex != 1)
                    GetGoods();
            }
        }

        /// <summary>
        /// Получение данных для комбика с менеджерами
        /// </summary>
        private void GetManagers()
        {
            if (UserSettings.User.StatusCode != "МН" /*&& UserSettings.User.StatusCode != "ДМН"*/)
            {
                int id_dep;
                if (int.TryParse(cbDep.SelectedValue.ToString(), out id_dep))
                {
                    DataTable dtManagers;
                    if (id_dep == 6)
                        dtManagers = Config.hCntAdd.GetManagers(6,true);
                    else
                    {
                        if (id_dep == 0)
                        {
                            dtManagers = Config.hCntMain.GetManagers(0, true);
                            DataRow[] dtAddManagers = Config.hCntAdd.GetManagers(6, true).Select("id_Access NOT IN (" + Config.GetStringFromRow(dtManagers, "id_Access") + ")");

                            if (dtAddManagers.Count() > 0)
                                dtManagers.Merge(DataTableExtensions.CopyToDataTable(dtAddManagers));

                            dtManagers.DefaultView.Sort = "FIO ASC";
                            dtManagers = dtManagers.DefaultView.ToTable();

                            int i;
                            for (i = 0; i < dtManagers.Rows.Count; i++)
                            {
                                if ((long)dtManagers.Rows[i]["Num"] != 0)
                                    dtManagers.Rows[i]["Num"] = i + 1;
                            }

                            dtManagers.DefaultView.Sort = "Num ASC";
                            dtManagers = dtManagers.DefaultView.ToTable();

                        }
                        else
                        {
                            dtManagers = Config.hCntMain.GetManagers(id_dep, true);
                        }
                    }

                    dtManagers.Rows[0]["FIO"] = "";
                    dtManagers.Columns["FIO"].ColumnName = "cname";
                    dtManagers.Columns["id_Access"].ColumnName = "id";

                    cbManager.DataSource = dtManagers;
                    cbManager.DisplayMember = "cname";
                    cbManager.ValueMember = "id";

                    cbManager.SelectedValue = isFilterSettingsExists("mngr") ? -1 : 0;
                }
            }
            else
            {
                lbManager.Visible = 
                    cbManager.Visible =
                    btFilterManager.Visible = false;
            }
        }

        private bool isFilterSettingsExists(string id_val)
        {
            if (cbDep.SelectedValue is int)
            {
                DataTable dtSettings = Config.hCntMain.GetFilterSettings(id_val, (int)cbDep.SelectedValue);
                return dtSettings != null && dtSettings.Rows.Count > 0;
            }
            return false;
        }

        private void cbManager_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetGoods();
        }

        private void tsViewImage_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtIm = new DataTable();
                dtIm = Config.hCntMain.GetGoodsImage(int.Parse(curRow["id_tovar"].ToString()));

                if (dtIm.Rows.Count == 0)
                {
                    MessageBox.Show("Не найдено изображений товара!");
                    return;
                }
                else
                {
                    byte[] ImageBytes = (byte[])dtIm.Rows[0]["Pic"];
                    Image img = IC.ByteArrayToImage(ImageBytes);

                    frmViewImage frmView = new frmViewImage(img);
                    frmView.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            //IC.ByteArrayToImage(

            //AddEditPeriod.frmAddEditPeriod frmSale = new AddEditPeriod.frmAddEditPeriod(int.Parse(curRow["id_tovar"].ToString()), curRow["ean"].ToString(), curRow["cName"].ToString(), (int)cbDep.SelectedValue);
            //frmSale.ShowDialog();

        }

        private void btImages_Click(object sender, EventArgs e)
        {
            if (grdGoods.CurrentCell != null)
            {       
                DataRow dr = dtGoods.DefaultView[grdGoods.CurrentRow.Index].Row;

                frmGoodsImages frmImages = new frmGoodsImages(int.Parse(dr["id_tovar"].ToString()), dr["ean"].ToString(), dr["cName"].ToString());
                frmImages.ShowDialog();
            }            
        }


        ////Поиск выделенного товара по ean
        //private void FindSelectedGood()
        //{
            //if (Config.currentEan.Trim().Length != 0 && dtGoods != null && grdGoods.Rows.Count > 0)
            //{
            //    grdGoods.Select();
            //    DataTable dtGridView = dtGoods.DefaultView.ToTable();
            //    int rIndex = 0;
            //    DataRow[] dtSelected = dtGridView.Select("Convert(ean,'System.Int64') = " + Config.currentEan);

            //    if (dtSelected.Count() > 0)
            //    {
            //        rIndex = dtGridView.Rows.IndexOf(dtSelected[0]);
            //        if (rIndex > 0)
            //        {
            //            grdGoods.CurrentCell = grdGoods[0, rIndex];
            //            grdGoods.FirstDisplayedScrollingRowIndex = rIndex;
            //        }
            //    }
            //}
        //}

        private void viewDataWeInOut()
        {
            DataTable dtTmp = Config.hCntMain.getAllSettingsWeInOut(2, "prhp");

            isWeInOut = (dtTmp != null && dtTmp.Rows.Count != 0 && dtTmp.Select("Convert(value, 'System.Int32') = " + idPost).Count() != 0);
        }

        private void tbSupplier2_MouseClick(object sender, MouseEventArgs e)
        {
            Posts.frmPosts frmPosts = new Posts.frmPosts((int)cbDep.SelectedValue);
            frmPosts.ShowDialog();
            tbSupplier2.Text = frmPosts.PostName;
            idPost2 = frmPosts.PostId;

            GetGoods();
        }

        private void tsmiAddGoodMatrix_Click(object sender, EventArgs e)
        {
            int id_tovar = (int)curRow["id_tovar"];
            DataTable dtResult = (int)cbDep.SelectedValue!=6? Config.hCntMain.setTovarMatrix(id_tovar, false): Config.hCntAdd.setTovarMatrix(id_tovar, false);
            if (dtResult == null || dtResult.Rows.Count == 0) return;

            curRow["idMaxrixGood"] = (int)dtResult.Rows[0]["id"];
        }

        private void tsmiDelGoodMatrix_Click(object sender, EventArgs e)
        {
            int id_tovar = (int)curRow["id_tovar"];
            DataTable dtResult = (int)cbDep.SelectedValue != 6 ? Config.hCntMain.setTovarMatrix(id_tovar, true) : Config.hCntAdd.setTovarMatrix(id_tovar, true);
            if (dtResult == null || dtResult.Rows.Count == 0) return;

            curRow["idMaxrixGood"] = DBNull.Value;
        }

        private void chbMatrix_Click(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void grdGoods_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //Рисуем рамку для выделеной строки
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
                Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

                ControlPaint.DrawBorder(e.Graphics, rect,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid);
            }
        }

        private void btClearPost2_Click(object sender, EventArgs e)
        {
            if (idPost2 != 0)
            {
                tbSupplier2.Text = "";
                idPost2 = 0;

                GetGoods();
            }
        }
    }

    /// <summary>
    /// Сравнивает строки в таблицах групп
    /// </summary>
    class comparerTU : IEqualityComparer<DataRow>
    {
        public bool Equals(DataRow x, DataRow y)
        {
            return ((int)x["id"]).Equals((int)y["id"])
                   && ((int)x["id_otdel"]).Equals((int)y["id_otdel"]);
        }

        public int GetHashCode(DataRow obj)
        {
            return (obj["id"].ToString() + obj["cname"].ToString().Trim() + obj["id_otdel"].ToString()).GetHashCode();
        }
    }
}
