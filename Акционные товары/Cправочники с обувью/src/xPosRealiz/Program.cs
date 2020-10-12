using Microsoft.Win32;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Project;
using Nwuram.Framework.Settings.Connection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Windows.Forms;
using System.ComponentModel;

namespace xPosRealiz
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            string line;
            System.IO.StreamReader file =
                    new System.IO.StreamReader(Application.StartupPath + @"\config.ini");
            String[] words = { };
            while ((line = file.ReadLine()) != null)
            {
                words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Count() > 0)
                    break;
            }
            file.Close();

            if (words.Count() > 0)
                Project.FillSettings(words);
            Application.Run(new MainForm());

        }
    }
}
