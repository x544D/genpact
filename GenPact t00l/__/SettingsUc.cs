using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace GenPact.__
{
    public partial class SettingsUc : UserControl
    {
        RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        public SettingsUc()
        {
            InitializeComponent();
        }

        private void WinStartupCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (WinStartupCheck.Checked)
            {
                registryKey.SetValue("GenPactLauncher", $"{Application.StartupPath}\\GenPactLauncher.exe");
            }
            else
            { 
                registryKey.DeleteValue("GenPactLauncher");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("On work .");
        }
    }
}
