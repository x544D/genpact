using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GenGine
{
    static class winapi
    {

        /// <summary>
        ///             ScrollingStuff  
        /// </summary>


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowScrollBar(IntPtr hWnd, int wBar, [MarshalAs(UnmanagedType.Bool)] bool bShow);

        [Flags]
        enum SaadScrollBar : int
        {
            /*
                SB_BOTH
                Shows or hides a window's standard horizontal and vertical scroll bars.
                SB_CTL
                Shows or hides a scroll bar control. The hwnd parameter must be the handle to the scroll bar control.
                SB_HORZ
                Shows or hides a window's standard horizontal scroll bars.
                SB_VERT
                Shows or hides a window's standard vertical scroll bar.
            */

            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }






    }
}
