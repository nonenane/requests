using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Requests
{
    class ProcessService
    {
        public static void StartStatusViewer(string process_name, string path_to_process)
        {
            if (!ProcessIsRunning(process_name))
            {
                string filename = Path.Combine(path_to_process, process_name + ".exe");
                Process.Start(filename, Environment.CommandLine.Substring(Environment.CommandLine.IndexOf(' ') + 1));
            }
        }

        public static bool ProcessIsRunning(string process_name)
        {
            return Process.GetProcessesByName(process_name).Length > 0;
        }
        
        public static void StopProcess(string process_name)
        {
            Process[] processes = Process.GetProcessesByName(process_name);
            foreach (Process process in processes)
            {
                process.Kill();
            }
        }
    }
}
