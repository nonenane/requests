using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PrihodRealizForms;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;

namespace Requests
{
    static class Config
    {
        public static Procedures hCntMain { get; set; } //осн. коннект
        public static Procedures hCntAdd { get; set; } //доп. коннект
        public static Procedures hCntShop2 { get; set; } //доп. коннект к базе магазина 2
        public static string statusesFiltr { get; set; }
        public static string statusesNames { get; set; }
        public static DateTime curDate { get; set; }
        public static DataTable dtRequests { get; set; }
        public static DataTable dtPereocGoods { get; set; } //выбранные товары для переоценки
        public static string curPereocFrmName { get; set; } //текущее имя формы с переоценками
        public static ArrayList editPereocForms = new ArrayList();
        public static DataTable dtProperties { get; set; }
        public static ArrayList openedRequests { get; set; }
        public static frmEditRequest linkToCurrentRequest { get; set; }
        public static string currentEan { get; set; } //текущий выделенный товар на форме редактирования

        
        /// <summary>
        /// признак того, что дата на форме "Укажите дату переоценки" выбрана
        /// </summary>
        public static bool DatePereocSelected { get; set; }

        /// <summary>
        /// Дата выбранная на форме "Укажите дату переоценки"
        /// </summary>
        public static DateTime DatePereoc { get; set; }

        public static string[] RunArguments = null;


        //public int curIdPost
        /// <summary>
        /// Получение списка типов кредитования
        /// (создается вручную, т к в базе нет соответствующей таблицы)
        /// </summary>
        /// <param name="all">Добавление строки "все"</param>
        /// <returns>Таблица типов кредитования</returns>
        public static DataTable GetCreditTypes(bool all)
        {
            DataTable dtCreditTypes;
            //в БД таблицы нет, создаем вручную
            dtCreditTypes = new DataTable();
            dtCreditTypes.Columns.Add(new DataColumn("id", typeof(int)));
            dtCreditTypes.Columns.Add(new DataColumn("cname", typeof(string)));

            //Добавляем записи в таблицу
            DataRow drNew;
            if (all)
            {
                drNew = dtCreditTypes.NewRow();
                drNew[0] = 0;
                drNew[1] = "Все";
                dtCreditTypes.Rows.Add(drNew);
            }

            drNew = dtCreditTypes.NewRow();
            drNew[0] = 1;
            drNew[1] = "Под реализацию";
            dtCreditTypes.Rows.Add(drNew);

            drNew = dtCreditTypes.NewRow();
            drNew[0] = 2;
            drNew[1] = "С отсрочкой";
            dtCreditTypes.Rows.Add(drNew);

            drNew = dtCreditTypes.NewRow();
            drNew[0] = 3;
            drNew[1] = "В кредит";
            dtCreditTypes.Rows.Add(drNew);

            drNew = dtCreditTypes.NewRow();
            drNew[0] = 4;
            drNew[1] = "Вторая кредитная линия";
            dtCreditTypes.Rows.Add(drNew);

            dtCreditTypes.AcceptChanges();

            return dtCreditTypes;
        }
        
        #region Методы
        public static string GetStringFromDT(DataTable dtGoods, string colName)
        {
            if (dtGoods.Rows.Count == 0)
                return "-1";
            string id_goods = "";
            foreach (DataRow dRow in dtGoods.Rows)
            {
                id_goods += dRow[colName] + ",";
            }

            return id_goods.Remove(id_goods.Length - 1);
        }

