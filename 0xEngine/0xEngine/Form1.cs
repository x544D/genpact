using GenGine.ucs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenGine
{
    public partial class Form1 : Form
    {

        #region MoveFormShit
        private void md(object s, MouseEventArgs m)
        {
            sets.isDragging = true;
            sets.onClickLocation = m.Location;
        }

        private void mu(object s, MouseEventArgs m)
        {
            sets.isDragging = false;
        }

        private void mm(object s, MouseEventArgs m)
        {
            if (sets.isDragging)
            {
                int dx = sets.onClickLocation.X - m.Location.X;
                int dy = sets.onClickLocation.Y - m.Location.Y;

                Location = new Point(Location.X - dx, Location.Y - dy);
            }
        }
        #endregion


        Properties.Settings sets = new Properties.Settings();
        MyMsgBox MsgBox = null;
        UserControl uc_controller = null;
        UserControl _ct_save_current_state = new ctables() { Tag = "first" , Dock = DockStyle.Fill};


        public Form1()
        {
            InitializeComponent();
            if (!File.Exists(Application.CommonAppDataPath + "\\recent.txt")) File.Create(Application.CommonAppDataPath + "\\recent.txt");
            //engine.Update_last_opened_cts_File();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            uc_controller = new processes();
            ((processes)uc_controller).OnProcessNextButtonClick += (o, ea) => {
                Menu_Clicks_Handler(pMenu2, new EventArgs());
            };
            op_container.Controls.Add(uc_controller);
            uc_controller.Dock = DockStyle.Fill;
        }





        private void Menu_Clicks_Handler(object sender, EventArgs e)
        {

            Control clicked = sender as Control;
            string MainMenuItemName = string.Empty;

            if (clicked.Tag.ToString() == "next") MainMenuItemName = "pMenu1"; // hadi yla clicka next f userControl processes
            else MainMenuItemName = "pMenu" + clicked.Tag.ToString();


            if (sets.clickedMenuItemTag != clicked.Tag.ToString())
            {
                string OldMenuItemName = "pMenu" + sets.clickedMenuItemTag;
                menu_Container.Controls[OldMenuItemName].BringToFront();
                menu_Container.Controls[MainMenuItemName].SendToBack();

                if (uc_controller != null) uc_controller.Dispose();
                _ct_save_current_state.Visible = false;


                switch (MainMenuItemName)
                {

                    case "pMenu1":
                        uc_controller = new processes();
                        ((processes)uc_controller).OnProcessNextButtonClick += (o, ea) => {
                            Menu_Clicks_Handler(pMenu2, new EventArgs());
                        };
                        break;

                    case "pMenu2":
                        if (_ct_save_current_state.Tag.ToString() == "first")
                        {
                            _ct_save_current_state.Tag = "!";
                            op_container.Controls.Add(_ct_save_current_state);
                        }
                        _ct_save_current_state.Visible = true;
                        break;

                    case "pMenu3":
                        uc_controller = new bots();
                        break;



                    default:
                        break;

                }

                if (_ct_save_current_state.Visible == false)
                {
                    op_container.Controls.Add(uc_controller);
                    uc_controller.Dock = DockStyle.Fill;
                }

                sets.clickedMenuItemTag = clicked.Tag.ToString();
            }


        }


        private void minBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }


        private void closeBtn_Click(object sender, EventArgs e)
        {
            if (MsgBox == null)
            {
                MsgBox = new MyMsgBox();
                bool wasVisible = _ct_save_current_state.Visible;

                foreach (Control c in op_container.Controls)
                {
                    c.Visible = false;
                }

                op_container.Controls.Add(MsgBox);
                MsgBox.Left = (op_container.Width - MsgBox.Width) / 2;
                MsgBox.Top = (op_container.Height - MsgBox.Height) / 2;

                new Thread(new ThreadStart(() => {


                        while (MsgBox.res == 0) ;

                        if (MsgBox.res == 1) Environment.Exit(0);
                        else
                        {
                            MsgBox.Dispose();
                            MsgBox = null;
                            foreach (Control item in op_container.Controls)
                            {
                                if (item != _ct_save_current_state) item.Visible = true;
                                else if (wasVisible) _ct_save_current_state.Visible = true;
                            }
                        }


                })) { IsBackground = true }.Start();
            }
        }
    }
}
