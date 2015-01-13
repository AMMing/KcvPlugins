using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace AMing.SettingsExtensions.Helper
{
    public class PenetrateHelper
    {
        /// <summary>
        /// 设置穿透
        /// </summary>
        /// <param name="win"></param>
        /// <returns></returns>
        public static bool SetPenetrate(Window win)
        {
            var handle = new WindowInteropHelper(win).Handle;
            var winlong = Win32.Window.GetWindowLong(handle, Win32.Window.GWL_EXSTYLE);
            return SetWindowLong(handle, winlong | Win32.Window.WS_EX_TRANSPARENT | Win32.Window.WS_EX_LAYERED);
        }

        /// <summary>
        /// 取消穿透
        /// </summary>
        /// <param name="win"></param>
        /// <returns></returns>
        public static bool CancelPenetrate(Window win)
        {
            var handle = new WindowInteropHelper(win).Handle;
            return SetWindowLong(handle, 0);
        }

        private static bool SetWindowLong(IntPtr handle, int val)
        {
            var result = Win32.Window.SetWindowLong(handle, Win32.Window.GWL_EXSTYLE, val);

            return result == val;
        }
    }
}
