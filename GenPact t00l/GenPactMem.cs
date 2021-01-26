using GenPact.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenPact
{
    class GenPactMem
    {

        public static JObject jsControls = ReadJs();

        static byte[] LastNearJmp;
        static long lastAllocationAddr;

        #region MEMORY_KERNEL32
        const uint PROCESS_ALL_ACCESS = 0x001F0FFF;



        //OP
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);


        // RPM
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, Int64 lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);


        //WPM
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(
          IntPtr hProcess,
          IntPtr lpBaseAddress,
          byte[] lpBuffer,
          int dwSize,
          ref int lpNumberOfBytesWritten);


        //VPE
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);


        //CH
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr hHandle);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);


        // MY API
        [DllImport("GenPactApi.dll")]
        static extern bool _0x100(int pid, int status);

        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);

        //OT
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);


        [Flags]
        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }


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





        #endregion


        #region WINAPI_USER32

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);




        #endregion



        private static void SuspendProcess(int pid)
        {
            var process = Process.GetProcessById(pid);

            foreach (ProcessThread pT in process.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                SuspendThread(pOpenThread);

                CloseHandle(pOpenThread);
            }
        }

        public static void ResumeProcess(int pid)
        {
            var process = Process.GetProcessById(pid);

            if (process.ProcessName == string.Empty)
                return;

            foreach (ProcessThread pT in process.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                var suspendCount = 0;
                do
                {
                    suspendCount = ResumeThread(pOpenThread);
                } while (suspendCount > 0);

                CloseHandle(pOpenThread);
            }
        }

        private static long GetModuleBaseAddress(Process p , string moduleName)
        {
            if (p != null)
            {
                foreach (ProcessModule module in p.Modules)
                {
                    if (module.ModuleName.ToLower() == moduleName.ToLower())
                    {
                        return (long)module.BaseAddress;
                    }
                }
            }

            return 0;
        }

        private static IntPtr GetAddressPointedTo(IntPtr hProc, IntPtr address , int[] offset)
        {
            
            byte[] resAdr = new byte[8]; 
            int b_read = 0;
            bool rpm1 = ReadProcessMemory(hProc, (Int64)address, resAdr, resAdr.Length, ref b_read);
            long ___ = BitConverter.ToInt64(resAdr, 0) + offset[0];

            for (int c = 1; c < offset.Length; c++)
            {
                ReadProcessMemory(hProc, (Int64)___, resAdr, resAdr.Length, ref b_read);
                ___ = BitConverter.ToInt64(resAdr, 0)+offset[c];
            }

            return (IntPtr)___;
        }

        public static bool HardCoreMemoryWrite(out string errMsg, byte[] Value2Write, string ModuleName, IntPtr bAddress, int[] offsets)
        {
            if (Settings.Default.GameProcess != null)
            {

                if (bAddress != IntPtr.Zero)
                {
                    IntPtr hProc = OpenProcess(PROCESS_ALL_ACCESS, false, Properties.Settings.Default.GameProcess.Id);

                    if (hProc != IntPtr.Zero)
                    {

                        
                        long _tmpAddress = GetModuleBaseAddress(Properties.Settings.Default.GameProcess, ModuleName);

                        if (_tmpAddress != 0)
                        {
                            _tmpAddress += (long)bAddress;
                        }
                        else
                        {
                            errMsg = "[ ! ] Failed Getting a module BaseAddress !";
                            return false;
                        }


                        if (offsets != null ) _tmpAddress = (long)GetAddressPointedTo(hProc, (IntPtr)_tmpAddress, offsets);
                        uint __oldPrto = 0x40;


                        //errMsg = _tmpAddress.ToString("X");
                        //return false;

                        if (VirtualProtectEx(hProc, (IntPtr)_tmpAddress, (UIntPtr)Value2Write.Length, 0x40, out __oldPrto))
                        {
                            int byte_written = 0;

                            if (WriteProcessMemory(hProc, (IntPtr)_tmpAddress, Value2Write, Value2Write.Length, ref byte_written))
                            {
                                VirtualProtectEx(hProc, (IntPtr)_tmpAddress, (UIntPtr)Value2Write.Length, __oldPrto, out _);
                                errMsg = null;
                                return true;
                            }
                            else errMsg = "[ ! ] Failed Writing Memory !";
                        }
                        else errMsg = $"[ ! ] VTPE {0x40} Failed .";

                        CloseHandle(hProc);
                    }
                    else errMsg = $"[ ! ] OP failed .";
                }
                else errMsg = $"[ ! ] Could not find {ModuleName} .";
            }
            else errMsg = $"[ ! ] GenshinImpact is not running !";
            return false;
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        static byte[] RelativeJumper(long d, long s, string startBytes="", string endBytes="")
        {
            long offs = d - s - 5;
            char[] _ = offs.ToString("X").Substring(8).Reverse().ToArray();
            for (int i = 0; i < 8; i++)
            {
                if (i % 2 == 0)
                {
                    var x = _[i + 1];
                    _[i + 1] = _[i];
                    _[i] = x;
                }
            }
            string str_bytes = startBytes + new string(_) + endBytes;
            byte[] ret_b = StringToByteArray(str_bytes);

            return ret_b;
        }

        public static bool HardCoreMemoryWrite_jump(out string errMsg, byte[] Value2Write, string ModuleName, IntPtr bAddress, int[] offsets , bool Reverse = false)
        {
            if (Settings.Default.GameProcess != null)
            {

                if (bAddress != IntPtr.Zero)
                {
                    IntPtr hProc = OpenProcess(PROCESS_ALL_ACCESS, false, Properties.Settings.Default.GameProcess.Id);

                    if (hProc != IntPtr.Zero)
                    {


                        long _tmpAddress = GetModuleBaseAddress(Properties.Settings.Default.GameProcess, ModuleName);

                        if (_tmpAddress != 0)
                        {
                            _tmpAddress += (long)bAddress;
                        }
                        else
                        {
                            errMsg = "[ ! ] Failed Getting a module BaseAddress !";
                            return false;
                        }


                        if (offsets != null) _tmpAddress = (long)GetAddressPointedTo(hProc, (IntPtr)_tmpAddress, offsets);
                        uint __oldPrto = 0x40;


                        //errMsg = _tmpAddress.ToString("X");
                        //return false;

                        if (VirtualProtectEx(hProc, (IntPtr)_tmpAddress, (UIntPtr)Value2Write.Length, 0x40, out __oldPrto))
                        {
                            int byte_written = 0;

                            if (!Reverse)
                            {
                                // Allocating the bytes and the jmp back
                                IntPtr valloc = VirtualAllocEx(hProc, IntPtr.Zero, (IntPtr)(Value2Write.Length), AllocationType.Reserve | AllocationType.Commit, MemoryProtection.ReadWrite);
                                GenPact_t00l.MainGenPact.WriteLogs(valloc.ToString("X"));

                                if (valloc != IntPtr.Zero)
                                {
                                    if (WriteProcessMemory(hProc, valloc, Value2Write, Value2Write.Length, ref byte_written))
                                    {
                                        byte[] JmpNearBytes = RelativeJumper((long)valloc, _tmpAddress, "E9", "6690");

                                        lastAllocationAddr = (long)valloc;
                                        LastNearJmp = JmpNearBytes;

                                        if (WriteProcessMemory(hProc, (IntPtr)_tmpAddress, JmpNearBytes, JmpNearBytes.Length, ref byte_written))
                                        {
                                            VirtualProtectEx(hProc, (IntPtr)_tmpAddress, (UIntPtr)JmpNearBytes.Length, __oldPrto, out _);
                                            errMsg = null;
                                            return true;
                                        }
                                        else errMsg = "[ ! ] Failed Writing The jumpNear Bytes !";
                                    }
                                    else
                                    {
                                        errMsg = "[ ! ] Failed Writing to the allocated Memory !";
                                    }
                                }
                                else errMsg = "[ ! ] Failed allocating Memory !";
                            }
                            else
                            {
                                // Removing the allocated mem
                                VirtualFreeEx(hProc, (IntPtr)lastAllocationAddr , LastNearJmp.Length, AllocationType.Reserve);

                                if (WriteProcessMemory(hProc, (IntPtr)_tmpAddress, Value2Write, Value2Write.Length, ref byte_written))
                                {
                                    VirtualProtectEx(hProc, (IntPtr)_tmpAddress, (UIntPtr)Value2Write.Length, __oldPrto, out _);
                                    errMsg = null;
                                    return true;
                                }
                                else errMsg = "[ ! ] Failed Writing Memory !";
                            }
                        }
                        else errMsg = $"[ ! ] VTPE {0x40} Failed .";

                        CloseHandle(hProc);
                    }
                    else errMsg = $"[ ! ] OP failed .";
                }
                else errMsg = $"[ ! ] Could not find {ModuleName} .";
            }
            else errMsg = $"[ ! ] GenshinImpact is not running !";
            return false;
        }




        public static JObject ReadJs()
        {
            try
            {
                if (!File.Exists(@"D:\VS2019 PROJ\repos\GenPact t00l\x64\Release\configs\ctrls.json")) File.Create(@"D:\VS2019 PROJ\repos\GenPact t00l\x64\Release\configs\ctrls.json");
                JObject o1 = JObject.Parse(File.ReadAllText(@"D:\VS2019 PROJ\repos\GenPact t00l\x64\Release\configs\ctrls.json"));

                return o1;
            }
            catch (Exception)
            {
                return null;
            }
        }


        

        public static void PlaySound(UnmanagedMemoryStream snd)
        {
            if (Settings.Default.Sound)
            {
                using (var player = new SoundPlayer(snd))
                {
                    player.Play();
                }
            }
        }






    }
}
