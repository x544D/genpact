using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace GenPact.__
{
    public partial class CooldownUc : UserControl
    {


        public CooldownUc()
        {
            InitializeComponent();
        }


        private void off_on_Switcher(object o, EventArgs a)
        {
            if (((PictureBox)o).Tag.ToString() == "off")
            {
                ((PictureBox)o).Image = Properties.Resources.on;
                ((PictureBox)o).Tag = "on";
                Controls[((PictureBox)o).Name + "Value"].Enabled = true;
            }
            else
            {
                ((PictureBox)o).Image = Properties.Resources.off;
                ((PictureBox)o).Tag = "off";
                Controls[((PictureBox)o).Name + "Value"].Enabled = false;
            }
        }

        private void CooldownUc_Load(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
          
                string _tmp = GenPactMem.GetKeyCombinaison();

                if (string.IsNullOrEmpty(_tmp)) SkillCD_SwitcherValue.Text += _tmp;
        }
    }
}
