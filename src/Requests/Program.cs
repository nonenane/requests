using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Nwuram.Framework.Project;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Logging;

namespace Requests
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0)
            {
                Project.FillSettings(args);
                Config.RunArguments = args;

                Logging.Init(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

                //Заполнение глобальных настроек
                //Осн. коннект
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
                //Доп. коннект
                Config.hCntAdd = new Procedures(ConnectionSettings.GetServer("2"), ConnectionSettings.GetDatabase("2"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
                //Доп. коннект к базе магазина 2
                Config.hCntShop2 = new Procedures(ConnectionSettings.GetServer("3"), ConnectionSettings.GetDatabase("3"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

                Logging.StartFirstLevel(1);
                Logging.Comment("Вход в программу");
                Logging.StopFirstLevel();
                                
                Application.Run(new Main());

                Logging.StartFirstLevel(2);
                Logging.Comment("Пользователь закрыл программу");
                Logging.StopFirstLevel();

                Nwuram.Framework.Project.Project.clearBufferFiles();
            }
        }
    }
}
