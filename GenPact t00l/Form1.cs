using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.IO;
using GenPact.__;
using GenPact;
using GenPact.Properties;
using System.Collections.Generic;
using GenPact.__.Sub_Panel_Features_Ucs;

namespace GenPact_t00l
{

    public partial class MainGenPact : Form
    {        


        #region DECLARATIONS

        Control Currently_In_Front_Uc;
        private Process Watcher = null;

        bool genshin_was_not_running = true;
        const string news_link = "http://x544d.github.io/genpact/GenPact";
        const string donate_link = "https://www.paypal.me/Omrani007";
        const string GenPact_download = "http://x544d.github.io/genpact/genpact.zip";
        const string NewVersionMessage = "+ New Version out !\nYou will be redirected to a download link.\n\nIf the zip has a password please reach me Privately on discord.\nDisocrd : Kaizuku#4781";
        const string PopUpKeyword = "[P]";
        const string ForcedPopUpKeyword = "[FP]";
        const string version = "v1.1";
        const string AndroidActionsFile = "logs/android.log";
        const string GenGineActionsFile = "logs/gengine.log";

        public static DisabledRichTextBox LogsRichTb;


        int miliseconds2wait = 500;
        string news = "[ ! ] Failed to fetch news from the server .";


        Point clickPt;
        bool isDrging = false;
        Color successColor = Color.Lime;
        Color onHoldColor = Color.Orange;
        Color failedColor = Color.Red;
        const string GenshinProc = "genshinimpact";//mspaint

        #endregion

        #region UserControls

        public SpeedUc speedUc;
        public HomeUc homeUc;
        public FeaturesUC featuresUc;
        public SettingsUc settingsUc;
        public AndroidVersionUc androidVersionUc;
        public GenGineUc genGineUc;


        private void UcInitialize()
        {
            speedUc = new SpeedUc(WriteLogs)
            {
                Parent = ContentPanel,
                Dock = DockStyle.Fill,
                Visible = false
            };

            homeUc = new HomeUc()
            {
                Parent = ContentPanel,
                Dock = DockStyle.Fill,
                Visible = false
            };

            featuresUc = new FeaturesUC()
            {
                Parent = ContentPanel,
                Dock = DockStyle.Fill,
                Visible = false
            };
 
            settingsUc = new SettingsUc()
            {
                Parent = ContentPanel,
                Dock = DockStyle.Fill,
                Visible = false
            };

            genGineUc = new GenGineUc()
            {
                Parent = ContentPanel,
                Dock = DockStyle.Fill,
                Visible = false
            };

            androidVersionUc = new AndroidVersionUc()
            {
                Parent = ContentPanel,
                Dock = DockStyle.Fill,
                Visible = false
            };
        }



        #endregion

        #region MY FUNCTIONS

        private void LaunchWtahcer()
        {
            if (!File.Exists($"{Application.StartupPath}\\Watcher.exe"))
            {
                MessageBox.Show("+ GenPact Files seems to be broken , Re-download !");
                Environment.Exit(0);
            }

            ProcessStartInfo pf = new ProcessStartInfo
            {
                FileName = $"{Application.StartupPath}\\Watcher.exe",
                Arguments = $"{Process.GetCurrentProcess().Id}",
                CreateNoWindow = true,
                UseShellExecute = false
            };
            Watcher = Process.Start(pf);
        }

        private Process CheckGenShinProcess() => Process.GetProcesses().Where(p => p.ProcessName.ToLower() == GenshinProc).FirstOrDefault();

        public static void WriteLogs(string t, Color? color = null, bool append = false)
        {
            if (color != null) LogsRichTb.ForeColor = (Color)color;

            if (append) LogsRichTb.Text += $"\n{t}";
            else
            {
                LogsRichTb.Clear();
                LogsRichTb.Text = $"\n{t}";
            }

        }


        private void MenuClick_Item(object sender , EventArgs a )
        {
            if (((Control)sender).Name ==  "GenGineBtn")
            {
                if (File.Exists($"{Application.StartupPath}\\GenGine.exe")) 
                {
                    MessageBox.Show("Keep in mind this is still Under construction !\n");
                    Process.Start($"{Application.StartupPath}\\GenGine.exe");
                }

                return;
            }

            foreach (Control ctrl in ContentPanel.Controls)
            {
                if (ctrl.Tag == ((Control)sender).Tag)
                {
                    Synch_Environment(ctrl);
                    break;
                }
            }

        }


