using GenPact.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenPact_t00l
{
    static class Program
    {
        // Start the child process.
        static readonly Process proc = new Process();
        static readonly byte ln = 0x9;

        private static readonly string Server = "http://40.66.33.182:8000/0x5BBE/";
        private static readonly HttpClient client = new HttpClient();

        static async Task<string> req(string p)
        {
            var values = new Dictionary<string, string>
                {
                    { "GET", p },
                };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(Server, content);

            var responseString = await response.Content.ReadAsStringAsync();

            while (string.IsNullOrEmpty(responseString)) { }

            return responseString;
        }

        [STAThread]
        static void Main(string[] args)
        {

            try
            {
                if (args.Length != ln)
                {
                    MessageBox.Show("[ 0x5A ] Error Happend while launching !");
                    Environment.Exit(0x5A);
                }

                Settings.Default.anti_anti_dbg = Convert.ToBoolean(args[1]);
                Settings.Default.language = args[2];
                Settings.Default.startup = Convert.ToBoolean(args[3]);
                Settings.Default.hwnd_destroy = Convert.ToBoolean(args[4]);
                Settings.Default.Sound = Convert.ToBoolean(args[5]);
                Settings.Default.logs_as = Convert.ToBoolean(args[6]);
                Settings.Default.self_move = Convert.ToBoolean(args[7]);
                Settings.Default.r0 = Convert.ToBoolean(args[8]);


                MessageBox.Show(string.Join("\n", Settings.Default));

                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.Arguments = "/C wmic diskdrive get serialnumber";
                proc.Start();
                string output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
                output = output.Replace("\r", "").Replace(" ", "");
                string[] spl = output.Split('\n');
                output = "";

                foreach (var item in spl)
                {
                    if (!item.ToLower().Contains("serialnumber") && item != "\n" && !string.IsNullOrEmpty(item))
                    {
                        string enit = string.Empty;

                        for (int i = 0; i < item.Length; i++)
                        {
                            if (i % 2 == 0) enit += (char)(((int)item[i]) + 1);
                            else enit += (char)(((int)item[i]) - 1);
                        }

                        output += enit + "|";
                    }
                }

                output = output.Substring(0, output.Length - 1);
                string cr = req(output).Result;

                while (string.IsNullOrEmpty(cr)) { }

                if (cr.StartsWith("XX") )
                {
                    File.WriteAllText("logs/genPact.log", "Your Subscription is not Active !");
                    Environment.Exit(2); 
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error !");
                Environment.Exit(0);
            }


#if !DEBUG
            if (args.Length < 1)
            {
                MessageBox.Show("+ Please Use GenPact Launcher .");
                Environment.Exit(-1);
            }
            else
            {
                try
                {

                    Process _ = Process.GetProcessById(Convert.ToInt32(args[0], 16));

                    if (_ == null || _.ProcessName.ToLower() != "winlogon")
                    {
                        MessageBox.Show("+ Please Use the GenPact Launcher .");
                        Environment.Exit(-1);
                    }



                }
                catch (Exception)
                {
                    MessageBox.Show("+ Error > Please Use the GenPact Launcher .");
                    Environment.Exit(-1);
                }
            }

            try
            {
            }
            catch (Exception)
            {
                MessageBox.Show("Error 0x2");
                Environment.Exit(0);
            }


#endif



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainGenPact());
        }
    }
}
