using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Nwuram.Framework.Project;
using System.Data;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;

namespace ViewSalesPromProducts
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length != 0)
                if (Project.FillSettings(args))
                {
                    //Logging.Init(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
                    //Logging.StartFirstLevel(1);
                    //Logging.Comment("Вход в программу");
                    //Logging.StopFirstLevel();

                    Config.connectMain = new sqlProcedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
                    Config.connectSecond = new sqlProcedures(ConnectionSettings.GetServer("2"), ConnectionSettings.GetDatabase("2"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

                    if (Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToUpper().Equals("КД"))
                        Application.Run(new frmMain());
                    else
                        if (Nwuram.Framework.Settings.User.UserSettings.User.StatusCode.ToUpper().Equals("ОП"))
                        Application.Run(new frmViewDiscountGoods());

                    //Logging.StartFirstLevel(2);
                    //Logging.Comment("Выход из программы");
                    //Logging.StopFirstLevel();
                    //Project.clearBufferFiles();
                }
        }
    }
}
