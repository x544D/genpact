using System;
using System.Drawing;
using System.Windows.Forms;

namespace GenPact
{
    public class DisabledRichTextBox : RichTextBox
    {

        private const int WM_SETFOCUS = 0x07;
        private const int WM_ENABLE = 0x0A;
        private const int WM_SETCURSOR = 0x20;

        protected override void WndProc(ref Message m)
        {
            if (!(m.Msg == WM_SETFOCUS || m.Msg == WM_ENABLE || m.Msg == WM_SETCURSOR))
                base.WndProc(ref m);
        }
    }


    public class MyPicBox : PictureBox
    {
        public MyPicBox()
        {
            BorderStyle = BorderStyle.None;
            AutoSize = false;
            Controls.Add(new Label()
            { Height = 1, Dock = DockStyle.Bottom, BackColor = Color.Black });
        }
    }


    public class MyControl : Control
    {

        protected override void OnVisibleChanged(EventArgs e)
        {
            Refresh();
            Update();
            base.OnVisibleChanged(e);   
        }


        public virtual void SynchroniseUi()
        {
            this.Refresh();
            this.Update();
        }
    }




}
