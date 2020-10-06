using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.Settings.Connection;
using System.Collections;

namespace Requests
{
    class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string username, string password, string appName)
            : base(server, database, username, password, appName)
        {
        }

        ArrayList ap = new ArrayList();
                
        /// <summary>
        /// Получение списка отделов
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeps()
        {
            ap.Clear();
            ap.Add(UserSettings.User.StatusCode == "КД" || UserSettings.User.StatusCode == "КНТ" || UserSettings.User.StatusCode == "ПР");
            ap.Add(UserSettings.User.Id);
            ap.Add(ConnectionSettings.GetIdProgram());
            return executeProcedure("[Requests].[GetDeps]", new string[3] { "@is_KD", "@id_user", "@id_prog" }, new DbType[3] { DbType.Boolean, DbType.Int32, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение списка менеджеров
        /// </summary>
        /// <param name="id_dep">ID отдела</param>
        /// <returns></returns>
        public DataTable GetManagers(int id_dep)
        {
            ap.Clear();
            ap.Add(id_dep);
            ap.Add(ConnectionSettings.GetIdProgram());
            ap.Add(UserSettings.User.Id);
            return executeProcedure("[Requests].[GetManagers]", new string[3] { "@id_dep", "@id_prog", "@id_user"}, new DbType[3] { DbType.Int32, DbType.Int32, DbType.Int32 } ,ap);
        }

        /// <summary>
        /// Получение списка менеджеров
        /// </summary>
        /// <param name="id_dep">ID отдела</param>
        /// <returns></returns>
        public DataTable GetManagers(int id_dep, bool showFilterString)
        {
            ap.Clear();
            ap.Add(id_dep);
            ap.Add(ConnectionSettings.GetIdProgram());
            ap.Add(UserSettings.User.Id);
            ap.Add(showFilterString);
            return executeProcedure("[Requests].[GetManagers]", new string[4] { "@id_dep", "@id_prog", "@id_user", "@showFilterString" }, new DbType[4] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);
        }

        /// <summary>
        /// Получение типов заявок
        /// </summary>
        /// <returns></returns>
        public DataTable GetTypes(string typesId, bool isAll)
        {
            ap.Clear();
            ap.AddRange(new object[2] { typesId, isAll });
            return executeProcedure("[Requests].[GetTypes]", new string[2] { "@typesId", "@isAll" }, new DbType[2] { DbType.String, DbType.Boolean }, ap);
        }

        /// <summary>
        /// Получение типов заявок (+ строка "В соотв с фильтром")
        /// </summary>
        /// <param name="typesId">список id типов для отображения</param>
        /// <param name="isAll">признак наличия строки "Все"</param>
        /// <param name="showFilterString">признак наличия строки "В соотв с фильтром"</param>
        /// <returns></returns>
        public DataTable GetTypes(string typesId, bool isAll, bool showFilterString)
        {
            ap.Clear();
            ap.AddRange(new object[3] { typesId, isAll, showFilterString });

            return executeProcedure("[Requests].[GetTypes]",
                new string[3] { "@typesId", "@isAll", "@showFilterString" },
                new DbType[3] { DbType.String, DbType.Boolean, DbType.Boolean }, ap);
        }

        /// <summary>
        /// Получение спика заявок
        /// </summary>
        /// <param name="date_start">с даты</param>
        /// <param name="date_end">по дату</param>
        /// <param name="id_dep">отдел</param>
        /// <param name="id_operand">тип заявки</param>
        /// <param name="credit_type">тип кредитования</param>
        /// <param name="id_man">менеджер заявки</param>
        /// <param name="ean">ean товара, находящегося в заявке</param>
        /// <returns></returns>
        public DataTable GetRequests(DateTime date_start, DateTime date_end, int id_dep, int id_operand, int credit_type, int id_man, string ean)
        {
            ArrayList apReq = new ArrayList();
            apReq.Clear();
            apReq.AddRange(new object[10] { date_start, date_end, id_dep, id_operand, UserSettings.User.Id, credit_type, ConnectionSettings.GetIdProgram(), (UserSettings.User.StatusCode == "МН" /*|| UserSettings.User.StatusCode == "ДМН"*/ ? 0 : 1), id_man, ean });
            return executeProcedure("[Requests].[GetRequests]", new string[10] { "@date_out_start", "@date_out_end", "@id_dep", "@id_operand", "@id_user", "@credit_type", "@id_prog", "@status", "@id_man", "@ean" },
                                                                new DbType[10] { DbType.DateTime, DbType.DateTime, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.String }, apReq);
        }

        /// <summary>
        /// Получение/сохранение настройки фильтров(фильты по статусам)
        /// </summary>
        /// <param name="isSave">t - сохранение, f - получение</param>
        /// <returns></returns>
        public string SetFilterSettings(bool isSave, string value)
        {
            ap.Clear();
            ap.AddRange(new object[4] { value, ConnectionSettings.GetIdProgram(), UserSettings.User.Id, isSave });
            DataTable result = executeProcedure("[Requests].[SetFilterSettings]", new string[4] { "@value", "@id_prog", "@id_user", "@is_save" }, new DbType[4] { DbType.String, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);
            if (isSave)
            {
                return "";
            }
            else
            {
                if (result != null && result.Rows.Count > 0)
                {
                    return result.Rows[0][0].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// Получение статусов заявок
        /// </summary>
        /// <returns></returns>
        public DataTable GetReqStatuses()
        {
            ap.Clear();
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.StatusCode == "МН" /*|| Nwuram.Framework.Settings.User.UserSettings.User.StatusCode == "ДМН"*/ ? 0 : 1);
            return executeProcedure("[Requests].[GetReqStatuses]", new string[1] { "@status" }, new DbType[1] { DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение с сервера текущей даты
        /// </summary>
        /// <returns></returns>
        public DateTime GetCurDate(bool isTime)
        {
            ap.Clear();
            ap.Add(isTime);
            return DateTime.Parse(executeProcedure("[Requests].[GetCurDate]", new string[1] { "@time" }, new DbType[1] { DbType.Boolean }, ap).Rows[0][0].ToString());
        }

        /// <summary>
        /// Удаление заявки
        /// </summary>
        /// <param name="id">id заявки</param>
        public void DeleteRequest(int id)
        {
            ap.Clear();
            ap.AddRange(new object[] { id, UserSettings.User.Id });
            executeProcedure("[Requests].[DeleteRequest]", new string[] { "@id", "@id_user" }, new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        ///// <summary>
        ///// Смена даты выдачи заявки
        ///// </summary>
        ///// <param name="id">id в trequest</param>
        ///// <param name="newDate">новая дата выдачи</param>
        //public void ChangeReqDate(int id, DateTime newDate)
        //{
        //    ap.Clear();
        //    ap.AddRange(new object[2] { id, newDate });
        //    executeProcedure("[Requests].[ChangeReqDate]", new string[2] { "@id", "@newDate" }, new DbType[2] { DbType.Int32, DbType.DateTime }, ap);
        //}

        ///// <summary>
        ///// Копирование заявки (возможно не понадобиться)
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="isUpdate"></param>
        //public void CopyRequest(int id, bool isUpdate)
        //{
        //    ap.Clear();
        //    ap.AddRange(new object[2] { id, isUpdate });
        //    executeProcedure("[Requests].[CopyRequest]", new string[2] { "@id", "@isUpdate" }, new DbType[2] { DbType.Int32, DbType.Boolean }, ap);
        //}

        /// <summary>
        /// Получение списка окон
        /// </summary>
        /// <param name="id">id отдела</param>
        /// <returns></returns>
        public DataTable GetWindows(int id_dep)
        {
            ap.Clear();
            ap.Add(id_dep);
            return executeProcedure("[Requests].[GetWindows]", new string[1] { "@id_dep" }, new DbType[1] { DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение списка ЮЛ
        /// </summary>
        /// <param name="idDep">id отдела</param>
        /// <param name="date">на дату</param>
        /// <returns></returns>
        public DataTable GetUl(int idDep, DateTime date, int id_post, bool isView, bool isBonus)
        {
            ap.Clear();
            ap.AddRange(new object[5] { idDep, date, id_post, isView, isBonus });
            return executeProcedure("[Requests].[GetUL]", new string[5] { "@idDep", "@date", "@id_post", "@isView", "@isBonus" }, new DbType[5] { DbType.Int32, DbType.DateTime, DbType.Int32, DbType.Boolean, DbType.Boolean }, ap);
        }

        /// <summary>
        /// Получение списка типов бонусов
        /// </summary>
        /// <returns></returns>
        public DataTable GetBonusTypes(bool isAll)
        {
            ap.Clear();
            ap.Add(isAll);
            return executeProcedure("[Requests].[GetBonusTypes]", new string[1] { "@isAll" }, new DbType[1] { DbType.Boolean }, ap);
        }

        /// <summary>
        /// Получение параметров смешаного бонуса
        /// </summary>
        /// <param name="id_req">номер заявки</param>
        /// <returns></returns>
        public DataTable GetBonusProp(int id_req)
        {
            ap.Clear();
            ap.Add(id_req);
            return executeProcedure("[Requests].[GetBonusProp]", new string[1] { "@id_req" }, new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable GetRequestBody(int id_trequest, bool forCopy,DateTime dateToRCena = new DateTime())
        {
            if (dateToRCena == new DateTime())
                dateToRCena = DateTime.Now;
            
            ArrayList apReqBody = new ArrayList();
            apReqBody.Clear();
            apReqBody.AddRange(new object[6] { id_trequest, (UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН" ? 0 : 1), UserSettings.User.Id, forCopy, ConnectionSettings.GetIdProgram(), dateToRCena });
            return executeProcedure("[Requests].[GetRequestBody]",
                new string[6] { "@id_trequest", "@status", "@id_user", "@forCopy", "@id_prog","@dateToRCena" }, 
                new DbType[6] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Boolean, DbType.Int32,DbType.DateTime }, apReqBody);

        }

        /// <summary>
        /// Получение текущего остатка товара
        /// </summary>
        /// <param name="id_tovar">id товара</param>
        /// <param name="id_dep_req">id отдела заявки</param>
        /// <returns></returns>
        public decimal GetTekOst(int id_tovar, int id_dep_req)
        {
            ap.Clear();
            ap.AddRange(new object[2] { id_tovar, id_dep_req });
            decimal ret = 0;
            if (decimal.TryParse(executeProcedure("[Requests].[GetTekOst]", new string[2] { "@id_tovar", "@id_dep_req" }, new DbType[2] { DbType.Int32, DbType.Int32 }, ap).Rows[0][0].ToString(), out ret))
            {
                return ret;
            }

            return 0;
        }

        /// <summary>
        /// Получение среднего расхода по товару
        /// </summary>
        /// <param name="id_req">id строки в заявке</param>
        /// <returns></returns>
        public decimal GetAvgRealiz(int id_req)
        {
            ap.Clear();
            ap.Add(id_req);
            decimal ret;

            if (decimal.TryParse(executeProcedure("[Requests].[GetAvgReal]", new string[1] { "@id_request" }, new DbType[1] { DbType.Int32 }, ap).Rows[0][0].ToString(), out ret))
            {
                return ret;
            }
            return 0;
        }

        /// <summary>
        /// Получение настроек программы из property_list
        /// </summary>
        /// <returns></returns>
        public DataTable GetProperties()
        {
            ap.Clear();
            ap.AddRange(new object[2] { ConnectionSettings.GetIdProgram(), UserSettings.User.Id });
            return executeProcedure("[Requests].[GetProperties]", new string[2] { "@id_prog", "@id_user" }, new DbType[2] { DbType.Int32, DbType.Int32 }, ap);
        }

        public bool GetProperties(string id_val)
        {
            ap.Clear();
            ap.Add(ConnectionSettings.GetIdProgram());
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);

            DataTable dt = new DataTable();

            dt = executeProcedure("[Requests].[GetProperties]",
                new string[2] { "@id_prog", "@id_user" },
                new DbType[2] { DbType.Int32, DbType.Int32 }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    if ((dt.Rows[i]["id_val"].ToString() == id_val) && (dt.Rows[i]["val"].ToString() == "1"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Сохранение текущих настроек формы
        /// </summary>
        /// <param name="id_val">id_val в property_list</param>
        /// <param name="val">значение</param>
        public void SetDefaultDates(string id_val, string val)
        {
            ap.Clear();
            ap.AddRange(new object[4] { ConnectionSettings.GetIdProgram(), UserSettings.User.Id, id_val, val });
            executeProcedure("[Requests].[SetProperties]", new string[4] { "@id_prog", "@id_user", "@id_val", "@val" }, new DbType[4] { DbType.Int32, DbType.Int32, DbType.String, DbType.String }, ap);
        }

        /// <summary>
        /// Получение списка ТУ групп
        /// </summary>
        /// <param name="id_dep">id отдела</param>
        /// <returns></returns>
        public DataTable GetTU(int id_dep)
        {
            string code = UserSettings.User.StatusCode;
            ap.Clear();
            ap.Add(code == "МН" || code == "ДМН" ? 0 : (code == "КД" || code == "КНТ" || code == "ПР" ? 1 : (code == "РКВ" ? 2 : -1)));
            ap.AddRange(new object[3] { UserSettings.User.Id, id_dep, ConnectionSettings.GetIdProgram() });
            return executeProcedure("[Requests].[GetTU]", new string[4] { "@id_status", "@id_user", "@id_otdel", "@id_prog" }, new DbType[4] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение списка ТУ групп
        /// </summary>
        /// <param name="id_dep">id отдела</param>
        /// <returns></returns>
        public DataTable GetTU(int id_dep, bool isNew, bool notTakeNew)
        {
            string code = UserSettings.User.StatusCode;
            ap.Clear();
            ap.Add(code == "МН" || code == "ДМН" ? 0 : (code == "КД" || code == "КНТ" || code == "ПР" ? 1 : (code == "РКВ" ? 2 : -1)));
            ap.AddRange(new object[5] { UserSettings.User.Id, id_dep, ConnectionSettings.GetIdProgram(), isNew, notTakeNew });
            return executeProcedure("[Requests].[GetTU]", new string[6] { "@id_status", "@id_user", "@id_otdel", "@id_prog", "@isNew", "@notTakeNew" }, new DbType[6] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Boolean, DbType.Boolean }, ap);
        } 
        
        /// <summary>
        /// Получение списка ТУ групп
        /// </summary>
        /// <param name="id_dep">id отдела</param>
        /// <returns></returns>
        public DataTable GetTU(int id_dep, bool showFilterString)
        {
            string code = UserSettings.User.StatusCode;
            ap.Clear();
            ap.Add(code == "МН" || code == "ДМН" ? 0 : (code == "КД" || code == "КНТ" || code == "ПР" ? 1 : (code == "РКВ" ? 2 : -1)));
            ap.AddRange(new object[4] { UserSettings.User.Id, id_dep, ConnectionSettings.GetIdProgram(), showFilterString });
            return executeProcedure("[Requests].[GetTU]", new string[5] { "@id_status", "@id_user", "@id_otdel", "@id_prog", "showFilterString" }, new DbType[5] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);
        }

        /// <summary>
        /// Получение списка ТУ групп
        /// </summary>
        /// <param name="id_dep">id отдела</param>
        /// <returns></returns>
        public DataTable GetTUForManager(int id_dep, int id_Manager)
        {
            string code = UserSettings.User.StatusCode;
            ap.Clear();
            ap.AddRange(new object[5] { 0, id_Manager, id_dep, ConnectionSettings.GetIdProgram(), false });
            return executeProcedure("[Requests].[GetTU]", new string[5] { "@id_status", "@id_user", "@id_otdel", "@id_prog", "showFilterString" }, new DbType[5] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);
        }

        /// <summary>
        /// Получение списка инв. групп
        /// </summary>
        /// <param name="id_dep">id отдела</param>
        /// <returns></returns>
        public DataTable GetInvGrp(int id_dep)
        {
            string code = UserSettings.User.StatusCode;
            ap.Clear();
            ap.AddRange(new object[3] { (code == "МН" || code == "ДМН" ? 0 : (code == "КД" || code == "КНТ" || code == "ПР" ? 1 : (code == "РКВ" ? 2 : -1))),
                                         UserSettings.User.Id, id_dep});
            return executeProcedure("[Requests].[GetGroup]", new string[3] { "@id_status", "@id_user", "@id_otdel" }, new DbType[3] { DbType.Int32, DbType.Int32, DbType.Int32 }, ap);

        }

        /// <summary>
        /// Получение списка инв. групп
        /// </summary>
        /// <param name="id_dep">id отдела</param>
        /// <param name="id_grp1">id ТУ группы</param>
        /// <param name="id_unit">тип товара</param>
        /// <returns></returns>
        public DataTable GetInvGrp(int id_dep, int id_grp1, int id_unit)
        {
            string code = UserSettings.User.StatusCode;
            ap.Clear();
            ap.AddRange(new object[5] { (code == "МН" || code == "ДМН" ? 0 : (code == "КД" || code == "КНТ" || code == "ПР" ? 1 : (code == "РКВ" ? 2 : -1))),
                                         UserSettings.User.Id, id_dep, id_grp1, id_unit});
            return executeProcedure("[Requests].[GetGroup]", new string[5] { "@id_status", "@id_user", "@id_otdel", "@id_grp1", "@id_unit" }, new DbType[5] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32 }, ap);

        }

        /// <summary>
        /// Получение списка инв. групп
        /// </summary>
        /// <param name="id_dep">id отдела</param>
        /// <returns></returns>
        public DataTable GetInvGrp(int id_dep, bool showFilterString)
        {
            string code = UserSettings.User.StatusCode;
            ap.Clear();
            ap.AddRange(new object[4] { (code == "МН" || code == "ДМН" ? 0 : (code == "КД" || code == "КНТ" || code == "ПР" ? 1 : (code == "РКВ" ? 2 : -1))),
                                         UserSettings.User.Id, id_dep, showFilterString});
            return executeProcedure("[Requests].[GetGroup]", new string[4] { "@id_status", "@id_user", "@id_otdel", "@showFilterString" }, new DbType[4] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);

        }

        /// <summary>
        /// Получение списка инв. групп
        /// </summary>
        /// <param name="id_dep">id отдела</param>
        /// <returns></returns>
        public DataTable GetInvGrpForManager(int id_dep, int id_Manager)
        {
            string code = UserSettings.User.StatusCode;
            ap.Clear();
            ap.AddRange(new object[4] { 0, id_Manager, id_dep, false});
            return executeProcedure("[Requests].[GetGroup]", new string[4] { "@id_status", "@id_user", "@id_otdel", "@showFilterString" }, new DbType[4] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Boolean }, ap);
        }

        /// <summary>
        /// Получение списка подгрупп
        /// </summary>
        /// <param name="id_dep">id отдела</param>
        /// <returns></returns>
        public DataTable GetSubGrp(int id_dep)
        {
            ap.Clear();
            ap.Add(id_dep);
            return executeProcedure("[Requests].[GetGrp3]", new string[1] { "@id_otdel" }, new DbType[1] { DbType.Int32 }, ap);
        } 
        
        /// <summary>
        /// Получение списка подгрупп
        /// </summary>
        /// <param name="id_dep">id отдела</param>
        /// <returns></returns>
        public DataTable GetSubGrp(int id_dep, bool showFilterString)
        {
            ap.Clear();
            ap.Add(id_dep);
            ap.Add(showFilterString);
            return executeProcedure("[Requests].[GetGrp3]", new string[2] { "@id_otdel", "@showFilterString" }, new DbType[2] { DbType.Int32, DbType.Boolean }, ap);
        }

        /// <summary>
        /// Получение списка товаров
        /// </summary>
        /// <param name="id_dep">id отдела</param>
        /// <param name="id_grp1">id ТУ группы</param>
        /// <param name="id_grp2">id Инв. группы</param>
        /// <param name="id_grp3">id подгруппы</param>
        /// <param name="id_post">id поставщика</param>
        /// <param name="id_oper1">id менеджера</param> 
        /// <returns></returns>
        public DataTable GetGoods(int id_dep, int id_grp1, int id_grp2, int id_grp3, int id_post, int id_oper1)
        {
            ArrayList apGoods = new ArrayList();
            apGoods.Clear();
            apGoods.AddRange(new object[9] { (UserSettings.User.StatusCode != "МН" && UserSettings.User.StatusCode != "ДМН"/* || UserSettings.User.StatusCode == "КНТ" || UserSettings.User.StatusCode == "ПР"*/ ? 1 : 0),
                                      ConnectionSettings.GetIdProgram(),
                                      UserSettings.User.Id,
                                      id_dep,
                                      id_grp1,
                                      id_grp2,
                                      id_grp3,
                                      id_post,
                                      id_oper1});
            return executeProcedure("[Requests].[GetGoods]", new string[9] { "@id_status", "@id_prog", "@id_user", "@id_dep", "@id_grp1", "@id_grp2", "@id_grp3", "@id_post", "@id_oper1" }, new DbType[9] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32 }, apGoods);
        }

        /// <summary>
        /// Получение полного наименования товара
        /// </summary>
        /// <param name="id_tovar">id товара </param>
        /// <returns></returns>
        public string GetFullNameTovar(int id_tovar)
        {
            ap.Clear();
            ap.Add(id_tovar);
            DataTable dtFullName = executeProcedure("[Requests].[getFullNameTovar]", new string[1] { "@id_tovar" }, new DbType[1] { DbType.Int32 }, ap);

            if (dtFullName != null && dtFullName.Rows.Count > 0)
            {
                return dtFullName.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Добавление\удаления товара из каталога, запрет на добавление товара
        /// </summary>
        /// <param name="type">Тип операции
        ///                    1 - Добавление товара в каталог
		///		               2 - Удалить товар из каталога
		///		               3 - Добавить товар в список запрещенных
		///		               4 - Удалить товар из списка запрещенных
        ///                    </param>
        /// <param name="id_tovar">id товара</param>
        public void SetGoodsPosition(int type, int id_tovar)
        {
            ap.Clear();
            ap.AddRange(new object[3] { type, id_tovar, UserSettings.User.Id });
            executeProcedure("[Requests].[SetGoodsPosition]", new string[3] { "@type", "@id_tovar", "@id_user" }, new DbType[3] { DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Добавление\удаления товара из каталога, запрет на добавление товара
        /// </summary>
        /// <param name="type">Тип операции
        ///                    1 - Добавление товара в каталог
        ///		               2 - Удалить товар из каталога
        ///		               3 - Добавить товар в список запрещенных
        ///		               4 - Удалить товар из списка запрещенных
        ///                    </param>
        /// <param name="id_tovar">id товара</param>
        /// <param name="comment">причина добавления</param>
        public void SetGoodsPosition(int type, int id_tovar, string comment)
        {
            ap.Clear();
            ap.AddRange(new object[4] { type, id_tovar, UserSettings.User.Id, comment });
            executeProcedure("[Requests].[SetGoodsPosition]", new string[4] { "@type", "@id_tovar", "@id_user", "@comment" }, new DbType[4] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.String }, ap);
        }

        /// <summary>
        /// Получение списка закупочных цен для товаров
        /// </summary>
        /// <param name="id_goods">Список id товаров</param>
        /// <returns></returns>
        public DataTable GetZcena(string id_goods)
        {
            ap.Clear();
            ap.Add(id_goods);
            return executeProcedure("[Requests].[GetZceana]", new string[1] { "@id_goods" }, new DbType[1] { DbType.String }, ap);
        }

        /// <summary>
        /// Получение данных по переоценке
        /// </summary>
        /// <param name="id_pereoc">id переоценки</param>
        /// <returns></returns>
        public DataTable GetPereoc(int id_pereoc)
        {
            ap.Clear();
            ap.Add(id_pereoc);
            return executeProcedure("[Requests].[GetPereoc]", new string[1] { "@id_pereoc" }, new DbType[1] { DbType.Int32 }, ap);
        }

        /// <summary>
        /// Сохранение заголовка переоценки
        /// </summary>
        /// <param name="id_trequest">id в j_trequest</param>
        /// <param name="id_dep">id отдела</param>
        /// <param name="cprimech">примечание</param>
        /// <returns></returns>
        public int SetPereocHead(int id_trequest, int id_dep, DateTime datePereoc, string cprimech)
        {
            ap.Clear();
            ap.AddRange(new object[5] { id_trequest, id_dep, datePereoc, cprimech, UserSettings.User.Id });
            DataTable result = executeProcedure("[Requests].[SetPereocHead]", 
                new string[] { "@id_trequest", "@id_dep", "@date", "@cprimech", "@id_user" }, 
                new DbType[] { DbType.Int32, DbType.Int32, DbType.DateTime, DbType.String, DbType.Int32 }, ap);
            int retId = (id_trequest != 0 ? id_trequest : -1);
            if (result != null && result.Rows.Count > 0)
            {
                retId = int.Parse(result.Rows[0][0].ToString());
            }
            return retId;
        }

        /// <summary>
        /// Сохранение тела переоценки
        /// </summary>
        /// <param name="id_trequest">id шапки</param>
        /// <param name="id_request">id записи в j_request</param>
        /// <param name="id_tovar">id товара</param>
        /// <param name="netto">нетто</param>
        /// <param name="zcena">цена закупки</param>
        /// <param name="rcena">цена продажи</param>
        /// <param name="zcenabnds">цена без ндс</param>
        public void SetPereocBody(int id_trequest, int id_request, int id_tovar, decimal netto, decimal zcena, decimal rcena, decimal zcenabnds)
        {
            ap.Clear();
            ap.AddRange(new object[7] { id_trequest, id_request, id_tovar, netto, zcena, rcena, zcenabnds });
            executeProcedure("[Requests].[SetPereocBody]", new string[7] { "@id_trequest", "@id_request", "@id_tovar", "@netto", "@zcena", "@rcena", "@zcenabnds" }, new DbType[7] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.Decimal }, ap);
        }

        /// <summary>
        /// Удаление записи в акте переоценки
        /// </summary>
        /// <param name="idPereoc">id акта переоценки</param>
        /// <param name="id_goods">строка с id товаров через запятую</param>
        public void DelPereocBody(int idPereoc, string id_goods)
        {
            ap.Clear();
            ap.AddRange(new object[2] { idPereoc, id_goods });
            executeProcedure("[Requests].[DelPereocBody]", new string[2] { "@id_trequest", "@id_goods" }, new DbType[2] { DbType.Int32, DbType.String }, ap);
        }

        /// <summary>
        /// Получение настроек программы
        /// </summary>
        /// <param name="id_value">id параметра</param>
        /// <returns></returns>
        public DataTable GetSettings(string id_value)
        {
            ap.Clear();
            ap.AddRange(new object[2] { ConnectionSettings.GetIdProgram(), id_value });
            return executeProcedure("[Requests].[GetSettings]", new string[2] { "@id_prog", "@id_value" }, new DbType[2] { DbType.Int32, DbType.String }, ap);
        }

        /// <summary>
        /// Получение скопированного акта переоценки
        /// </summary>
        /// <param name="id_pereoc"></param>
        /// <returns></returns>
        public DataTable CopyPereoc(int id_pereoc)
        {
            ap.Clear();
            ap.AddRange(new object[] { id_pereoc, ConnectionSettings.GetIdProgram(), UserSettings.User.Id });
            return executeProcedure("[Requests].[CopyPereoc]", new string[] { "@id_pereoc", "@id_prog", "@id_user" }, new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение настроек программы
        /// </summary>
        /// <param name="id_value">id параметра</param>
        /// <param name="defaultval">значение по умолчанию</param>
        /// <returns></returns>
        public decimal GetSettings(string id_value, decimal defaultval)
        {
            decimal res = defaultval;

            ap.Clear();
            ap.AddRange(new object[] { ConnectionSettings.GetIdProgram(), id_value });
            DataTable dt = new DataTable();

            dt = executeProcedure("[Requests].[GetSettings]",
                new string[] { "@id_prog", "@id_value" },
                new DbType[] { DbType.Int32, DbType.String }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                string val = dt.Rows[0]["value"].ToString().Replace('.', NumericSeparator());

                decimal.TryParse(val, out res);
            }

            return res;
        }

        /// <summary>
        /// Процедура получения текущего разделителя целой и дробной части числа на локальном компьютере
        /// </summary>
        /// <returns> разделитель </returns>
        public static char NumericSeparator()
        {
            //обновление информации по региональным настройкам windows
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            return char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        }

        /// <summary>
        /// Проверка заявки для копирования
        /// </summary>
        /// <param name="id_dep">id отдела</param>
        /// <param name="id_trequest">id заявки</param>
        /// <returns></returns>
        public bool CheckReqToCopy(int id_dep, int id_trequest)
        {
            bool result = false;

            ap.Clear();
            ap.AddRange(new object[4] { ConnectionSettings.GetIdProgram(), UserSettings.User.Id, id_dep, id_trequest });
            DataTable dtRes = executeProcedure("[Requests].[CheckReqToCopy]", new string[4] { "@id_prog", "@id_user", "@id_dep", "@id_trequest" }, new DbType[4] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32 }, ap);

            if (dtRes != null && dtRes.Rows.Count > 0)
            {
                result = (bool)dtRes.Rows[0][0];
            }

            return result;
        }

        /// <summary>
        /// Получение новых цен для тваров
        /// </summary>
        /// <param name="id_trequest">id заявки</param>
        /// <returns></returns>
        public DataTable GetNewPrice(int id_trequest)
        {
            ap.Clear();
            ap.Add(id_trequest);
            return executeProcedure("[Requests].[GetNewPrice]", new string[1] { "@id_trequest" }, new DbType[1] { DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение запрещенных товаров в заявке
        /// </summary>
        /// <param name="id_trequest">id заявки</param>
        /// <returns></returns>
        public DataTable GetProhToCopy(int id_trequest)
        {
            ap.Clear();
            ap.Add(id_trequest);
            return executeProcedure("[Requests].[GetProhToCopy]", new string[1] { "@id_trequest" }, new DbType[1] { DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение общего заказа для товара
        /// </summary>
        /// <param name="id_tovar">id товара</param>
        /// <param name="id_dep">id отдела</param>
        /// <returns>Общий заказ</returns>
        public decimal GetCommonOrder(int id_tovar, int id_dep)
        {
            decimal retValue = 0;
            ap.Clear();
            ap.AddRange(new object[3] { id_tovar, id_dep, ConnectionSettings.GetIdProgram() });
            DataTable dtResult = executeProcedure("[Requests].[GetCommonOrder]", new string[3] { "@id_tovar", "@id_dep_req", "@id_prog" }, new DbType[3] { DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                retValue = decimal.Parse(dtResult.Rows[0][0].ToString());
            }

            return retValue;
        }

        /// <summary>
        /// Проверка ограничений по товару
        /// </summary>
        /// <param name="id_trequest">id заявки</param>
        /// <param name="dataOut"дата заявки></param>
        /// <param name="ean">код товара</param>
        /// <param name="zapas2">Запас2</param>
        /// <param name="otsechBegin">Начало периода отсечки</param>
        /// <param name="otsechEnd">Конец периоа отсечки</param>
        /// <param name="PlanRealiz">Плановая реализация</param>
        /// <param name="tfRuleZakaz">Кол-во товара в заявке по группе пересорта</param> 
        /// <returns></returns>
        public DataTable CheckGoodsLimitation(int id_trequest, DateTime dataOut, string ean, int zapas2, DateTime? otsechBegin, DateTime? otsechEnd, decimal PlanRealiz, decimal zakaz, int id_grp2, decimal? tfRuleZakaz)
        {
            ap.Clear();
            ap.AddRange(new object[12] { ConnectionSettings.GetIdProgram(), id_trequest, dataOut, ean, zapas2, otsechBegin, otsechEnd, PlanRealiz, zakaz, (UserSettings.User.StatusCode == "МН" /*|| UserSettings.User.StatusCode == "ДМН" */? 0 : 1), id_grp2, tfRuleZakaz });
            return executeProcedure("[Requests].[CheckGoodsLimitation]", new string[12] { "@id_prog", "@id_trequest", "@dataOut", "@ean", "@zapas2", "@otsechBegin", "@otsechEnd", "@PlanRealiz", "@zakaz", "@mode", "@id_grp2n", "@tfRuleZakaz" }, new DbType[12] { DbType.Int32, DbType.Int32, DbType.DateTime, DbType.String, DbType.Int32, DbType.DateTime, DbType.DateTime, DbType.Decimal, DbType.Decimal, DbType.Int32, DbType.Int32, DbType.Decimal }, ap);
        }

        /// <summary>
        /// Сохранение шапки заявки
        /// </summary>
        /// <param name="id">id заявки</param>
        /// <param name="id_dep">отдел</param>
        /// <param name="data_out">дата выдачи</param>
        /// <param name="id_post">поставщик</param>
        /// <param name="id_man">менеджер</param>
        /// <param name="nstatus">статус</param>
        /// <param name="ntypeorg">юр. лицо</param>
        /// <param name="cprimech">примечание</param>
        /// <param name="id_operand">тип заявки</param>
        /// <param name="porthole">окно</param>
        /// <param name="id_TypeBonus">тип бонуса</param>
        /// <param name="id_unit">вес/штука</param>
        /// <param name="Credit">тип кредита</param>
        /// <param name="CreditPeriod">период кредита</param>
        /// <param name="Deficit">тов. бонус</param>
        /// <param name="Goods">недостача</param>
        /// <param name="deletedIdReq">удаляемые товары</param>
        /// <returns></returns>
        public int SetRequestHead(int id, int id_dep, DateTime data_out, int id_post, int id_man, int nstatus, int ntypeorg, string cprimech, int id_operand, int porthole, int id_TypeBonus, int id_unit, int Credit, int CreditPeriod, decimal Deficit, decimal Goods, string deletedIdReq)
        {
            int idTreq = 0;
            ap.Clear();
            ap.AddRange(new object[18] { id, id_dep, data_out, id_post, UserSettings.User.Id, id_man, nstatus, ntypeorg, cprimech, id_operand, porthole, id_TypeBonus, id_unit, Credit, CreditPeriod, Deficit, Goods, deletedIdReq });
            DataTable dtResult = executeProcedure("[Requests].[SetRequestHead]", new string[18] { "@id", "@id_dep", "@data_out", "@id_post", "@id_user", "@id_man", "@nstatus", "@ntypeorg", "@cprimech", "@id_operand", "@porthole", "@id_TypeBonus", "@id_unit", "@Credit", "@CreditPeriod", "@Deficit", "@Goods", "@deletedIdReq" },
                                                                               new DbType[18] { DbType.Int32, DbType.Int32, DbType.DateTime, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.String, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Decimal, DbType.Decimal, DbType.String }, ap);
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                idTreq = (int)dtResult.Rows[0][0];
            }

            return idTreq;
        }

        /// <summary>
        /// Сохранение тела заявки
        /// </summary>
        /// <param name="id">id строки с товаром</param>
        /// <param name="id_trequest">id заявки</param>
        /// <param name="id_tovar">товар</param>
        /// <param name="id_unit">вес/шт</param>
        /// <param name="nzatar">затарка</param>
        /// <param name="netto">заказ</param>
        /// <param name="zcena">цена закупки</param>
        /// <param name="rcena">цена продажи</param>
        /// <param name="zcenabnds">цена закупки без ндс</param>
        /// <param name="nprimech">старый/новый</param>
        /// <param name="cprimech">примечание</param>
        /// <param name="id_subject">страна/субъект</param>
        /// <param name="beginOfPeriod">начало отсечки</param>
        /// <param name="endOfPeriod">конец отсечки</param>
        /// <param name="causeOfDecline">причина неуспешного прохождения ограничений</param>
        /// <param name="PeriodOfStorage">срок хранения</param>
        /// <param name="PlanRealiz">плановая реализация</param>
        /// <param name="creditDecline">причина неуспешного прохождения ограничений по кредиту</param>
        /// <param name="id_CreditStatus">статус кредита</param>
        /// <param name="ean">еан</param>
        /// <param name="ShelfSpace">полочное пр-во</param>
        /// <param name="isTransparent">признак упаковки</param>
        /// <returns></returns>
        public int SetRequestBody(int id, int id_trequest, int id_tovar, int id_unit, decimal nzatar, decimal netto, decimal zcena, decimal rcena, decimal zcenabnds, int nprimech, string cprimech, int id_subject, DateTime? beginOfPeriod, DateTime? endOfPeriod, string causeOfDecline, string PeriodOfStorage, decimal? PlanRealiz, string creditDecline, int id_CreditStatus, string ean, decimal ShelfSpace, bool isTransparent)
        {
            int idReq = 0;
            ap.Clear();
            ap.AddRange(new object[] { id, id_trequest, id_tovar, id_unit, nzatar, netto, zcena, rcena, zcenabnds, nprimech, cprimech, id_subject, beginOfPeriod, endOfPeriod, causeOfDecline, PeriodOfStorage, PlanRealiz, creditDecline, id_CreditStatus, ean, ShelfSpace, UserSettings.User.Id, isTransparent });
            DataTable dtResult = executeProcedure("[Requests].[SetRequestBody]", new string[] { "@id", "@id_trequest", "@id_tovar", "@id_unit", "@nzatar", "@netto", "@zcena", "@rcena", "@zcenabnds", "@nprimech", "@cprimech", "@id_subject", "@beginOfPeriod", "@endOfPeriod", "@causeOfDecline", "@PeriodOfStorage", "@PlanRealiz", "@creditDecline", "@id_CreditStatus", "@ean", "@ShelfSpace", "@id_user", "@isTransparent" },
                                                                                 new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.Int32, DbType.String, DbType.Int32, DbType.DateTime, DbType.DateTime, DbType.String, DbType.String, DbType.Decimal, DbType.String, DbType.Int32, DbType.String, DbType.Decimal, DbType.Int32, DbType.Boolean }, ap);
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                idReq = (int)dtResult.Rows[0][0];
            }

            return idReq;
        }

        /// <summary>
        /// Содержит ли распродажные товары
        /// </summary>
        /// <param name="id_goods">id товаров для проверки</param>
        /// <param name="data_out">ограничение по дате</param>
        /// <returns></returns>
        public bool IsContainsSaleGoods(string id_goods, DateTime data_out)
        {
            ap.Clear();
            ap.AddRange(new object[2] {id_goods, data_out});
            DataTable dtResult = executeProcedure("[Requests].[IsContainsSaleGoods]", new string[2] { "@id_goods", "@data_out" }, new DbType[2] { DbType.String, DbType.DateTime }, ap);

            if (dtResult != null
                && dtResult.Rows.Count > 0)
                return bool.Parse(dtResult.Rows[0][0].ToString());
            else
                return false;
        }

        /// <summary>
        /// Получение списка ед.измерения
        /// </summary>
        /// <returns></returns>
        public DataTable GetUnitList()
        {
            ap.Clear();
            return executeProcedure("[Requests].[GetUnitList]",new string[0] {}, new DbType[0] {},ap);
        }

        /// <summary>
        /// Получение списка ндс
        /// </summary>
        /// <returns></returns>
        public DataTable GetNdsList()
        {
            ap.Clear();
            return executeProcedure("[Requests].[GetNdsList]", new string[0] { }, new DbType[0] { }, ap);
        }

        /// <summary>
        /// Получение списка стран/субъектов
        /// </summary>
        /// <returns></returns>
        public DataTable GetSubjectList()
        {
            ap.Clear();
            return executeProcedure("[Requests].[GetSubjectList]", new string[0] { }, new DbType[0] { }, ap);
        }

        /// <summary>
        /// Генерирует новый еан
        /// </summary>
        /// <param name="id_dep">отдел</param>
        /// <param name="ean">максимальный новый еан в заявке</param>
        /// <returns></returns>
        public int GetNewEAN(int id_dep, string ean)
        {
            ap.Clear();
            ap.AddRange(new object[2] { id_dep, ean });
            DataTable result = executeProcedure("[Requests].[GetNewEAN]", new string[2] { "@id_dep", "@ean" }, new DbType[2] { DbType.Int32, DbType.String }, ap);

            int retValue;
            if(result == null || result.Rows.Count == 0 || !int.TryParse(result.Rows[0][0].ToString(),out retValue))
            {
                retValue = 1;
            }

            return retValue;
        }

        /// <summary>
        /// Проверка нового товара на существование в s_tovar
        /// </summary>
        /// <param name="ean"></param>
        /// <returns></returns>
        public bool CheckNewGoodExists(string ean)
        {
            bool retValue = false;

            ap.Clear();
            ap.Add(ean.Trim());

            DataTable result = executeProcedure("[Requests].[CheckNewGoodExists]", new string[1] { "@ean" }, new DbType[1] { DbType.String }, ap);

            if (result != null && result.Rows.Count != 0)
            {
                retValue = (bool)result.Rows[0][0];
            }

            return retValue;
        }

        /// <summary>
        /// Получение информации по товару
        /// </summary>
        /// <param name="findPlace">место поиска (s_tovar, j_newtovar или проверка в обоих)</param>
        /// <param name="ean">еан</param>
        /// <param name="idDepReq">отдел</param>
        /// <returns></returns>
        public DataTable GetGoodInformation(int findPlace, string ean, int idDepReq, int idOperReq, int idPost)
        {
            ap.Clear();
            ap.AddRange(new object[8] { findPlace, ean, idDepReq, idOperReq, UserSettings.User.Id, (UserSettings.User.StatusCode == "МН" || UserSettings.User.StatusCode == "ДМН" ? 0 : 1), ConnectionSettings.GetIdProgram(), idPost });
            return executeProcedure("[Requests].[GetGoodInformation]", new string[8] { "@findPlace", "@ean", "@idDepReq", "@idOperReq", "@idUser", "@userMode", "@idProg", "@id_post" }, new DbType[8] { DbType.Int32, DbType.String, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Изменяет значение затарки в справочнике затарки
        /// </summary>
        /// <param name="id_tovar">товар</param>
        /// <param name="zatar">новое значение затарки</param>
        public void ChangeZatar(int id_tovar, decimal zatar)
        {
            ap.Clear();
            ap.AddRange(new object[3] { id_tovar, zatar, UserSettings.User.Id });
            executeProcedure("[Requests].[ChangeZatar]", 
                new string[3] { "@id_tovar", "@zatar", "@id_user" }, 
                new DbType[3] { DbType.Int32, DbType.Decimal , DbType.Int32 }, ap);
        }

        /// <summary>
        /// Изменяет значение признака упаковки в справочнике затарки
        /// </summary>
        /// <param name="id_tovar">id товара</param>
        /// <param name="isTransparent">признак упаковки</param>
        public void ChangeTransparent(int id_tovar, bool isTransparent)
        {
            ap.Clear();
            ap.AddRange(new object[3] { id_tovar, isTransparent, UserSettings.User.Id });
            executeProcedure("[Requests].[ChangeTransparent]",
                new string[3] { "@id_tovar", "@isTransparent", "@id_user" },
                new DbType[3] { DbType.Int32, DbType.Boolean , DbType.Int32 }, ap);
        }

        /// <summary>
        /// Добавляет новый товар
        /// </summary>
        /// <param name="cname">название</param>
        /// <param name="ean">еан</param>
        /// <param name="id_grp1">ту группа</param>
        /// <param name="id_grp2">инв. группа</param>
        /// <param name="ntypeorg">юр. лицо</param>
        /// <param name="brutto">брутто</param>
        /// <returns></returns>
        public DataTable AddNewGood(string cname, string ean, int id_grp1, int id_grp2, int ntypeorg, decimal brutto)
        {
            ap.Clear();
            ap.AddRange(new object[6] { cname, ean, id_grp1, id_grp2, ntypeorg, brutto });
            return executeProcedure("[Requests].[AddNewGood]",
                new string[6] { "@cname", "@ean", "@id_grp1", "@id_grp2", "@ntypeorg", "@brutto" }, 
                new DbType[6] { DbType.String, DbType.String, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Decimal }, ap);
        }

        public DataTable EditNewGood(int id, decimal brutto)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(brutto);

            return executeProcedure("[Requests].[EditNewGood]",
                new string[] { "@id", "@brutto" },
                new DbType[] { DbType.Int32, DbType.Decimal }, ap);
        }

        public void UpdateNewGoodNtypeorg(int id_tovar, int ntypeorg)
        {
            ap.Clear();
            ap.AddRange(new object[] { id_tovar, ntypeorg });
            executeProcedure("Requests.UpdateNewGoodNtypeorg", new string[] { "@id_tovar", "@ntypeorg" }, new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Возвращает даты периода отсечки товара
        /// </summary>
        /// <param name="id_tovar">товар</param>
        /// <returns></returns>
        public DataTable GetGoodODates(int id_tovar)
        {
            ap.Clear();
            ap.AddRange(new object[3] { id_tovar, ConnectionSettings.GetIdProgram(), UserSettings.User.Id });
            return executeProcedure("[Requests].[GetGoodODates]", new string[3] { "@idGood", "@idProg", "@idUser" }, new DbType[3] { DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получает информацию о списке товаров
        /// </summary>
        /// <param name="id_goods">id товаров через запятую</param>
        /// <returns></returns>
        public DataTable GetMassGoodsInformation(string id_goods, int id_dep, int id_post)
        {
            ap.Clear();
            ap.AddRange(new object[5] { id_goods, ConnectionSettings.GetIdProgram(), UserSettings.User.Id, id_dep, id_post });
            return executeProcedure("[Requests].[GetMassGoodsInformation]", new string[5] { "@id_goods", "@id_prog", "@id_user", "@id_dep", "@id_post" }, new DbType[5] { DbType.String, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Обновляет значение страны/субъекта товара в справочнике
        /// </summary>
        /// <param name="id_subject">субъект</param>
        /// <param name="id_tovar">товар</param>
        public void ChangeSubject(int id_subject, int id_tovar)
        {
            ap.Clear();
            ap.AddRange(new object[3] { id_subject, id_tovar, UserSettings.User.Id });
            executeProcedure("[Requests].[ChangeSubject]", new string[3] { "@id_subject", "@id_tovar", "@id_user" }, new DbType[3] { DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Передача заявки в режиме МН
        /// </summary>
        /// <param name="id_trequest">номер заявки</param>
        /// <param name="nstatus">статус</param>
        /// <param name="CauseOfDecline">ограничения</param>
        public void SendManagerReq(int id_trequest, int nstatus, string CauseOfDecline)
        {
            ap.Clear();
            ap.AddRange(new object[4] { id_trequest, nstatus, CauseOfDecline, UserSettings.User.Id });
            executeProcedure("[Requests].[SendManagerReq]", new string[4] { "@id_trequest", "@nstatus", "@cause", "@id_user" }, new DbType[4] { DbType.Int32, DbType.Int32, DbType.String, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение запаса2 для товара
        /// </summary>
        /// <param name="idTovar">id товара</param>
        /// <param name="planRealiz">плановая реализация</param>
        /// <param name="beginOfPeriod">начало периода отсечки</param>
        /// <param name="endOfPeriod">конец периода отсечки</param>
        /// <returns></returns>
        public int GetZapas2(int idTovar, decimal planRealiz, DateTime beginOfPeriod, DateTime endOfPeriod)
        {
            ap.Clear();
            ap.AddRange(new object[5] { idTovar, planRealiz, beginOfPeriod, endOfPeriod, ConnectionSettings.GetIdProgram() });
            DataTable result = executeProcedure("[Requests].[GetZapas2]", new string[5] { "@id_tovar", "@plan_realiz", "@beginOfPeriod", "@endOfPeriod", "@id_prog" }, new DbType[5] { DbType.Int32, DbType.Decimal, DbType.DateTime, DbType.DateTime, DbType.Int32 }, ap);

            if (result == null || result.Rows.Count == 0)
            {
                return 0;
            }

            return int.Parse(result.Rows[0][0].ToString());
        }

        /// <summary>
        /// Очистка поля в теле заявки
        /// </summary>
        /// <param name="id_requests">id записи в теле заявки</param>
        /// <param name="fieldName">название поля</param>
        /// <param name="newValue">новое значение поля</param>
        public void EarseReqBodyValue(string id_requests, string fieldName, string newValue)
        {
            ap.Clear();
            ap.AddRange(new object[3] { id_requests, fieldName, newValue });
            executeProcedure("[Requests].[EarseReqBodyValue]", new string[3] { "@id_requests", "@fieldName", "@newValue" }, new DbType[3] { DbType.String, DbType.String, DbType.String }, ap);
        }

        /// <summary>
        /// Получение правила по группе пересорта товара
        /// </summary>
        /// <param name="id_goods">id товара</param>
        /// <returns></returns>
        public DataTable GetTFGoodsRules(string id_goods)
        {
            ap.Clear();
            ap.Add(id_goods);
            return executeProcedure("[Requests].[GetTFGoodsRules]", new string[1] { "@id_goods" }, new DbType[1] { DbType.String }, ap);
        }

        /// <summary>
        /// Проверка является ли товар новым
        /// </summary>
        /// <param name="ean">штрих-код товара</param>
        /// <returns></returns>
        public bool IsNewTovar(string ean)
        {
            bool newT = true;

            ap.Clear();
            ap.Add(ean);
            DataTable dt = new DataTable();
            dt = executeProcedure("[Requests].[IsNewTovar]",
                new string[1] { "@ean" }, 
                new DbType[1] { DbType.String }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                newT = (dt.Rows[0][0].ToString() == "1") ? true : false;
            }

            return newT;

        }

        /// <summary>
        ////Процедура проверки ограниченных прав у пользователя
        /// </summary>
        /// <returns>true - есть ограничения для пользователя, false - ограничений нет</returns>
        public bool NedoUser()
        {
            bool result = true;

            if (UserSettings.User.StatusCode == "ПР")
            {
                DataTable dt = new DataTable();
                dt = GetProperties();

                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    DataRow[] drWind = dt.Select("id_val = 'zcena'");
                    if (drWind != null && drWind.Count() > 0 && (drWind[0]["val"].ToString() == "1"))
                    {

                        result = false;
                    }
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        public void SetProperties(string id_val, string val, int p)
        {
            ap.Clear();
            ap.Add(ConnectionSettings.GetIdProgram());
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(id_val);
            ap.Add(val);
            ap.Add(p);

            executeProcedure("[Requests].[SetProperties]",
                new string[] { "@id_prog", "@id_user", "@id_val", "@val", "@p" },
                new DbType[] { DbType.Int32, DbType.Int32, DbType.String, DbType.String, DbType.Int32 }, ap);
        }

        public void ChangePereocNstatus(int id, int nstatus)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(nstatus);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);

            if (UserSettings.User.StatusCode == "КД")
            {
                ap.Add(1);
            }
            else
            {
                ap.Add(0);
            }

            executeProcedure("[Requests].[ChangePereocNstatus]",
                new string[] { "@id", "@nstatus", "@id_User", "@isKd" },
                new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение настроек фильтров
        /// </summary>
        /// <param name="id_value">id значения</param>
        /// <param name="id_dep">id отдела</param>
        /// <returns></returns>
        public DataTable GetFilterSettings(string id_value, int id_dep)
        {
            ap.Clear();
            ap.Add(id_value);
            ap.Add(UserSettings.User.Id);
            ap.Add(id_dep);
            return executeProcedure("[Requests].[GetFilterSettings]", new string[3] { "@id_value", "@id_user", "@id_dep" }, new DbType[3] { DbType.String, DbType.Int32, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Сохранение настроек фильтра
        /// </summary>
        /// <param name="idValue">id значения</param>
        /// <param name="id_dep">id отдела</param>
        /// <param name="value">значение</param>
        /// <param name="isDelete">признак удаления</param>
        public void SaveFilterSettings(string idValue, int id_dep, int value, bool isDelete)
        {
            ap.Clear();
            ap.AddRange(new object[5] { idValue, UserSettings.User.Id, id_dep, value, isDelete });
            executeProcedure("[Requests].[SaveFilterSettings]",
                                new string[5] { "@id_value", "@id_user", "@id_dep", "@value", "@isDelete" },
                                new DbType[5] { DbType.String, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Boolean },
                                ap);
        }

        /// <summary>
        /// Получение списка товаров менеджеров
        /// </summary>
        /// <param name="id_manager">id менеджера</param>
        /// <param name="id_dep">id отдела</param>
        /// <param name="id_grp1">id ту группы</param>
        /// <param name="id_grp2">id инв группы</param>
        /// <param name="isForManager">признак получения товаров менеджера</param>
        public DataTable GetGoodsForManager(int id_manager, int id_dep, int id_grp1, int id_grp2, bool isForManager)
        {
            ap.Clear();
            ap.AddRange(new object[5] { id_manager, id_dep, id_grp1, id_grp2, isForManager });
            return executeProcedure("[Requests].[GetGoodsForManager]",
                                    new string[5] { "@id_manager", "@id_dep", "@id_grp1", "@id_grp2", "@isForManager" },
                                    new DbType[5] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Boolean },
                                    ap);
        }

        /// <summary>
        /// Добавление/удаление товаров для менеджера
        /// </summary>
        /// <param name="id_manager">id менеджера</param>
        /// <param name="id_tovar">id товара</param>
        /// <param name="id_dep">id отдела</param>
        /// <param name="id_grp1">id ту группы</param>
        /// <param name="id_grp2">id инв. группы</param>
        /// <param name="isDelete">признак удаления</param>
        public void SetManagerGoods(int id_manager, int id_tovar, int id_dep, int id_grp1, int id_grp2, bool isDelete)
        {
            ap.Clear();
            ap.AddRange(new object[6] { id_manager, id_tovar, id_dep, id_grp1, id_grp2, isDelete });
            executeProcedure("[Requests].[SetManagerGoods]",
                            new string[6] { "@id_manager", "@id_tovar", "@id_dep", "@id_grp1", "@id_grp2", "@isDelete" },
                            new DbType[6] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Boolean },
                            ap);
        }


        /// <summary>
        /// Процедура получения изображений по товару
        /// </summary>
        /// <param name="id_tov">id товара</param>
        /// <returns></returns>
        public DataTable GetImagesByIdTov(int id_tov)
        {
            ap.Clear();
            ap.Add(id_tov);

            return executeProcedure("[Requests].[GetImagesByIdTov]",
                        new string[] { "@id_tovar" },
                        new DbType[] { DbType.Int32 },
                        ap);
        }
        

        /// <summary>
        /// Процедура получения изображения по товару
        /// </summary>
        /// <param name="id_tovar">id товара </param>
        /// <returns></returns>
        public DataTable GetGoodsImage(int id_tovar)
        {
            ap.Clear();
            ap.Add(id_tovar);

            return executeProcedure("[Requests].[GetGoodsImage]",
                        new string[] { "@id_tovar" },
                        new DbType[] { DbType.Int32 },
                        ap);
        }

        /// <summary>
        /// Процедура удаления изображения по товару
        /// </summary>
        /// <param name="id">id изображения</param>
        /// <returns></returns>
        public DataTable DelGoodsImage(int id)
        {
            ap.Clear();
            ap.Add(id);

            return executeProcedure("[Requests].[DelGoodsImage]",
                        new string[] { "@id" },
                        new DbType[] { DbType.Int32 },
                        ap);
        }

        public DataTable SetDefaultImage(int id_tovar, int id)
        {
            ap.Clear();
            ap.Add(id_tovar);
            ap.Add(id);

            return executeProcedure("[Requests].[SetDefaultImage]",
                        new string[] { "@id_tovar", "@id" },
                        new DbType[] { DbType.Int32, DbType.Int32 },
                        ap);
        }

        //[Requests].[SetDefaultImage]

        /// <summary>
        /// Добавление изображения
        /// </summary>
        /// <param name="id_tovar">id товара</param>
        /// <param name="Pic">изображение</param>
        /// <param name="cName">наименование изображения</param>
        /// <returns>id, добавленной записи</returns>
        public int AddGoodsImage(int id_tovar, byte[] Pic, string cName)
        {
            int id = 0;

            ap.Clear();
            ap.Add(id_tovar);
            ap.Add(Pic);
            ap.Add(cName);

            DataTable dt = new DataTable();
            dt = executeProcedure("[Requests].[AddGoodsImage]",
                        new string[] { "@id_tovar", "@Pic", "@FileName" },
                        new DbType[] { DbType.Int32, DbType.Binary, DbType.String },
                        ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                id = int.Parse(dt.Rows[0][0].ToString());
            }

            return id;
        }

        /// <summary>
        /// автосохранение заголовка заявки
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id_Post"></param>
        /// <param name="ntypeorg"></param>
        /// <param name="cprimech"></param>
        /// <param name="id_operand"></param>
        /// <param name="porthole"></param>
        /// <param name="id_typeBonus"></param>
        /// <param name="deletedIdReq"></param>
        public int SetAutoRequestHead(int id, DateTime dateRequest, int id_Post, int ntypeorg, string cprimech, int id_operand, int porthole, int id_typeBonus, string deletedIdReq)
        {
            ap.Clear();
            ap.AddRange(new object[] { id, UserSettings.User.Id, UserSettings.User.IdDepartment, dateRequest, id_Post, ntypeorg, cprimech, id_operand, porthole, id_typeBonus, deletedIdReq });
            DataTable dt = executeProcedure("Requests.SetAutoRequestHead", new string[] { "@id", "@id_User", "@id_Otdel", "@dateRequest", "@id_Post", "@ntypeorg", "@cprimech", "@id_operand", "@porthole", "@id_TypeBonus", "@deletedIdReq" },
                                                            new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.DateTime, DbType.Int32, DbType.Int32, DbType.String, DbType.Int32, DbType.Int32, DbType.Int32, DbType.String }, ap);
            return dt != null && dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : -1;
        }

        /// <summary>
        /// автосохранения тела заявки
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id_autoSaveTrequest"></param>
        /// <param name="id_tovar"></param>
        /// <param name="id_unit"></param>
        /// <param name="nzatar"></param>
        /// <param name="netto"></param>
        /// <param name="zcena"></param>
        /// <param name="rcena"></param>
        /// <param name="zcenabnds"></param>
        /// <param name="nprimech"></param>
        /// <param name="cprimech"></param>
        /// <param name="id_subject"></param>
        /// <param name="periodOfStorage"></param>
        /// <param name="isTransparent"></param>
        public int SetAutoRequestBody(int id, int id_autoSaveTrequest, int id_tovar, int id_unit, decimal nzatar, decimal netto, decimal zcena, decimal rcena, decimal zcenabnds, int nprimech, string cprimech, int id_subject, string periodOfStorage, bool isTransparent, decimal shelfSpace)
        {
            ap.Clear();
            ap.AddRange(new object[] { id, id_autoSaveTrequest, id_tovar, id_unit, nzatar, netto, zcena, rcena, zcenabnds, nprimech, cprimech, id_subject, periodOfStorage, isTransparent, shelfSpace, UserSettings.User.Id });
            DataTable dt = executeProcedure("Requests.SetAutoRequestBody", new string[] { "@id", "@id_autoSaveTrequest", "@id_tovar", "@id_unit", "@nzatar", "@netto", "@zcena", "@rcena", "zcenabnds", "nprimech", "cprimech", "@id_subject", "@PeriodOfStorage", "@isTransparent", "@shelfSpace", "@id_user" },
                                                            new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.Int32, DbType.String, DbType.Int32, DbType.String, DbType.Boolean, DbType.Decimal, DbType.Int32 }, ap);
            return dt != null && dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : -1;
        }

        public DataTable AutoSavedRequestsExists()
        {
            ap.Clear();
            ap.Add(UserSettings.User.Id);
            return executeProcedure("Requests.AutoSavedRequestExists", new string[] { "@id_user" }, new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable GetAutoRequestHead()
        {
            ap.Clear();
            ap.Add(UserSettings.User.Id);
            return executeProcedure("Requests.GetAutoRequestHead", new string[] { "@id_user" }, new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable GetAutoRequestBody()
        {
            ap.Clear();
            ap.AddRange(new object[] { UserSettings.User.Id, ConnectionSettings.GetIdProgram() });
            return executeProcedure("Requests.GetAutoRequestBody", new string[] { "@id_user", "@id_prog" }, new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public void ClearAutoSaveTables(int id_auto_treq)
        {
            ap.Clear();
            ap.Add(id_auto_treq);
            executeProcedure("Requests.ClearAutoSaveTables", new string[] { "@id_auto_treq" }, new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable CheckSertificate(int id_tovar, int id_subject)
        {
            ap.Clear();
            ap.AddRange(new object[] { id_tovar, id_subject });
            return executeProcedure("Requests.CheckSertificate", new string[] { "@id_tovar", "@id_subject" }, new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable GetAutoPereocBody(int id_auto_treq)
        {
            ap.Clear();
            ap.Add(id_auto_treq);
            return executeProcedure("Requests.GetAutoPereocBody", new string[] { "@id_auto_treq" }, new DbType[] { DbType.Int32 }, ap);
        }

        public DataSet GetOrders(DateTime date)
        {
            ap.Clear();
            ap.AddRange(new object[] { UserSettings.User.IdDepartment, date, ConnectionSettings.GetIdProgram(), UserSettings.User.Id });
            DataTable dtHeader = executeProcedure("Requests.GetOrders", new string[] { "@id_otdel", "@date", "@id_prog", "@id_user" }, new DbType[] { DbType.Int32, DbType.DateTime, DbType.Int32, DbType.Int32 }, ap);
            if (UserSettings.User.StatusCode == "МН")
            {
                DataRow[] orders = dtHeader.Select("id_order <> 0");
                dtHeader = orders.Length > 0 ? orders.CopyToDataTable() : dtHeader.Clone();
            }
            dtHeader.TableName = "grdHeader";

            ap.Clear();
            ap.AddRange(new object[] { UserSettings.User.IdDepartment, date });
            DataTable dtBody = executeProcedure("Requests.GetOrderBody", new string[] { "@id_otdel", "@date" }, new DbType[] { DbType.Int32, DbType.DateTime }, ap);
            dtBody.TableName = "grdBody";

            DataSet ordersSet = new DataSet();
            ordersSet.Tables.Add(dtHeader);
            ordersSet.Tables.Add(dtBody);

            DataColumn keyColumn = ordersSet.Tables["grdHeader"].Columns["id"];
            DataColumn foreignKeyColumn = ordersSet.Tables["grdBody"].Columns["id_tovar"];
            ordersSet.Relations.Add("grdBody", keyColumn, foreignKeyColumn);

            return ordersSet;
        }

        public int SaveOrderHead(int id, DateTime date, int id_tovar, decimal inventory, decimal plan_realiz, decimal sred_rashod, decimal netto, int id_grp3, int isCalcByRemains)
        {
            ap.Clear();
            ap.AddRange(new object[] { id, date, id_tovar, inventory, plan_realiz, sred_rashod, netto, id_grp3, UserSettings.User.Id, isCalcByRemains });
            DataTable dt = executeProcedure("Requests.SaveOrderHead", new string[] { "@id", "@date", "@id_tovar", "@inventory", "@plan_realiz", "@sred_rashod", "@netto", "@id_grp3", "@id_user", "@isCalcByRemains" },
                                                                      new DbType[] { DbType.Int32, DbType.DateTime, DbType.Int32, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
            return dt != null && dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
        }

        public void SaveOrderBody(int id, int id_tMainOrdering, int id_post, int id_subject, decimal fact_netto, string caliber, decimal zcena, bool is_select)
        {
            ap.Clear();
            ap.AddRange(new object[] { id, id_tMainOrdering, id_post, id_subject, fact_netto, caliber, zcena, is_select, UserSettings.User.Id });
            executeProcedure("Requests.SaveOrderBody", new string[] { "@id", "@id_tMainOrdering", "@id_post", "@id_subject", "@fact_netto", "@caliber", "@zcena", "@is_select", "@id_user" }, 
                                                              new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Decimal, DbType.String, DbType.Decimal, DbType.Boolean, DbType.Int32 }, ap);
        }

        public DataTable GetInventoryReport(DateTime date)
        {
            ap.Clear();
            ap.AddRange(new object[] { date, UserSettings.User.IdDepartment });
            return executeProcedure("Requests.GetInventoryReport", new string[] { "@date", "@id_otdel" }, new DbType[] { DbType.DateTime, DbType.Int32 }, ap);
        }

        public DataTable GetOrdersHeadForReport(DateTime date, string id_grp1, string id_grp3)
        {
            ap.Clear();
            ap.AddRange(new object[] { UserSettings.User.IdDepartment, date, ConnectionSettings.GetIdProgram(), UserSettings.User.Id });
            DataTable dt = executeProcedure("Requests.GetOrders", new string[] { "@id_otdel", "@date", "@id_prog", "@id_user" }, new DbType[] { DbType.Int32, DbType.DateTime, DbType.Int32, DbType.Int32 }, ap);
            DataRow[] rows = dt.Select("id_order <> 0" + (id_grp1.Length == 0 ? "" : " and id_grp1 in (" + id_grp1 + ")") + (id_grp3.Length == 0 ? "" : " and id_grp3 = " + id_grp3.ToString()));
            return rows.Length > 0 ? rows.CopyToDataTable() : dt.Clone();
        }

        public DataTable GetOrdersBody(DateTime date)
        {
            ap.Clear();
            ap.AddRange(new object[] { UserSettings.User.IdDepartment, date });
            return executeProcedure("Requests.GetOrderBody", new string[] { "@id_otdel", "@date" }, new DbType[] { DbType.Int32, DbType.DateTime }, ap);
        }

        public void SetOrderTRequest(int id_order_body, int id_trequest)
        {
            ap.Clear();
            ap.AddRange(new object[] { id_order_body, id_trequest });
            executeProcedure("Requests.SetOrderTRequest", new string[] { "@id_order_body", "@id_trequest" }, new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable SaveOrderRequestHead(DateTime date, int id_post)
        {
            ap.Clear();
            ap.AddRange(new object[] { UserSettings.User.IdDepartment, date, id_post, UserSettings.User.Id, ConnectionSettings.GetIdProgram() });
            return executeProcedure("Requests.SaveOrderRequestHead", new string[] { "@id_otdel", "@date", "@id_post", "@id_user", "@id_prog" }, 
                                                                     new DbType[] { DbType.Int32, DbType.Date, DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        public void SaveOrderRequestBody(int id_trequest, int id_tovar, int npp, decimal netto, decimal zcena, string caliber, int id_subject)
        {
            ap.Clear();
            ap.AddRange(new object[] { id_trequest, id_tovar, npp, netto, zcena, caliber, id_subject });
            executeProcedure("Requests.SaveOrderRequestBody", new string[] { "@id_trequest", "@id_tovar", "@npp", "@netto", "@zcena", "@caliber", "@id_subject" }, 
                                                              new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Decimal, DbType.Decimal, DbType.String, DbType.Int32 }, ap);
        }

        public DataTable GetDopOrder(DateTime date)
        {
            ap.Clear();
            ap.AddRange(new object[] { UserSettings.User.IdDepartment, date, ConnectionSettings.GetIdProgram(), UserSettings.User.Id });
            return executeProcedure("Requests.GetDopOrder", new string[] { "@id_otdel", "@date", "@id_prog", "@id_user" }, new DbType[] { DbType.Int32, DbType.DateTime, DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable SaveOrderTTost(DateTime date)
        {
            ap.Clear();
            ap.AddRange(new object[] { UserSettings.User.IdDepartment, date, UserSettings.User.Id });
            return executeProcedure("Requests.SaveOrderTTost", new string[] { "@id_otdel", "@date", "@id_user" }, new DbType[] { DbType.Int32, DbType.DateTime, DbType.Int32 }, ap);
        }

        public void SaveOrderOst(int id_tost, int id_tovar, string ean, decimal inventory, int npp)
        {
            ap.Clear();
            ap.AddRange(new object[] { id_tost, id_tovar, ean, inventory, npp, UserSettings.User.Id });
            executeProcedure("Requests.SaveOrderOst", new string[] { "@id_tost", "@id_tovar", "@ean", "@inventory", "@npp", "@id_user" }, 
                                                      new DbType[] { DbType.Int32, DbType.Int32, DbType.String, DbType.Decimal, DbType.Int32, DbType.Int32 }, ap );
        }

        public bool DepartmentInSettings(string id_value)
        {
            ap.Clear();
            ap.AddRange(new object[] { ConnectionSettings.GetIdProgram(), id_value });
            DataTable dt = executeProcedure("Requests.GetSettings", new string[] { "@id_prog", "@id_value" }, new DbType[] { DbType.Int32, DbType.String }, ap);
            return dt != null && dt.Select("value = " + UserSettings.User.IdDepartment.ToString()).Length > 0;
        }

        public int GetGoodZatar(int id_tovar)
        {
            ap.Clear();
            ap.Add(id_tovar);
            DataTable dt = executeProcedure("Requests.GetGoodZatar", new string[] { "@id_tovar" }, new DbType[] { DbType.Int32 }, ap);
            return dt != null && dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["nzatar"]) : -1;
        }

        public void SaveGrp3(int id_tovar, int id_grp3)
        {
            ap.Clear();
            ap.AddRange(new object[] { id_tovar, id_grp3 });
            executeProcedure("Requests.SaveGrp3", new string[] { "@id_tovar", "@id_grp3" }, new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public void DeleteOrderBody(int id)
        {
            ap.Clear();
            ap.Add(id);
            executeProcedure("Requests.DeleteOrderBody", new string[] { "@id" }, new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable GetULSettingsZakaznik()
        {
            ap.Clear();
            return executeProcedure("Requests.GetULSettingsZakaznik", new string[] { }, new DbType[] { }, ap);
        }

        public DataRow GetZakaznikNtypeorg()
        {
            DataTable dtSettings = GetSettings("dpos");
            if (dtSettings != null && dtSettings.Rows.Count > 0)
            {
                DataTable dt = GetULSettingsZakaznik();
                DataRow[] rows = dt.Select("nTypeOrg = " + dtSettings.Rows[0]["value"].ToString());
                return rows.Length > 0 ? rows[0] : null;
            }
            return null;
        }

        public string GetStoreName()
        {
            DataTable dt = GetSettings("shop");
            return dt != null && dt.Rows.Count > 0 ? dt.Rows[0]["value"].ToString() : "МАГАЗИН НЕ УКАЗАН";
        }

        public DataTable GetTekOstAndZapas(int findPlace, string ean)
        {
            ap.Clear();
            ap.AddRange(new object[] { findPlace, ean, ConnectionSettings.GetIdProgram() });
            return executeProcedure("Requests.GetTekOstAndZapas", new string[] { "@findPlace", "@ean", "@idProg" }, new DbType[] { DbType.Int32, DbType.String, DbType.Int32 }, ap);
        }
    
        //NEW 27.07.2017

        public DataTable getAllSettingsWeInOut(int type, string id_value)
        {
            ap.Clear();

            ap.Add(type);
            ap.Add(Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram());
            ap.Add(id_value);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);

            return executeProcedure("[requests].[getAllSettingsWeInOut]",
                new string[] { "@type", "@id_prog", "@id_value","@id_user" },
                new DbType[] { DbType.Int32, DbType.Int32, DbType.String,DbType.Int32 },
                ap);
        }

        public DataTable setAllSettingsWeInOut(string id_value, string value, int type)
        {
            ap.Clear();

            ap.Add(Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram());
            ap.Add(id_value);
            ap.Add(value);
            ap.Add(type);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);

            return executeProcedure("[requests].[setAllSettingsWeInOut]",
                new string[] { "@id_prog", "@id_value", "@value", "@type", "@id_user" },
                new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.Int32 },
                ap);
        }

        public DataTable checkStatusTovarInBases(string ean, int id_deps)
        {
            ap.Clear();

            ap.Add(ean);
            ap.Add(id_deps);

            return executeProcedure("[requests].[checkStatusTovarInBases]",
                new string[] { "@ean", "@id_deps" },
                new DbType[] { DbType.String, DbType.Int32 },
                ap);
        }

        public DataTable getWeInOutTovarPrice(string ean, DateTime date, int type)
        {
            ap.Clear();

            ap.Add(ean);
            ap.Add(date);
            ap.Add(type);

            return executeProcedure("[requests].[getWeInOutTovarPrice]",
                new string[] { "@ean", "@date", "@type"},
                new DbType[] {  DbType.String, DbType.Date, DbType.Int32 },
                ap);
        }

        public DataTable getWeInOutTovarList(int id_tovar,DateTime dateStart, DateTime  dateEnd,
            string listOperant, string listPost, int idTReq, int type)
        {
            ap.Clear();

            ap.Add(id_tovar);
            ap.Add(dateStart);
            ap.Add(dateEnd);
            ap.Add(listOperant);
            ap.Add(listPost);
            ap.Add(idTReq);

            ap.Add(type);

            return executeProcedure("[requests].[getWeInOutTovarList]",
                new string[] { "@id_tovar", "@dateStart", "@dateEnd", "@listOperant", "@listPost", "@idTReq", "@type" },
                new DbType[] { DbType.Int32, DbType.Date, DbType.Date, DbType.String, DbType.String, DbType.Int32, DbType.Int32 },
                ap);
        }

        public DataTable getWeInOutTovarList(int id_tovar, DateTime dateStart, DateTime dateEnd,
            string listOperant, string listPost, int idTReq, int type, string list)
        {
            ap.Clear();

            ap.Add(id_tovar);
            ap.Add(dateStart);
            ap.Add(dateEnd);
            ap.Add(listOperant);
            ap.Add(listPost);
            ap.Add(idTReq);

            ap.Add(type);

            ap.Add(list);

            return executeProcedure("[requests].[getWeInOutTovarList]",
                new string[] { "@id_tovar", "@dateStart", "@dateEnd", "@listOperant", "@listPost", "@idTReq", "@type", "@list" },
                new DbType[] { DbType.Int32, DbType.Date, DbType.Date, DbType.String, DbType.String, DbType.Int32, DbType.Int32, DbType.String },
                ap);
        }

        public DataTable setWeInOutTovarList(int id_trequest, int id_prihod, int id_tovar, decimal netto, decimal zcena, int type)
        {
            ap.Clear();

            ap.Add(id_trequest);
            ap.Add(id_prihod);
            ap.Add(id_tovar);
            ap.Add(netto);
            ap.Add(zcena);
            ap.Add(type);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);

            return executeProcedure("[requests].[setWeInOutTovarList]",
                new string[] { 
                            "@id_trequest",
                            "@id_prihod",
                            "@id_tovar",
                            "@netto",
                            "@zcena",
                            "@type",
                            "@id_user"
                             },
                new DbType[] { 
                            DbType.Int32,
                            DbType.Int32 ,
                            DbType.Int32 ,
                            DbType.Decimal,
                            DbType.Decimal,
                            DbType.Int32,
                            DbType.Int32
                             },
                ap);
        }

        public DataTable getWeInOutIdPostNtypeOrgList(string listIdPrihod, int id_post, int ntypeorg, int type)
        {
            ap.Clear();

            ap.Add(listIdPrihod);
            ap.Add(id_post);
            ap.Add(ntypeorg);
            ap.Add(type);

            return executeProcedure("[requests].[getWeInOutIdPostNtypeOrgList]",
                new string[] { "@listIdPrihod", "@id_post", "@ntypeorg", "@type" },
                new DbType[] { DbType.String, DbType.Int32, DbType.Int32, DbType.Int32 },
                ap);
        }

        public DataTable setSubShopData(int id_otdel, int id_post, DateTime data_out, int id_oper1, int ntypeorg, int porthole,int type, int oldTRequest)
        {
            ap.Clear();

            ap.Add(id_otdel);
            ap.Add(id_post);
            ap.Add(data_out);
            ap.Add(id_oper1);
            ap.Add(ntypeorg);
            ap.Add(porthole);

            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);

            ap.Add(type);
            ap.Add(oldTRequest);

            return executeProcedure("[requests].[setSubShopData]",
                new string[] { 
                    "@id_otdel", 
                    "@id_post", 
                    "@data_out", 
                    "@id_oper1",
                    "@ntypeorg",
                    "@porthole",
                    "@id_user",
                    "@type",
                    "@inRequest"
                },
                new DbType[] { 
                    DbType.Int32, 
                    DbType.Int32,                     
                    DbType.Date,
                    DbType.Int32,
                    DbType.Int32,
                    DbType.Int32, 
                    DbType.Int32,
                    DbType.Int32,
                    DbType.String
                },
                ap);
        }

        public DataTable setSubShopDataTovar(int id_trequest, int npp, int id_tovar, decimal nzatar,
            decimal netto, decimal zcena, decimal rcena, decimal zcenabnds, int type, string ean,DateTime date_out)
        {
            ap.Clear();

            ap.Add(id_trequest);
            ap.Add(npp);
            ap.Add(id_tovar);
            ap.Add(nzatar);
            ap.Add(netto);
            ap.Add(zcena);
            ap.Add(rcena);
            ap.Add(zcenabnds);

            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);

            ap.Add(type);
            ap.Add(ean);
            ap.Add(date_out);

            return executeProcedure("[requests].[setSubShopDataTovar]",
                new string[] { 
                    "@id_trequest", 
                    "@npp", 
                    "@id_tovar", 
                    "@nzatar",
                    "@netto",
                    "@zcena",
                    "@rcena",
                    "@zcenabnds",
                    "@id_user" ,
                    "@type",
                    "@ean",
                    "@date_out"
                },
                new DbType[] { 
                    DbType.Int32, 
                    DbType.Int32,                     
                    DbType.Int32,
                    DbType.Decimal,
                    DbType.Decimal,
                    DbType.Decimal,
                    DbType.Decimal, 
                    DbType.Decimal, 
                    DbType.Int32,
                    DbType.Int32 ,
                    DbType.String,
                    DbType.Date
                
                },
                ap);
        }

        public DataTable updateCinTRequest(int id_trequest, string listIdSubShop)
        {
            ap.Clear();

            ap.Add(id_trequest);
            ap.Add(listIdSubShop);

            return executeProcedure("[requests].[updateCinTRequest]",
                new string[] { "@id_trequest", "@listIdSubShop" },
                new DbType[] { DbType.Int32, DbType.String },
                ap);
        }

        public DataTable setLinkTRequest(int idTrequest, int idPrihod, int idOtgruz, int type)
        {
            ap.Clear();

            ap.Add(idTrequest);
            ap.Add(idPrihod);
            ap.Add(idOtgruz);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(type);

            return executeProcedure("[requests].[setLinkTRequest]",
                new string[] { 
                    "@idTrequest", 
                    "@idPrihod",
                    "@idOtgruz",
                    "@id_user",
                    "@type"
                },
                new DbType[] { 
                    DbType.Int32, 
                    DbType.Int32, 
                    DbType.Int32,
                    DbType.Int32,
                    DbType.Int32
                },
                ap);
        }

        public DataTable getOstTo3Shop(string listTovar)
        {
            ap.Clear();

            ap.Add(listTovar);
            
            return executeProcedure("[requests].[getOstTo3Shop]",
                new string[] { 
                
                    "@listTovar"
                },
                new DbType[] { 
                   
                    DbType.String
                },
                ap);
        }

        public DataTable getLinkTRequest(int idTrequest, int type)
        {
            ap.Clear();

            ap.Add(idTrequest);
            ap.Add(type);


            return executeProcedure("[requests].[getLinkTRequest]",
                new string[] { 
                
                    "@idTrequest",
                    "@type"

                },
                new DbType[] { 
                   
                    DbType.Int32,
                    DbType.Int32

                },
                ap);
        }

        public DataTable getWeInBusyTovar(int id_tovar, int id_trequest)
        {
            ap.Clear();

            ap.Add(id_tovar);
            ap.Add(id_trequest);

            return executeProcedure("[requests].[getWeInBusyTovar]",
                new string[] { 
                
                     "@id_tovar"
                    ,"@id_trequest"
                },
                new DbType[] { 
                   
                    DbType.Int32,
                    DbType.Int32
                },
                ap);
        }

        public int setCurPeriod()
        {
            int error = -1;

            ap.Clear();

            ap.Add(UserSettings.User.Id);
            ap.Add(ConnectionSettings.GetIdProgram());

            DataTable dt = executeProcedure("[requests].[setCurPeriod]",
                            new string[] { "@idUser", "@idProg" },
                            new DbType[] { DbType.Int32, DbType.Int32 }, ap);
            if (dt != null && dt.Rows.Count > 0)
                error = (int)dt.Rows[0]["error"];
            return error;
        }
    }
}
