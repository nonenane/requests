using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using Nwuram.Framework.Settings.User;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Diagnostics;

namespace ViewSalesPromProducts
{
    public class sqlProcedures : SqlProvider
    {
        public sqlProcedures(string server, string database, string username, string password, string appName)
                : base(server, database, username, password, appName)
        {
        }

        ArrayList ap = new ArrayList();
        static SqlProvider sql = new SqlProvider(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        public void TestConnectionDB()
        {
            while (true)
            {
                try
                {
                    sql = new SqlProvider(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
                    Thread.Sleep(1000);
                }
                catch
                {
                    MessageBox.Show(" Потеряно соединение с сервером!!! \n Убедитесь в восстановлени связи. \n Нажмите кнопку ОК. \n Если соединение не восстановлено, обратитесь в ОЭЭС.", "Предупреждение",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Process.GetCurrentProcess().Kill();
                }
            }
        }

        public DataTable getDep()
        {
            ap.Clear();
            return executeProcedure("[requests].[getDep]",
                 new string[] { },
                 new DbType[] { }, ap);

        }
        public DataTable getInvGroup(int otdel)
        {
            ap.Clear();
            ap.Add(otdel);
            return executeProcedure("[requests].[getInvGroup]",
                 new string[] { "@otdel" },
                 new DbType[] { DbType.Int32 }, ap);

        }

        public DataTable getTYGroup(int otdel)
        {
            ap.Clear();
            ap.Add(otdel);
            return executeProcedure("[requests].[getTYGroup]",
                 new string[] { "@otdel" },
                 new DbType[] { DbType.Int32 }, ap);

        }

        public DataTable getAllTovar()
        {
            ap.Clear();
            return executeProcedure("[requests].[getAllTovar]",
                new string[] { },
                new DbType[] { }, ap);
        }

        public DataTable getPromotionalTovar()
        {
            ap.Clear();
            return executeProcedure("[requests].[getPromotionalTovar]",
                new string[] { },
                new DbType[] { }, ap);
        }
      
        public DataTable setDeletePromotionalTovar(int id_tovar, int type_operation)
        {
            ap.Clear();
            ap.Add(id_tovar);
            ap.Add(UserSettings.User.Id);
            ap.Add(type_operation);
            return executeProcedure("[requests].[setDeletePromotionalTovar]",
                new string[] { "@id_tovar", "@id_Creator", "@type_operation" },
                new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable getTablePromotionalTovar(DateTime dateStart, DateTime dateEnd)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);

            return executeProcedure("[requests].[getTablePromotionalTovar]",
                new string[] { "@dateStart", "@dateEnd" },
                new DbType[] { DbType.DateTime, DbType.DateTime }, ap);
        }



        public DataTable getCatalogPromotionalTovars()
        {
            ap.Clear();
            return executeProcedure("[requests].[getCatalogPromotionalTovars]",
                new string[] { },
                new DbType[] { }, ap);
        }


    }
}
