using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using GenPact.Properties;
using System.Collections.Generic;

namespace GenPact.__
{
    public partial class SpeedUc : UserControl
    {

        Dictionary<string, string> dico = new Dictionary<string, string>() {

                { "en" , "+ GenPact [VIP]" },
                { "fr" , "+ Fr GenPact [VIP]" },
                { "cn" , "+ Cn GenPact [VIP]" },
                { "ar" , "+ Ar GenPact [VIP]" },

        };



        int cnt = 1;

        const string MainModuleName = "unityplayer.dll";
        const Int64 ModuleOffset = 0x19018E8;
        int[] offset = { 0xFC };
        Process hProc;
        Action<string, Color?, bool> WriteLogs;


        public SpeedUc(Action<string, Color?, bool> WriteLogs)
        {
            InitializeComponent();
            hProc = Settings.Default.GameProcess;
            this.WriteLogs = WriteLogs;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                GenPactMem.PlaySound(Resources.sound_you_dumb_fuck);

                DialogResult res = MessageBox.Show("Going over 5 might get you BANNED !", "Are you dumb ?", MessageBoxButtons.YesNo);

                if (res == DialogResult.Yes)
                {

                    DialogResult res1 = MessageBox.Show("Well i guess you are Dumb !\nAre you realy sure you want this ?", "Hmm ...", MessageBoxButtons.YesNo);
                    if (res1 == DialogResult.Yes)
                    {
                        SpeedBar.Minimum = 1;
                        SpeedBar.Maximum = 20;
                        SpeedBar.Update();
                        SpeedBar.Refresh();
                    }
                    else
                    {
                        checkBox1.Checked = false;
                        checkBox1.Update();
                        checkBox1.Refresh();
                        SpeedBar_Scroll(null, null);
                    }
                }
                else
                {
                    checkBox1.Checked = false;
                }
            }
            else
            {
                SpeedBar.Minimum = 1;
                SpeedBar.Maximum = 5;
                SpeedBar.Update();
                SpeedBar.Refresh();
                SpeedBar_Scroll(null, null);
            }
        }

        private void off_on_Switcher(object o, EventArgs a)
        {
            PictureBox _ref_tmp = ((PictureBox)o);

            if (_ref_tmp.Tag.ToString() == "off")
            {
                if (_ref_tmp.Name.Contains("UltiCD"))
                {
                    GenPactMem.PlaySound(Resources.sound_UltiCD_on);
                    ulti_cd('n');
                }
                else
                {
                    GenPactMem.PlaySound(Resources.sound_SkillCD_on);
                }

                _ref_tmp.Image = Resources.toggleon;
                _ref_tmp.Tag = "on";
                Controls[_ref_tmp.Name + "Value"].Enabled = false;
            }
            else
            {
                if (_ref_tmp.Name.Contains("UltiCD"))
                {
                    GenPactMem.PlaySound(Resources.sound_UltiCD_off);
                    ulti_cd('f');
                }
                else
                {
                    GenPactMem.PlaySound(Resources.sound_SkillCD_off);
                }
                _ref_tmp.Image = Resources.toggleoff;
                _ref_tmp.Tag = "off";
                Controls[_ref_tmp.Name + "Value"].Enabled = true;
            }
        }


        private void SpeedBar_Scroll(object sender, EventArgs e)
        {
            string __err = "";
            
            CurrentSpeed.Text = SpeedBar.Value.ToString();
            bool res = GenPactMem.HardCoreMemoryWrite(out __err, BitConverter.GetBytes((float)SpeedBar.Value), MainModuleName, (IntPtr)ModuleOffset, offset) ;

            if (!res) WriteLogs(__err, Color.Red, true);
            else WriteLogs($"[ {++cnt} ] Succeded on changing the value !", Color.AliceBlue, true);

        }

        private void SpeedUc_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            //checkBox1.Text = dico[Settings.Default.language];
        }

        public void SynchroniseUi()
        {
            this.Update();
            this.Refresh();
        }


        private void ulti_cd(char _key)
        {
            string _err = null;
            byte[] ActualBytes = { 0x0F, 0x10, 0x70, 0x30 };
            long UltiCD_BaseAddress = 0x1CCE771;
            string UltiCD_ModuleName = "userassembly.dll";
            if (_key == 'n') ActualBytes = new byte[] { 0x0, 0x0, 0x0, 0x0 };

            if (GenPactMem.HardCoreMemoryWrite(out _err, ActualBytes , UltiCD_ModuleName , (IntPtr)UltiCD_BaseAddress, null))
            {
                if (_key == 'n') WriteLogs($"[ {++cnt} ] Success -> UltiCD is ON !", Color.YellowGreen, true);
                else WriteLogs($"[ {++cnt} ] Success -> UltiCD is OFF !", Color.YellowGreen, true);
            }
            else WriteLogs($"[ {++cnt} ] {_err}", Color.Red, true);
        }


        private void skill_cd(char _key)
        {
            string _err = null;

            byte[] ActualBytes = { 0xF3 , 0x0F, 0x58 , 0x7B, 0x70 };
            long UltiCD_BaseAddress = 0x1CCFD0D;
            string UltiCD_ModuleName = "userassembly.dll";
            if (_key == 'n') ActualBytes = new byte[] { 0x90, 0x90, 0x90, 0x90 };

            if (GenPactMem.HardCoreMemoryWrite(out _err, ActualBytes, UltiCD_ModuleName, (IntPtr)UltiCD_BaseAddress, null))
            {
                WriteLogs($"[ {++cnt} ] Success -> UltiCD is ON !", Color.YellowGreen, true);
            }
            else WriteLogs($"[ {++cnt} ]  Error happend -> {_err} !", Color.Red, true);
        }

    }
}
