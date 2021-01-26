using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenPactWatcher
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);


        [DllImport("kernel32.dll")]
        static extern uint GetLastError();


        private static void WL(string txt)
        {
            File.AppendAllText("logs/watcher.log", $"[ {DateTime.Now} ] - {txt} \n");
        }


        static void Main(string[] args)
        {
            try
            {
                
                if (args.Length != 1) Environment.Exit(0);
                Process _ = Process.GetProcesses().Where(p => p.Id == int.Parse(args[0])).FirstOrDefault();
                if (_ == null || _.ProcessName != "lsass") Environment.Exit(0);


                while (!_.HasExited)
                {
                    var p = Process.GetProcesses().Where(proc => proc.ProcessName == "upload_crash").FirstOrDefault();

                    if (p != null)
                    {
                        p.Kill();
                        if (!p.HasExited)
                        {
                            WL("[ ! ] Failed Killed Successfully !");

                            if (!TerminateProcess(p.Handle, 0))
                            {
                                WL($"[ ! ] [ {GetLastError()} ] - Error Happend .");
                            }
                            else
                            {
                                WL("[ + ] Detected and Terminated Successfully !");
                            }
                        }
                        else
                        {
                            WL("[ + ] Detected and Killed Successfully !");
                        }
                    }

                    Thread.Sleep(500);
                }
            }
            catch (Exception) { Environment.Exit(0); }
            WL("[ ! ] GenPact Was closed !");
            Environment.Exit(0);
        }
    }
}