        public static void GetStatuses()
        {
            DataTable dtStatuses = hCntMain.GetReqStatuses();
            statusesFiltr = (Nwuram.Framework.Settings.User.UserSettings.User.StatusCode == "МН" /*|| Nwuram.Framework.Settings.User.UserSettings.User.StatusCode == "ДМН"*/ ? hCntMain.SetFilterSettings(false, "") : hCntMain.SetFilterSettings(false, "").Replace("0", ""));
            statusesNames = "";

            foreach (DataRow drStat in dtStatuses.Rows)
            {
                if (statusesFiltr.Replace(" ", "").Split(',').Contains<string>(drStat["id"].ToString()))
                {
                    statusesNames += (Config.statusesNames.Length == 0 ? "" : ", ") + drStat["cName"].ToString().Trim();
                }
            }

            if (statusesFiltr.Replace(" ", "").Split(new char[] {','} ,StringSplitOptions.RemoveEmptyEntries).Count() == dtStatuses.Rows.Count)
            {
                statusesFiltr = "";
            }

            if (statusesFiltr.Length == 0)
            {
                statusesNames = "Все статусы";
            }
        }

        public static void ChangeFormEnabled(System.Windows.Forms.Form frm, bool enabled)
        {
            foreach (System.Windows.Forms.Control con in frm.Controls)
            {
                con.Enabled = enabled;
            }
        }

        /// <summary>
        /// Вывод форм движение/реализация
        /// </summary>
        /// <param name="id_tovar">id товара</param>
        /// <param name="ean">код товара</param>
        /// <param name="name">наименование</param>
        /// <param name="isPrihod">тип выводимой формы (true - приход, false - реализация)</param>
        public static void ShowPrihodRealiz(int id_tovar, string ean, string name, int id_dep, bool isPrihod)
        {
            if (isPrihod)
            {
                frmPrihod prih;
                if (id_dep != 6)
                {
                    prih = new frmPrihod(id_tovar,
                                            ean,
                                            name,
                                            id_dep);

                }
                else
                {
                    prih = new frmPrihod(id_tovar,
                                            ean,
                                            name,
                                            id_dep);
                }

                prih.ShowDialog();
            }
            else
            {
                frmReal real;
                if (id_dep != 6)
                {
                    real = new frmReal(id_tovar,
                                        ean,
                                        name,
                                        id_dep);

                }
                else
                {
                    real = new frmReal(id_tovar,
                                        ean,
                                        name,
                                        id_dep);
                }
                real.ShowDialog();
            }
        }

        #endregion

        #region Выгрузка в EXCEL

        //Преобразование Грида в DataTable
        public static DataTable GetDataTableFromGrid(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            // Колонки
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Visible)
                {
                    DataColumn dc = new DataColumn(column.HeaderText.ToString());
                    dt.Columns.Add(dc);
                }
            }

