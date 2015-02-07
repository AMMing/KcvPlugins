using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace AMing.Plugins.Core.Helper
{
    public class ToolWindowHelper
    {
        public static void SetToolWindow(Window win)
        {
            WindowInteropHelper wndHelper = new WindowInteropHelper(win);

            int exStyle = (int)Win32.ToolWindow.GetWindowLong(wndHelper.Handle, (int)Win32.ToolWindow.GetWindowLongFields.GWL_EXSTYLE);

            exStyle |= (int)Win32.ToolWindow.ExtendedWindowStyles.WS_EX_TOOLWINDOW;
            Win32.ToolWindow.SetWindowLong(wndHelper.Handle, (int)Win32.ToolWindow.GetWindowLongFields.GWL_EXSTYLE, (IntPtr)exStyle);
        }
    }
}
