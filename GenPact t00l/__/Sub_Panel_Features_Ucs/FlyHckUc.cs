using System;
using System.Drawing;
using System.Windows.Forms;
using GenPact_t00l;

namespace GenPact.__.Sub_Panel_Features_Ucs
{
    public partial class FlyHckUc : UserControl
    {
        
        public FlyHckUc()
        {
            InitializeComponent();
        }

        private void FlyHckUc_Load(object sender, EventArgs e)
        {

        }

        private void FlyHack_Switcher_Click(object sender, EventArgs e)
        {
            if (FlyHack_Switcher.Tag.ToString() == "OFF")
            {
                // Gravity Bytes
                Memory_Writer(new byte[] { 0xE9, 0xA0, 0xD8, 0x31, 0xFF, 0x66, 0x90 }, 0xCD275B, "userassembly.dll");
                // Hover Bytes
                Memory_Writer_jump(new byte[] { 0xB8, 0x01, 0x00, 0x00, 0x00, 0xE9, 0x0D, 0x22, 0x20, 0x02 }, 0x21E2210, "userassembly.dll"); 

                FlyHack_Switcher.Image = Properties.Resources.on;
                FlyHack_status.Text = "ON";
                FlyHack_status.ForeColor = Color.GreenYellow;
                FlyHack_Switcher.Tag = "ON";
            }
            else
            {
                // Gravity Bytes
                Memory_Writer(new byte[]{ 0x83, 0x78, 0x24, 0x00, 0x0F, 0x94, 0xC0 } , 0xCD275B, "userassembly.dll");
                // Hover Bytes

                /*
                 0xB8, 0x01, 0x00, 0x00, 0x00, 0xE9, 0x0D, 0x22, 0x20, 0x02

                 7FFEC2620000 - B8 01000000           - mov eax,00000001 { 1 }
                 7FFEC2620005 - E9 0D222002           - jmp UserAssembly.dll+21E2217


                 UserAssembly.dll+21E2210 - 0F B6 81 68 01 00 00     

                 */
                Memory_Writer_jump(new byte[]{ 0x0F, 0xB6, 0x81, 0x68, 0x01, 0x00, 0x00 } , 0x21E2210, "userassembly.dll" , rev:true);

                FlyHack_Switcher.Image = Properties.Resources.off;
                FlyHack_status.Text = "OFF";
                FlyHack_status.ForeColor = Color.Red;
                FlyHack_Switcher.Tag = "OFF";
            }
        }


        private void Memory_Writer(byte[] _Bytes, long bAddr , string ModuleName , string successMsg = "[ + ] Success !")
        {
            string _err = null;
            if (GenPactMem.HardCoreMemoryWrite(out _err, _Bytes, ModuleName, (IntPtr)bAddr, null)) MainGenPact.WriteLogs(successMsg, Color.Red, true);
            else MainGenPact.WriteLogs($"{_err}", Color.Red, true);
        }

        private void Memory_Writer_jump(byte[] _Bytes, long bAddr, string ModuleName, string successMsg = "[ + ] Success !" , bool rev = false)
        {
            string _err = null;
            if (GenPactMem.HardCoreMemoryWrite_jump(out _err, _Bytes, ModuleName, (IntPtr)bAddr, null , rev)) MainGenPact.WriteLogs(successMsg, Color.Red, true);
            else MainGenPact.WriteLogs($"{_err}", Color.Red, true);
        }
    }
}