            //Название колонок записываем в 1 строку
            DataRow headRow = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                headRow[dc.ColumnName] = dc.ColumnName;
            }
            dt.Rows.Add(headRow);

            // Строки
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                DataRow dr = dt.NewRow();
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    if (dt.Columns.Contains(dgv.Columns[dgvCell.ColumnIndex].HeaderText))
                    {
                        dr[dgv.Columns[dgvCell.ColumnIndex].HeaderText] = (dgvRow.Cells[dgv.Columns[dgvCell.ColumnIndex].Name].Value == null) ? "" : dgvRow.Cells[dgv.Columns[dgvCell.ColumnIndex].Name].Value.ToString().Trim();
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;

        }

        //Преобразование Грида в DataTable с группировкой по поставщику
        public static DataTable GetDataTableFromGridGroupByIdPost(DataGridView dgv)
        {
            string colWeightName = dgv.Columns["weight"].HeaderText;
            string colKolkorName = dgv.Columns["kolkor"].HeaderText;
            string colPostName = dgv.Columns["supplier"].HeaderText;

            DataGridViewColumn SortedColumn = dgv.Columns["supplier"];
            ListSortDirection GrdSortOrder = ListSortDirection.Ascending;            
            dgv.Sort(SortedColumn, GrdSortOrder);
            
            DataTable dt = new DataTable();
            DataTable dtResult = new DataTable();
            // Колонки
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if ((column.Visible) 
                    || (column.Name == "id_oper")                    
                    )                    
                {
                    DataColumn dc = new DataColumn(column.HeaderText.ToString());
                    dt.Columns.Add(dc);
                }
            }


            //Название колонок записываем в 1 строку
            DataRow headRow = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                headRow[dc.ColumnName] = dc.ColumnName;
            }
            dt.Rows.Add(headRow);

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                DataRow dr = dt.NewRow();
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    if (dt.Columns.Contains(dgv.Columns[dgvCell.ColumnIndex].HeaderText))
                    {
                        dr[dgv.Columns[dgvCell.ColumnIndex].HeaderText] = (dgvRow.Cells[dgv.Columns[dgvCell.ColumnIndex].Name].Value == null) ? "" : dgvRow.Cells[dgv.Columns[dgvCell.ColumnIndex].Name].Value.ToString().Trim();
                    }
                }
                if (int.Parse(dr["id_oper"].ToString()) != 4)
                {
                    dt.Rows.Add(dr);
                }
            }

            //DataView view = new DataView(dt);
            //view.Sort = "id_post asc";
            //dt = view.ToTable();
            
            dt.Columns.Remove("id_oper");

            dtResult = dt.Clone();

            DataRow drRes;
            DataRow drPost;
            DataRow[] PostRows;
            string post = colPostName;
            decimal weight = 0;
            int kolkor = 0;

            for (int i = 0; dt.Rows.Count > i; i++)
            {
                if (dt.Rows[i][colPostName].ToString() != post)
                {
                    drPost = dtResult.NewRow();
                    drPost[colPostName] 
                        = post
                        = dt.Rows[i][colPostName].ToString();

                    weight = 0;
                    kolkor = 0;

                    PostRows = dt.Select(colPostName + " like '%" + post + "%'");

                    for (int j = 0; PostRows.Count() > j; j++)
                    {
                        decimal we = 0;
                        decimal.TryParse(PostRows[j][colWeightName].ToString(), out we);
                        int kol = 0;
                        int.TryParse(PostRows[j][colKolkorName].ToString(), out kol);
                        weight += we;
                        kolkor += kol;
                    }
                    drPost["Вес"] = weight;
                    drPost["Кол. кор."] = kolkor;
                    dtResult.Rows.Add(drPost);
                }
                
                drRes = dtResult.NewRow();
                for (int z=0; drRes.ItemArray.Count() > z; z++)
                {
                    drRes[z] = dt.Rows[i][z];
                }                    
                dtResult.Rows.Add(drRes);
            }


            return dtResult;

        }

        //Преобразование Грида в DataTable с группировкой по дате и поставщику
        public static DataTable GetDataTableFromGridGroupByDateAndPost(DataGridView dgv)
        {
            string colWeightName = dgv.Columns["weight"].HeaderText;
            string colKolkorName = dgv.Columns["kolkor"].HeaderText;
            string colPostName = dgv.Columns["supplier"].HeaderText;

            DataTable dt = new DataTable();
            DataTable dtTemp = new DataTable();
            DataTable dtResult = new DataTable();

            BindingSource bs = new BindingSource();
            bs = (BindingSource)dgv.DataSource;

            dt = (DataTable)bs.DataSource;
                //GetDataTableFromGrid(dgv);

            DataView view = new DataView(dt);
            view.Sort = "req_date asc, id_post asc";

            dt = view.ToTable();
            
            dtTemp.Columns.Add("req_date", typeof(string));
            dtTemp.Columns.Add("id_post", typeof(int));
            dtTemp.Columns.Add("post_name", typeof(string));            
            dtTemp.Columns.Add("weight", typeof(decimal));
            dtTemp.Columns.Add("kolkor", typeof(int));
            dtTemp.AcceptChanges();

            dtResult.Columns.Add("req_date", typeof(string));
            dtResult.Columns.Add("post_name", typeof(string));
            dtResult.Columns.Add("weight", typeof(decimal));
            dtResult.Columns.Add("kolkor", typeof(int));
            dtResult.AcceptChanges();

            for (int i = 0; dt.Rows.Count > i; i++)
            {
                if (dt.Rows[i]["id_oper"].ToString() != "4")
                {
                    dtTemp.Rows.Add(
                        dt.Rows[i]["req_date"],
                        dt.Rows[i]["id_post"],
                        dt.Rows[i]["post_name"],
                        dt.Rows[i]["weight"],
                        dt.Rows[i]["kolkor"]);
                    dtTemp.AcceptChanges();
                }
            }

            EnumerableRowCollection drTemp = dtTemp.AsEnumerable().Where(r => (r["weight"]).Equals(DBNull.Value));
            foreach( DataRow item in drTemp)
                item["weight"] = "0,000";

            drTemp = dtTemp.AsEnumerable().Where(r => (r["kolkor"]).Equals(DBNull.Value));
            foreach (DataRow item in drTemp)
                item["kolkor"] = "0";

            if (dtTemp.Rows.Count == 0)
            {
                return dtResult;
            }

            string Q_req_date = "";
            int Q_id_post = 0;
            string Q_post_name = "";
            decimal Q_weight = 0;
            int Q_kolkor = 0;

            for (int i = 0; dtTemp.Rows.Count > i; i++)
            {
                //первая добавляемая запись
                if (i == 0)
                {
                    Q_req_date = dtTemp.Rows[i]["req_date"].ToString();
                    Q_id_post = Convert.ToInt32(dtTemp.Rows[i]["id_post"].ToString());
                    Q_post_name = dtTemp.Rows[i]["post_name"].ToString();
                    Q_weight = decimal.Parse(dtTemp.Rows[i]["weight"].ToString());
                    Q_kolkor = Convert.ToInt32(dtTemp.Rows[i]["kolkor"].ToString());

                    //если это последняя и единственная запись
                    if (dtTemp.Rows.Count - 1 == i)
                    {
                        dtResult.Rows.Add(Q_req_date, Q_post_name, Q_weight, Q_kolkor);
                    }
                }
                else
                {
                    //если у новой записи та же дата
                    if (Q_req_date == dtTemp.Rows[i]["req_date"].ToString())
                    {
                        //если у новой записи тот же поставщик 
                        if (Q_id_post == Convert.ToInt32(dtTemp.Rows[i]["id_post"].ToString()))
                        {
                            Q_weight += decimal.Parse(dtTemp.Rows[i]["weight"].ToString());
                            Q_kolkor += Convert.ToInt32(dtTemp.Rows[i]["kolkor"].ToString());
                        }
                        else
                        {
                            dtResult.Rows.Add(Q_req_date, Q_post_name, Q_weight, Q_kolkor);

                            Q_req_date = dtTemp.Rows[i]["req_date"].ToString();
                            Q_id_post = Convert.ToInt32(dtTemp.Rows[i]["id_post"].ToString());
                            Q_post_name = dtTemp.Rows[i]["post_name"].ToString();
                            Q_weight = decimal.Parse(dtTemp.Rows[i]["weight"].ToString());
                            Q_kolkor = Convert.ToInt32(dtTemp.Rows[i]["kolkor"].ToString());
                        }
                    }
                    //если у новой записи дата отличается
                    else
                    {
                        dtResult.Rows.Add(Q_req_date, Q_post_name, Q_weight, Q_kolkor);

                        Q_req_date = dtTemp.Rows[i]["req_date"].ToString();
                        Q_id_post = Convert.ToInt32(dtTemp.Rows[i]["id_post"].ToString());
                        Q_post_name = dtTemp.Rows[i]["post_name"].ToString();
                        Q_weight = decimal.Parse(dtTemp.Rows[i]["weight"].ToString());
                        Q_kolkor = Convert.ToInt32(dtTemp.Rows[i]["kolkor"].ToString());
                    }

                    //если это последняя запись
                    if (dt.Rows.Count - 1 == i)
                    {
                        dtResult.Rows.Add(Q_req_date, Q_post_name, Q_weight, Q_kolkor);
                    }
                }
            }

            return dtResult;

        }

        /// <summary>
        /// Получение номера колонки по порядку
        /// </summary>
        /// <param name="grd">Грид</param>
        /// <param name="colName">Им колонки</param>
        /// <returns>Номер колонки</returns>
        public static int getExcelColIndex(DataGridView grd, string colName)
        {
            int colIndex = 1;
            int i = 0;

            while (grd.Columns[i].Name != colName)
            {
                if (grd.Columns[i].Visible && !grd.Columns[i].GetType().Equals(typeof(DataGridViewCheckBoxColumn)))
                {
                    colIndex++;
                }
                i++;
            }

            return colIndex;
        }

        /// <summary>
        /// Задает формат видимой колонки
        /// </summary>
        /// <param name="Rep">Отчет</param>
        /// <param name="rowStart">С позиции строки</param>
        /// <param name="colStart">С позиции столбца</param>
        /// <param name="rowEnd">По позицию строки</param>
        /// <param name="colEnd">По позицию столбца</param>
        /// <param name="format">Формат</param>
        /// <param name="isVis">Видима ли колонка</param>
        public static void setColumnFormat(Nwuram.Framework.ToExcel.HandmadeReport Rep, int rowStart, int colStart, int rowEnd, int colEnd, string format, bool isVis)
        {
            if (isVis)
            {
                Rep.SetFormat(rowStart, colStart, rowEnd, colEnd, format);
            }
        }

        public static void setColumnFormat(Nwuram.Framework.ToExcelNew.ExcelUnLoad Rep, int rowStart, int colStart, int rowEnd, int colEnd, string format, bool isVis)
        {
            if (isVis)
            {
                Rep.SetFormat(rowStart, colStart, rowEnd, colEnd, format);
            }
        }

        #endregion

        public static bool isColumnsWider(DataGridView grd)
        {
            int colWidth = 0;

            foreach (DataGridViewColumn col in grd.Columns)
            {
                if (col.Visible)
                {
                    colWidth += col.Width;
                }
            }

            if (colWidth > grd.Width)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetStringFromRow(DataTable dtGoods, string colName)
        {
            if (dtGoods == null || dtGoods.Rows.Count == 0)
                return "-1";
            string id_goods = "";
            foreach (DataRow dRow in dtGoods.Rows)
            {
                id_goods += dRow[colName] + ",";
            }

            return id_goods.Remove(id_goods.Length - 1);
        }

        public static string GetStringFromRow(DataRow[] dtGoods, string colName)
        {
            if (dtGoods.Count() == 0)
                return "-1";
            string id_goods = "";
            foreach (DataRow dRow in dtGoods)
            {
                id_goods += dRow[colName] + ",";
            }

            return id_goods.Remove(id_goods.Length - 1);
        }

        public static string GetStringFromArray(ArrayList dtGoods)
        {
            if (dtGoods.Count > 0)
            {
                string id_goods = "";
                foreach (int ids in dtGoods)
                {
                    id_goods += ids.ToString() + ",";
                }

                return id_goods.Remove(id_goods.Length - 1);
            }

            return "";
        }

        public static bool CheckProperty(string query)
        {
            bool result = false;
            if (dtProperties != null
                && dtProperties.Rows.Count > 0)
                result = (dtProperties.Select(query).Count() > 0);
            return result;
        }

        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            //Taxes: Remote Desktop Connection and painting
            //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }

        /// <summary>
        /// Проверка ограничений
        /// </summary>
        /// <param name="dtReqGoods">Тело заявки</param>
        /// <param name="id_dep">Отдел заявки</param>
        /// <param name="idTReq">Номер заявки</param>
        /// <param name="dateOut">Дата выдачи</param>
        /// <param name="saveAsDraft">Принудительное сохранение как черновик</param>
        /// <param name="idToDelete">товары для удаления</param>
        /// <returns>Результат: 0 - ограничения пройдены
        ///                     1 - сохранение черновика
        ///                     2 - заявка не будет сохранена</returns>
        public static int checkRestriction(ref DataTable dtReqGoods, int id_dep, int idTReq, DateTime dateOut, bool saveAsDraft, out string idToDelete)
        {
            string errors = "";
            DateTime? beginOfPeriod;
            DateTime? endOfPeriod;
            decimal PlanRealiz;
            decimal? TFRuleZakaz;
            int result = 0;
            idToDelete = "";
            DataRow[] drGoodZakaz = null;
            DataTable dtTFRules = null;

            if (dtReqGoods.Select("nprimech <> 1").Count() > 0)
            {
                //Получаем группы пересорта для товаров
                dtTFRules = id_dep != 6 ? Config.hCntMain.GetTFGoodsRules(Config.GetStringFromRow(dtReqGoods.Select("nprimech = 0"), "id_tovar"))
                                                  : Config.hCntAdd.GetTFGoodsRules(Config.GetStringFromRow(dtReqGoods.Select("nprimech = 0"), "id_tovar"));
            }

            if(dtTFRules != null)
                //Определяем заказы по товарам из групп пересорта
                foreach (DataRow drRule in dtTFRules.Rows)
                {
                    drGoodZakaz = dtReqGoods.Select("id_tovar = " + drRule["id_tovar"].ToString());
                    if (drGoodZakaz != null && drGoodZakaz.Count() > 0)
                    {
                        drRule["zakaz"] = drGoodZakaz.AsEnumerable().Sum(x => x.Field<decimal>("zakaz"));
                    }
                }
            

            foreach (DataRow drGood in dtReqGoods.Select("zakaz <> 0"))
            {
                beginOfPeriod = endOfPeriod = null;
                TFRuleZakaz = null;

                //drGood["CauseOfDecline"] = "";
                if (drGood["BeginOfPeriod"].ToString().Length != 0)
                {
                    beginOfPeriod = DateTime.Parse(drGood["BeginOfPeriod"].ToString());
                }

                if (drGood["EndOfPeriod"].ToString().Length != 0)
                {
                    endOfPeriod = DateTime.Parse(drGood["EndOfPeriod"].ToString());
                }

                PlanRealiz = ((decimal)drGood["PlanRealiz"] == -1 ? 0 : (decimal)drGood["PlanRealiz"]);
                if (dtTFRules != null && dtTFRules.Rows.Count > 0)
                {
                    drGoodZakaz = dtTFRules.Select("id_tovar = " + drGood["id_tovar"].ToString());
                }

                //Определяем суммарный заказ по товарам, принадлежащим однй группе пересорта
                if (drGoodZakaz != null && drGoodZakaz.Count() > 0)
                {
                    drGoodZakaz = dtTFRules.Select("id_rule = " + drGoodZakaz[0]["id_rule"].ToString());
                    if (drGoodZakaz != null && drGoodZakaz.Count() > 0)
                    {
                        TFRuleZakaz = drGoodZakaz.AsEnumerable().Sum(x => x.Field<decimal>("zakaz"));
                    }
                }

                DataTable dtErrMsg = (id_dep != 6 ? Config.hCntMain.CheckGoodsLimitation(idTReq, dateOut, drGood["ean"].ToString().Trim(), int.Parse(drGood["zapas2"].ToString()), beginOfPeriod, endOfPeriod, PlanRealiz, (decimal)drGood["zakaz"], (int)drGood["id_grp2"], TFRuleZakaz)
                                                  : Config.hCntAdd.CheckGoodsLimitation(idTReq, dateOut, drGood["ean"].ToString().Trim(), int.Parse(drGood["zapas2"].ToString()), beginOfPeriod, endOfPeriod, PlanRealiz, (decimal)drGood["zakaz"], (int)drGood["id_grp2"], TFRuleZakaz));

                if (dtErrMsg != null
                    && dtErrMsg.Rows.Count > 0
                    && (int)dtErrMsg.Rows[0]["code"] != 0)
                {
                    errors += (drGood["ean"].ToString().Trim().Length < 13 ? drGood["ean"].ToString().Trim().PadLeft(26 - drGood["ean"].ToString().Trim().Length) : drGood["ean"].ToString().Trim()) + " " + dtErrMsg.Rows[0]["message"].ToString().Trim().Trim(',') + "\n";
                    idToDelete += (idToDelete.Length > 0 ? ", " : "") + drGood["idReq"].ToString().Trim();
                    drGood["CauseOfDecline"] = dtErrMsg.Rows[0]["message"].ToString().Trim().Trim(',');
                    drGood["limitType"] = dtErrMsg.Rows[0]["code"];
                }
            }
            dtReqGoods.AcceptChanges();

            if (errors.Length != 0)
            {
                if (saveAsDraft ||
                    MessageBox.Show("В заявке есть товары с количеством заказа\nпревышающим разрешенное количество!\n" + errors + "Сохранить заявку как черновик?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (saveAsDraft)
                        MessageBox.Show("В заявке есть товары с количеством заказа\nпревышающим разрешенное количество!\n" + errors + "Заявка будет сохранена как черновик!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    result = 1;
                }
                else
                {
                    result = 2;
                }

            }
            return result;
        }
 
        //new 27.07.2017

        public static bool checkStatusTovarInBases(string ean, int id_deps)
        {
            DataTable dtTmp = hCntMain.checkStatusTovarInBases(ean, id_deps);
            if (dtTmp == null || dtTmp.Rows.Count == 0) return false;

            dtTmp = hCntShop2.checkStatusTovarInBases(ean, id_deps);
            if (dtTmp == null || dtTmp.Rows.Count == 0) return false;

            return true;
        }

        public static DataTable dtSelectedTovar;

        public static void genSelectedTovar(int id)
        {
            dtSelectedTovar = new DataTable();

            //dtSelectedTovar.Columns.Add("id_trequest",typeof(Int32));
            dtSelectedTovar.Columns.Add("id_prihod", typeof(Int32));
            dtSelectedTovar.Columns.Add("id_tovar", typeof(Int32));
            dtSelectedTovar.Columns.Add("Netto",typeof(decimal));
            dtSelectedTovar.Columns.Add("zcena",typeof(decimal));            
            //dtSelectedTovar.Columns.Add();

            DataTable dtListTovarWeInOut = Config.hCntMain.getWeInOutTovarList(0, DateTime.Now, DateTime.Now, "", "", id, 4);

            foreach (DataRow r in dtListTovarWeInOut.Rows)
            {
                dtSelectedTovar.Rows.Add(r["id_prihod"],
                    r["id_tovar"],
                    r["Netto"],
                    r["zcena"]);
            }

            dtSelectedTovar.AcceptChanges();
        }

        public static void clearTovarSelectedTovar(int id_tovar)
        {
            DataRow[] row = dtSelectedTovar.Select("id_tovar = " + id_tovar);

            foreach (DataRow r in row)
                dtSelectedTovar.Rows.Remove(r);

            dtSelectedTovar.AcceptChanges();
        }

        public static void saveTovarSelected(int idTReq)
        {

            Config.hCntMain.setWeInOutTovarList(idTReq, 0, 0, 0, 0, 2);
            dtSelectedTovar.AcceptChanges();


            foreach (DataRow r in dtSelectedTovar.Rows)
            {
                int id_prihod = int.Parse(r["id_prihod"].ToString());
                int id_tovar = int.Parse(r["id_tovar"].ToString());
                decimal Netto = decimal.Parse(r["Netto"].ToString());
                decimal zcena = decimal.Parse(r["zcena"].ToString());

                Config.hCntMain.setWeInOutTovarList(idTReq, id_prihod, id_tovar, Netto, zcena, 1);
            }            
        }
    }
}
