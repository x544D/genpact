using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace GenPactLauncher
{
    class Program
    {


        const uint PROCESS_ALL_ACCESS = 0x001F0FFF;

        //OP
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);


        // RPM
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [MarshalAs(UnmanagedType.AsAny)] object lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);


        //WPM
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(
          IntPtr hProcess,
          IntPtr lpBaseAddress,
          [MarshalAs(UnmanagedType.AsAny)] object lpBuffer,
          int dwSize,
          out IntPtr lpNumberOfBytesWritten);


        //VPE
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);


        //CH
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr hHandle);


        //GPA
        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);


        //CRT
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess,
           IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress,
           IntPtr lpParameter, uint dwCreationFlags, out IntPtr lpThreadId);

        //VAE
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
            IntPtr dwSize, AllocationType flAllocationType, MemoryProtection flProtect);


        [Flags]
        public enum MemoryProtection
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, AllocationType dwFreeType);

        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }




        enum Errz : uint
        {
            NOT_ENOUGH_PERMISSION,
            NO_WINLOGON_PROC, 
            NO_WINEXEC_BADDR,
            WRONG_JSON_FILE,
            JSON_FILE_404,
        }



        static void WriteLastRunningLogs(string _)
        {

        }

        static Process WinlogonProc() => Process.GetProcesses().Where(p => p.ProcessName.ToLower() == "winlogon").FirstOrDefault();

        static void WriteErr(Errz e)
        {
            Console.WriteLine($"+ Error code : " + e.ToString("X"));
            Console.WriteLine("+ Type enter to exit .");
            Console.Read();
            Environment.Exit(0);
        }

        delegate uint WinExec(string lpCmdLine, uint uCmdShow);





        static void Main(string[] args)
        {

            const string version = "v1.1";
            const string news_link = "http://x544d.github.io/genpact/GenPact";
            const string GenPact_download = "http://x544d.github.io/genpact/genpact.zip";
            const string NewVersionMessage = "+ New Version out !\n+ Visit : http://x544d.github.io/genpact .\n\nIf the zip has a password please reach me Privately on discord.\nDisocrd : Kaizuku#4781";

#if !DEBUG

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(news_link);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string news = reader.ReadToEnd();

                    string[] _news = news.Split('\n');

                    if (_news.Length >= 1)
                    {
                        if (_news[0].Contains("-"))
                        {
                            if (_news[0].Split('-')[1].Trim() != version)
                            {
                                ProcessStartInfo pin = new ProcessStartInfo
                                {
                                    CreateNoWindow = true,
                                    UseShellExecute = false,
                                    FileName = "cmd.exe",
                                    WindowStyle = ProcessWindowStyle.Hidden,
                                    Arguments = $"/C start {GenPact_download}"
                                };

                                Console.WriteLine(NewVersionMessage);
                                Process.Start(pin);
                                Environment.Exit(0);
                            }
                           
                        }
                        else
                        {
                            Console.WriteLine("+ Err 0x1A");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("+ Err 0x1B");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("+ Error 0x1F - Please reach the dev at Disocrd : Kaizuku#4781 ");
                Console.ReadKey();
                Environment.Exit(0);
            }
#endif

            Process wl = WinlogonProc();
            JObject cfg = null;


            if (File.Exists("genpact.json"))
            {
                cfg = JObject.Parse(File.ReadAllText("genpact.json")); ;
                
                if (cfg.ContainsKey("launch"))
                {
                    foreach (var line in cfg["launch"].ToArray())
                    {
                        string path = line.ToString();
                        if (!string.IsNullOrEmpty(path))
                        {
                            if (File.Exists(path))
                            {
                                Console.Write($"[ + ] Launching : {path} ");

                                if (wl != null)
                                {
                                    Console.Write(".");

                                    IntPtr hProc = OpenProcess(PROCESS_ALL_ACCESS, false, wl.Id);

                                    if (hProc != IntPtr.Zero)
                                    {
                                        Console.Write(".");
                                        IntPtr pWinExec = IntPtr.Zero;

                                        foreach (ProcessModule module in wl.Modules)
                                        {
                                            if (module.ModuleName.ToLower() == "kernel32.dll")
                                            {
                                                pWinExec = GetProcAddress(module.BaseAddress, "WinExec");
                                                break;
                                            }
                                        }


                                        if (pWinExec == IntPtr.Zero) WriteErr(Errz.NO_WINEXEC_BADDR);
                                        Console.Write(".");

                                        IntPtr valloc = VirtualAllocEx(hProc, IntPtr.Zero, (IntPtr)(path.Length), AllocationType.Reserve | AllocationType.Commit, MemoryProtection.ReadWrite);

                                        if (valloc != IntPtr.Zero)
                                        {
                                            Console.Write(".");

                                            if (WriteProcessMemory(hProc, valloc, Encoding.ASCII.GetBytes(path), path.Length, out _))
                                            {
                                                Console.Write(".");
                                                IntPtr hThread = IntPtr.Zero;
                                                hThread = CreateRemoteThread(hProc, IntPtr.Zero, 0, pWinExec, valloc, 0, out _);

                                                if (hThread != IntPtr.Zero)
                                                {
                                                    Console.Write(" 100%\n");
                                                    CloseHandle(hProc);

                                                }
                                                else Console.Write("x");
                                            }
                                            else Console.Write("x");

                                            VirtualFreeEx(hProc, valloc, path.Length, AllocationType.Release);

                                        }
                                        else Console.Write("x");
                                        CloseHandle(hProc);
                                    }
                                    else
                                    {
                                        Console.WriteLine("\n[ + ] Failed Getting the necessary Permissions !");
                                        WriteErr(Errz.NOT_ENOUGH_PERMISSION);
                                    }
                                }
                                else WriteErr(Errz.NO_WINLOGON_PROC);
                            }
                        }                        
                    }
                }
                else WriteErr(Errz.WRONG_JSON_FILE);
            }
            else WriteErr(Errz.JSON_FILE_404);



            string GenPact = "\"" + AppDomain.CurrentDomain.BaseDirectory + "lsass.exe\" ";
            if (!File.Exists("lsass.exe"))
            {
                Console.WriteLine("+ GenPact Files Were Changed / removed / damaged .");
                Console.Write("+ Re-Download the Original Last Version ? y/n > ");
                if (Console.Read() == 'y')
                {
                    Process.Start("http://x544d.github.io/genpact.zip");
                }

                Environment.Exit(0);
            }
            Console.Write("[ + ] Launching GenPact ");

            if (wl != null)
            {
                GenPact += wl.Id.ToString("X");
                string _tmpc = "";
                try
                {
                    _tmpc = $" {cfg["anti_anti_dbg"]} {cfg["lang"]} {cfg["startup"]} {cfg["hwnd_destroy"]} {cfg["muted"]} {cfg["logs_as"]} {cfg["self_move"]} {cfg["r0"]}";
                }
                catch (Exception)
                {
                    _tmpc = "";
                }
                GenPact += _tmpc;

                IntPtr hProc = OpenProcess(PROCESS_ALL_ACCESS, false, wl.Id);
                if (hProc != IntPtr.Zero)
                {
                    Console.Write(".");
                    IntPtr pWinExec = IntPtr.Zero;

                    foreach (ProcessModule module in wl.Modules)
                    {
                        if (module.ModuleName.ToLower() == "kernel32.dll")
                        {
                            pWinExec = GetProcAddress(module.BaseAddress, "WinExec");
                            break;
                        }
                    }

                    if (pWinExec == IntPtr.Zero) WriteErr(Errz.NO_WINEXEC_BADDR);
                    Console.Write(".");

                    IntPtr valloc = VirtualAllocEx(hProc, IntPtr.Zero, (IntPtr)(GenPact.Length), AllocationType.Reserve | AllocationType.Commit, MemoryProtection.ReadWrite);

                    if (valloc != IntPtr.Zero)
                    {
                        Console.Write(".");

                        if (WriteProcessMemory(hProc, valloc, Encoding.ASCII.GetBytes(GenPact), GenPact.Length, out _))
                        {
                            Console.Write(".");
                            IntPtr hThread = IntPtr.Zero;
                            hThread = CreateRemoteThread(hProc, IntPtr.Zero, 0, pWinExec, valloc, 0, out _);

                            if (hThread != IntPtr.Zero)
                            {
                                Console.Write(".");

                                Console.Write(" 100%\n");
                                CloseHandle(hProc);

                            }
                            else Console.Write("x");
                        }
                        else Console.Write("x");


                        VirtualFreeEx(hProc, valloc, GenPact.Length, AllocationType.Release);

                    }
                    else Console.Write("x");

                    CloseHandle(hProc);
                }
                else
                {
                    Console.WriteLine("\n[ + ] Failed Getting the necessary Permissions !");
                    WriteErr(Errz.NOT_ENOUGH_PERMISSION);

                }

                Thread.Sleep(5000);
                Environment.Exit(0);
            }
            else WriteErr(Errz.NO_WINLOGON_PROC);
        }
    }
}
