using System;
using System.Drawing;
using System.Windows.Forms;
using GenPact_t00l;

namespace GenPact.__.Sub_Panel_Features_Ucs
{
    public partial class FeaturesUC : UserControl
    {
        string LastErrorString = string.Empty;

        public FeaturesUC()
        {
            InitializeComponent();
        }


       

        public void pictureBox8_Click(object sender, EventArgs e)
        {
            var c = ((PictureBox)sender);
            string[] tag = c.Tag.ToString().Split('_');
            
            if (tag.Length >= 2)
            {

                if (tag[0] == "off")
                {

                    if (!Activate(tag[1]))
                    {
                        MainGenPact.WriteLogs($"[ ! ] {tag[1]} Failed To Activate !", Color.Red, true);
                        return;
                    }

                    MainGenPact.WriteLogs($"[ + ] {tag[1]} Activated Successfully ! ", Color.GreenYellow, true);
                    c.Tag = $"on_{tag[1]}";
                    c.Image = Properties.Resources.toggleon;
                    c.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    if (!Deactivate(tag[1]))
                    {
                        MainGenPact.WriteLogs($"[ ! ] {tag[1]} Failed To Deactivate !", Color.Red, true);
                        return;
                    }


                    MainGenPact.WriteLogs($"[ + ] {tag[1]} Deactivated Successfully ! ", Color.GreenYellow, true);
                    c.Tag = $"off_{tag[1]}";
                    c.Image = Properties.Resources.toggleoff;
                    c.BorderStyle = BorderStyle.None;
                }
            }
        }


        private bool Activate(string key)
        {
            switch (key)
            {
                case "InstantCharge":
                    return GenPactMem.HardCoreMemoryWrite(out LastErrorString, new byte[] { 0x90, 0x90, 0x90, 0x90 }, "userassembly.dll", (IntPtr)0x346DCCF, null);

                case "FastAttacks":
                    return GenPactMem.HardCoreMemoryWrite(out LastErrorString, new byte[] { 0x41, 0xC7, 0x07, 0x01, 0x44, 0x88, 0x67, 0x7C }, "userassembly.dll", (IntPtr)0x120FF84 , null);

                case "Esp Treasure Chest":
                    if (!GenPactMem.HardCoreMemoryWrite(out LastErrorString, new byte[] { 0x75 }, "userassembly.dll", (IntPtr)0x1C6F317, null)) return false;
                    return GenPactMem.HardCoreMemoryWrite(out LastErrorString, new byte[] { 0x75 }, "userassembly.dll", (IntPtr)0x1C6F39A, null);

                case "Monster's Level":
                    return GenPactMem.HardCoreMemoryWrite(out LastErrorString, new byte[] { 0x0F, 0x84 }, "userassembly.dll", (IntPtr)0x125AD3D, null);

                case "Monster's Hp":
                    return GenPactMem.HardCoreMemoryWrite(out LastErrorString, new byte[] { 0x74 }, "userassembly.dll", (IntPtr)0x12597BB, null);

                default:
                    return false;
            }
            
        }

        private bool Deactivate(string key)
        {

            switch (key)
            {
                

                case "InstantCharge":
                    return GenPactMem.HardCoreMemoryWrite(out LastErrorString, new byte[] { 0x0F, 0x11, 0x47, 0x10 }, "userassembly.dll", (IntPtr)0x346DCCF, null);

                case "FastAttacks":
                    return GenPactMem.HardCoreMemoryWrite(out LastErrorString, new byte[] { 0x41, 0x0F, 0x11, 0x07, 0x44, 0x88, 0x67, 0x7C }, "userassembly.dll", (IntPtr)0x120FF84, null);

                case "Esp Treasure Chest":
                    if (!GenPactMem.HardCoreMemoryWrite(out LastErrorString, new byte[] { 0x74 }, "userassembly.dll", (IntPtr)0x1C6F317, null)) return false;
                    return GenPactMem.HardCoreMemoryWrite(out LastErrorString, new byte[] { 0x74 }, "userassembly.dll", (IntPtr)0x1C6F39A, null);

                case "Monster's Level":
                    return GenPactMem.HardCoreMemoryWrite(out LastErrorString, new byte[] { 0x0F, 0x87 }, "userassembly.dll", (IntPtr)0x125AD3D, null);

                case "Monster's Hp":
                    return GenPactMem.HardCoreMemoryWrite(out LastErrorString, new byte[] { 0x76 }, "userassembly.dll", (IntPtr)0x12597BB, null);

                default:
                    return false;
            }
        }

        private void FeaturesUC_Load(object sender, EventArgs e)
        {
            tt_1hk.SetToolTip(onehkPic, "One Hit Kill");
            tt_bow.SetToolTip(bowPic, "Instant Chanrges");
            tt_chest.SetToolTip(chestPic, "Chest Esp");
            tt_fov.SetToolTip(fovPic, "FOV - zooming");
            tt_gm.SetToolTip(gmPic, "GodMod");
            tt_rapidf.SetToolTip(rapidfPic, "Fast Attacks");
            tt_stamina.SetToolTip(staminaPic, "Infinit Stamina");
            tt_tp.SetToolTip(tpPic, "Teleporting");
            tt_Wing.SetToolTip(flyPic, "FlyMod - wing Mod");

            tt_EspChest.SetToolTip(EspChestPic, "Show Treasure Chests");
            tt_EspHp.SetToolTip(espHpPic, "Show Monster's HP");
            tt_EspLevel.SetToolTip(EspLvlPic, "Show Monster's Level");
        }
    }
}
