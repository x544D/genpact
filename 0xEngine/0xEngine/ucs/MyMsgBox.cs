using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenGine.ucs
{
    public partial class MyMsgBox : UserControl
    {

        public enum MsgBoxRes : uint
        {
            Null,
            Yes,
            No
        }

        public uint res  = (uint)MsgBoxRes.Null;

        public MyMsgBox()
        {
            InitializeComponent();
        }


        private void Yes_Click(object sender, EventArgs e)
        {
            res = (uint)MsgBoxRes.Yes;
        }

        private void No_Click(object sender, EventArgs e)
        {
            res = (uint)MsgBoxRes.No;
        }
    }
}
