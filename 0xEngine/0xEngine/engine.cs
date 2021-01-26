using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GenGine
{
    class engine
    {
        
        

        static string[] excluded = new string[] {
            "svchost",
            "opera",
            "lsass",
            "services",
            Application.ProductName,
        };

        public static Process[] getProcs()
        {        
            Process[] _procs = Process.GetProcesses().Where((e) => !excluded.Contains(e.ProcessName) && e.MainWindowHandle != IntPtr.Zero).ToArray();
            return _procs;
        }


        public static List<ct_class> extract_ct(string path)
        {
            List<ct_class> _cts = new List<ct_class>();

            if (path != null)
            {
                try
                {
                    XmlDocument doc_xml = new XmlDocument();
                    doc_xml.Load(path);

                    XmlNodeList cheats = doc_xml.SelectNodes("CheatTable/CheatEntries/CheatEntry");

                    foreach (XmlNode item in cheats)
                    {
                        int id;
                        if (int.TryParse(item.SelectSingleNode("ID").InnerText, out id))
                        {
                            string desc = item.SelectSingleNode("Description").InnerText ?? $"No description - ID {id.ToString()} ";
                            //string color = item.SelectSingleNode("Color").InnerText ?? "FFFFFF";
                            string asm = item.SelectSingleNode("AssemblerScript").InnerText;

                            if (asm != null)
                            {
                                asm_class _asm = ExtractAsm(asm);
                                
                                if (_asm != null) _cts.Add(new ct_class(id, desc, _asm));
                            }
                        }
                    }

                    return _cts;
                }
                catch (Exception)
                {
                    MessageBox.Show("[-] Please Open A Correct CT file. \n[Must have scripts only] !");
                    return null;
                }
            }

            return null;
        }


        // this func n9edr n7tajha after [VIP users]
        // so better nkhaliha static  + public
        public static asm_class ExtractAsm(string asm)
        {
            try
            {
                asm_class tempAsm = null;

                // ASM CLASS ATTRS

                int AllocSize = 0;

                string  ModuleName  = null;
                IntPtr  Offset      = IntPtr.Zero;
                IntPtr  FullAddress = IntPtr.Zero;

                List<byte>  OriginalBytes = new List<byte>();
                List<byte> FakeBytes = new List<byte>();

                ////////////////////////

                bool isAOB          = asm.Contains("aobscanmodule");
                string[] asmLines   = asm./*Substring(asm.IndexOf("[ENABLE]") + 1, asm.LastIndexOf("[DISABLE]") - asm.IndexOf("[ENABLE]") - 1)
                                        .*/Split('\n').Where(line => !line.Trim().StartsWith("//") && !string.IsNullOrEmpty(line.Trim()))
                                        .ToArray();

                // we will have to remove amy // coments from the script


                // AllocSize
                // either aob or normal injec
                asmLines.First(line => line.Contains("alloc"))
                .Replace("$", "").Replace(")", "").Split(',')
                .Where(allocSize => int.TryParse(allocSize, out AllocSize));

                if (isAOB)
                {
                    byte bb;

                    string[] Mod_AOB = asmLines.First(line => line.Contains("aobscanmodule")).
                    Replace(")", "").Split(',').ToArray();

                    // module name ex : saad.dll
                    ModuleName      = Mod_AOB[1];
                    // Getting them bytes
                    foreach (string b in Mod_AOB[2].Split(' ')) if (byte.TryParse(b, out bb)) OriginalBytes.Add(bb);

                    // either get FullAddrss or offset
                    foreach (string line in asm.Split('\n').
                                Where(l => l.Contains(ModuleName) && l.Contains("+") ||
                                l.Contains("ORIGINAL CODE - INJECTION POINT") ).ToArray()
                            )
                    {
                        //Try and get offset of our module (inj pt)
                        if (line.Contains('+')) Offset = new IntPtr(Convert.ToInt32(line.Split('+')[1].Trim().Replace("\n", ""), 16));
                        // if fail try to get the fullAddress yla kant
                        else FullAddress = new IntPtr(Convert.ToInt32(line.Split(':')[1].Trim().Replace("\n", ""), 16));
                        // ba9i getting Fake butes
                    }

                }
                else
                {

                }

                return tempAsm;
            }
            catch (Exception)
            {
                return null;
            }

        }


        // Get Recent CT files IN A DICTIONARY
        //public static Dictionary<> get_recent_opened_cts ()
        //{

        //}

        // add opened/ browsed File to the list of recent file if it does not exists
        public static Dictionary<int, object[]> get_recent_opened_cts()
        {
            try
            {
                Dictionary<int, object[]> FilesRecentlyOpened = new Dictionary<int, object[]>();
                string _recentFile = Application.CommonAppDataPath + "\\recent.txt";

                if (File.Exists(_recentFile))
                {
                    string[] lines = File.ReadAllLines(_recentFile);
                    int c = 0; // hadi ghi en cas goto Ignore khedmat  (we are calling this method on recent_Opened_Cts) in a for loop i++
                    for (int i = 0; i < lines.Length; i++)
                    {
                        //fileName
                        string name = lines[i].Substring(0, lines[i].IndexOf('|'));
                        //File Path  | Dir
                        string p = lines[i].Substring(lines[i].IndexOf('|') + 1, lines[i].LastIndexOf('|') - lines[i].IndexOf('|') - 1);
                        //LastTime Opened [Datetime]
                        string time = lines[i].Substring(lines[i].LastIndexOf('|') + 1, lines[i].Length - lines[i].LastIndexOf('|') - 1);

                        DateTime _time;
                        bool isDT = DateTime.TryParse(time, out _time);
                        if (!isDT) goto Ignore;
                        else c++;
                        

                        string fullFilePath = p.EndsWith(@"\") ? p + name : p + @"\" + name;
                        if (File.Exists(fullFilePath))
                        {
                            FilesRecentlyOpened.Add(c, new object[] { name, p.EndsWith(@"\") ? p : p + @"\", _time });
                        }

                        Ignore:;
                    }
                    return FilesRecentlyOpened;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