        private void Synch_Environment(Control _to_show_ctrl_)
        {
            if (_to_show_ctrl_ != Currently_In_Front_Uc)
            {
                if(Currently_In_Front_Uc != null) Currently_In_Front_Uc.Visible = false;

                Currently_In_Front_Uc = _to_show_ctrl_;
                Currently_In_Front_Uc.Visible = true;

                SelectedFeature.Text = $"[ {Currently_In_Front_Uc.Tag} ]";
            }


            //Dictionary<string, string> dico = new Dictionary<string, string>() {

            //    { "en" , "+ GenPact [VIP]" },
            //    { "fr" , "+ Fr GenPact [VIP]" },
            //    { "cn" , "+ Cn GenPact [VIP]" },
            //    { "ar" , "+ Ar GenPact [VIP]" },

            //};

            title.Text = "+ GenPact [VIP]"; // dico[Settings.Default.language];

            if (Currently_In_Front_Uc != null)
            {
                Currently_In_Front_Uc.Update();
                Currently_In_Front_Uc.Refresh();
            }

            Update();
            Refresh();

        }


        #endregion

        public MainGenPact()
        {
            LaunchWtahcer();

            InitializeComponent();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            
        }


        private void closeBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }



        private void Form1_Load(object sender, EventArgs e)
        {

            #region INITIALISATIONS

            Directory.CreateDirectory("logs"); 
            if (!File.Exists(AndroidActionsFile)) File.Create(AndroidActionsFile);
            if (!File.Exists(GenGineActionsFile)) File.Create(GenGineActionsFile);




            LogsRichTb = new DisabledRichTextBox
            {
                //Dock = DockStyle.Fill,
                ReadOnly = true,
                BackColor = Color.Black,
                BorderStyle = BorderStyle.None,
                ForeColor = Color.Orange,
                Parent = LogsPanel,
                Bounds = new Rectangle(10, 0, LogsPanel.Width - 11, LogsPanel.Height)
            };


            UcInitialize();
            Synch_Environment(homeUc);

            

            titlePanel.MouseMove += TitlePanel_MouseMove;
            titlePanel.MouseDown += TitlePanel_MouseDown;
            titlePanel.MouseUp += TitlePanel_MouseUp;

            //chestEspTip.SetToolTip(chestEspBtn, "Ch3sts h@ck");
            //Speed_Cd_Tip.SetToolTip(Speed_Cd_btn, "Sp3ed & Cd h@cks");
            //OneHitKillTip.SetToolTip(OneHitKillBtn, "One hit Kill h@ck");
            //GodModTip.SetToolTip(GodModBtn, "g0d Mode h@ck");
            //InfinitStaminaTip.SetToolTip(InfinitStamina, "Endless Stam!na h@ck");
            GenGineTip.SetToolTip(GenGineBtn, "GenGine (mini CE)");
            AndroidVersionTip.SetToolTip(AndroidVersionBtn, "Hack Android Version");
            ClearLogsTip.SetToolTip(ClearLogsBtn, "Clear Logs");
            SaveLogsTip.SetToolTip(SaveLogsBtn, "Save Logs");
            SeekHelpTip.SetToolTip(SeekHelpBtn, "Seek for help");

            stateColor.BackColor = onHoldColor;

            WriteLogs("[ + ] Waiting for GenshinImpact ...", onHoldColor);
            #endregion

            #region THREADS


            try
            {
#if !DEBUG
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(news_link);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    news = reader.ReadToEnd();

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

                                MessageBox.Show(NewVersionMessage);
                                Process.Start(pin);
                                Environment.Exit(0);
                            }
                            else
                            {
                                if (news.StartsWith(ForcedPopUpKeyword))
                                {
                                    MessageBox.Show(news);
                                }

                                else if (news.StartsWith(PopUpKeyword) && !File.Exists("GenPact.dmp"))
                                {
                                    MessageBox.Show(news);
                                    File.Create("GenPact.dmp");
                                }

                            }
                        }
                        else Environment.Exit(0);
                    }
                    else Environment.Exit(0);

                }




                new Thread(new ThreadStart(() =>
                {

                    while (true)
                    {
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(news_link);
                        req.AutomaticDecompression = DecompressionMethods.GZip;

                        using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                        using (Stream stream = response.GetResponseStream())
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string __ = reader.ReadToEnd();
                            string[] _news = __.Split('\n');

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

                                        BeginInvoke(new Action(() =>
                                        {
                                            Hide();
                                        }));

                                        MessageBox.Show(NewVersionMessage);
                                        Process.Start(pin);
                                        Environment.Exit(0);
                                    }
                                    else
                                    {
                                        if (__.StartsWith(ForcedPopUpKeyword))
                                        {
                                            news = __;
                                            MessageBox.Show(news);
                                        }
                                    }
                                }
                                else Environment.Exit(0);
                            }
                            else Environment.Exit(0);
                        }
                        Thread.Sleep(60000);
                    }

                })).Start();


