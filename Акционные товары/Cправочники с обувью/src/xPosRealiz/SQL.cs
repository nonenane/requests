using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using System.Data;

namespace xPosRealiz
{
    public class SQL
    {
        static ArrayList ap = new ArrayList();
        static SqlProvider sql = new SqlProvider(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        //static SqlProvider sqlDop = new SqlProvider(ConnectionSettings.GetServer("2"), ConnectionSettings.GetDatabase("2"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        #region Sprav
        public static DataTable getListDeps()
        {
            ap.Clear();
            return sql.executeProcedure("[xpos].[getListDeps]",
                new string[] { },
                new DbType[] { }, ap);
        }

        public static DataTable getListTovar()
        {
            ap.Clear();
            return sql.executeProcedure("[xpos].[getListTovar]",//for 5.50 dbase1
                new string[] { },
                new DbType[] { }, ap);
        }

       
        public static DataTable getLastListTovar(long last_id,bool all)
        {
            ap.Clear();
            ap.Add(last_id);
            ap.Add(all);
            return sql.executeProcedure("[xpos].[getLastListTovar]",
                new string[] { "@last_id", "@scope"},
                new DbType[] { DbType.Int64, DbType.Boolean}, ap);
        }

        public static DataTable getListGrp()
        {
            ap.Clear();
            return sql.executeProcedure("[xpos].[getListGrp]",
                new string[] { },
                new DbType[] { }, ap);
        }

        public static long getLastIdSprav()
        {
            ap.Clear();
            DataTable dt = sql.executeProcedure("[xpos].[getLastIdSprav]",
                new string[] { },
                new DbType[] { }, ap);
            try
            {
               return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            catch {return MainForm.lastID;}
        }

        public static DataTable getLastId()
        {
            ap.Clear();
            return sql.executeProcedure("[xpos].[getLastID]",
                new string[] { },
                new DbType[] { }, ap);
        }

        public static DataTable setLastId(int num, long id)
        {
            ap.Clear();
            ap.Add(num);
            ap.Add(id);
            return sql.executeProcedure("[xpos].[setLastID]",
                new string[] { "@number","@lastID" },
                new DbType[] { DbType.Int32, DbType.Int64 }, ap);
        }

        public static DataTable setJSprav(int terminal, long id)
        {
            ap.Clear();
            ap.Add(terminal);
            ap.Add(id);
            return sql.executeProcedure("[xpos].[setJSprav]",
                new string[] { "@terminal", "@id" },
                new DbType[] { DbType.Int32, DbType.Int64 }, ap);
        }

        public static string getJSprav(int terminal)
        {
            ap.Clear();
            ap.Add(terminal);
            DataTable dt = sql.executeProcedure("[xpos].[getJSprav]",
                new string[] { "@terminal"},
                new DbType[] { DbType.Int32}, ap);
            try { return dt.Rows[0][0].ToString(); }
            catch { return "0"; }
        }

        #endregion


        public static DataTable getSettings(string id_value)
        {
            ap.Clear();
            ap.Add(ConnectionSettings.GetIdProgram());
            ap.Add(id_value);
            return sql.executeProcedure("[xpos].[getSettings]",
                new string[2] { "@id_prog", "@id_value" },
                new DbType[2] { DbType.Int32,DbType.String }, ap);
            
        }


        public static DataTable getCatalogPromotionalTovars()
        {
            ap.Clear();

            return sql.executeProcedure("[xpos].[getCatalogPromotionalTovars]",
                new string[0] { },
                new DbType[0] { }, ap);

        }

    }
}