#endif

            new Thread(new ThreadStart(() =>
            {
                if (GenPactMem.jsControls == null) GenPactMem.jsControls = GenPactMem.ReadJs();

                while (true)
                {
                    for (int i = 32; i < 127; i++)
                    {
                        int kState = GenPactMem.GetAsyncKeyState(i);

                        if (kState == -32768)
                        {
                            string ch = Convert.ToString((char)i);
                            if (GenPactMem.jsControls.ContainsKey(ch))
                            {
                                try
                                {
                                    //MessageBox.Show($"Clicked -> {ch}");
                                    string feature = (string)GenPactMem.jsControls[ch].ToObject(typeof(string));
                                    if (!string.IsNullOrEmpty(feature))
                                    {
                                        foreach (Control c in featuresUc.Controls)
                                        {
                                            if (c.Tag != null && c is PictureBox)
                                            {
                                                if (c.Tag.ToString().Contains(feature))
                                                {
                                                    featuresUc.BeginInvoke(new Action(() =>
                                                    {
                                                        featuresUc.pictureBox8_Click(c as PictureBox, null);
                                                    }));
                                                }
                                            }
      
                                        }
                                    }
                                }
                                catch(Exception){ throw; }
                            }
                        }
                    }
                    Thread.Sleep(50);
                }
            })).Start();

            new Thread(new ThreadStart(() =>
                {
                    while (true)
                    {
                        if (Watcher.HasExited)
                        {
                            LaunchWtahcer();
                            WriteLogs("[ ! ] - Relaunched Watcher !", append:true);
                        }

                        Settings.Default.GameProcess = CheckGenShinProcess();

                        BeginInvoke(new Action(() =>
                        {
                            if (genshin_was_not_running)
                            {
                                if (Settings.Default.GameProcess != null)
                                {
                                    stateColor.BackColor = successColor;
                                    WriteLogs("[ + ] Found Genshin Impact .", successColor);
                                    ContentPanel.Enabled = true;
                                    MenuPanel.Enabled = true;
                                    genshin_was_not_running = false;
                                }
                            }
                            else
                            {
                                if (Settings.Default.GameProcess == null)
                                {
                                    stateColor.BackColor = onHoldColor;
                                    WriteLogs("[ + ] Waiting for GenshinImpact ...", onHoldColor);
                                    ContentPanel.Enabled = false;
                                    MenuPanel.Enabled = false;
                                    genshin_was_not_running = true;
                                    miliseconds2wait = 500;
                                }
                            }

                        }));
                        Thread.Sleep(miliseconds2wait);
                    }

                })).Start();
            }
            catch (ThreadAbortException)
            {
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("\n[ ! ] " + ex.Message);
                Environment.Exit(0);
            }



#endregion

        }

#region TitleBarMoving
        private void TitlePanel_MouseUp(object sender, MouseEventArgs e)
        {
            isDrging = false;
            Cursor = Cursors.Default;
        }

        private void TitlePanel_MouseDown(object sender, MouseEventArgs e)
        {
            isDrging = true;
            clickPt = e.Location;
            Cursor = Cursors.SizeAll;
        }

        private void TitlePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrging)
            {

                int dx = clickPt.X - e.Location.X;
                int dy = clickPt.Y - e.Location.Y;

                Location = new Point(Location.X - dx, Location.Y - dy);
            }
        }

#endregion

#region FOOTER
        private void label6_Click(object sender, EventArgs e)
        {
            Process.Start(donate_link);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(news);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("+ For now you can reach me on Discord Kaizuku#4781 !");
        }

        private void label5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("+ My Discord : Kaizuku#4781");
        }


#endregion

        private void RightSideMenu_Click(object sender, EventArgs e)
        {
            string flag = ((Control)sender).Tag.ToString();

            if (!string.IsNullOrEmpty(flag))
            {
                var _ = Resources.ResourceManager.GetStream($"sound_{flag}_lang");
                if (_ != null) GenPactMem.PlaySound(_);

                Settings.Default.language = flag;
                title_bar_lang.Text = $"[ {flag.ToUpper()} ]";
                Synch_Environment(Currently_In_Front_Uc);

            }
        }

        private void ClearLogsBtn_Click(object sender, EventArgs e)
        {
            LogsRichTb.Clear();
            LogsRichTb.Update();
            LogsRichTb.Refresh();
        }

        private void SaveLogsBtn_Click(object sender, EventArgs e)
        {
            string dt = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            File.WriteAllText($"logs/{dt}.log", LogsRichTb.Text);
            WriteLogs($"[ @ ] Saved Logs as : {dt}" , successColor, append:true);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            PictureBox ref_ctrl = ((PictureBox)sender);

            if (ref_ctrl.Tag.ToString() == "Sound_on")
            {
                GenPactMem.PlaySound(Resources.sound_Sound_off);

                ref_ctrl.Tag = "Sound_off";
                ref_ctrl.Image = Resources.sound_off;
                Settings.Default.Sound = false;
            }
            else
            {

                ref_ctrl.Tag = "Sound_on";
                ref_ctrl.Image = Resources.sound_on;
                Settings.Default.Sound = true;

                GenPactMem.PlaySound(Resources.sound_Sound_on);

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Synch_Environment(featuresUc);
        }
    }
}
