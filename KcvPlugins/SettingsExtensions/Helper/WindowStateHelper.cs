using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMing.SettingsExtensions.Helper
{
    public class WindowStateHelper
    {
        public Window CurrentWindow { get; set; }
        public WindowState OldwinState { get; set; }

        public bool IsInit { get; set; }

        public void Init(Window win)
        {
            this.CurrentWindow = win;
            this.CurrentWindow.StateChanged += CurrentWindow_StateChanged;
            this.OldwinState = this.CurrentWindow.WindowState;
            IsInit = true;
        }
        void CurrentWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.CurrentWindow.WindowState != WindowState.Minimized)
            {
                WindowShowHideForTaskBar(this.CurrentWindow, true);
                this.OldwinState = this.CurrentWindow.WindowState;
            }
            else
            {
                WindowShowHideForTaskBar(this.CurrentWindow, false);
            }
        }
        /// <summary>
        /// 显示隐藏窗体
        /// </summary>
        public WindowState? ShowHideWindow()
        {
            if (!IsInit) return null;

            var val = WindowState.Normal;
            if (this.CurrentWindow.WindowState == WindowState.Minimized)
            {
                WindowShowHideForTaskBar(this.CurrentWindow, true);
                val = this.OldwinState;
            }
            else
            {
                val = WindowState.Minimized;
            }
            this.CurrentWindow.WindowState = val;

            return val;
        }
        public static void WindowShowHideForTaskBar(Window win, bool isshow)
        {
            var old = Controls.AppendProperty.GetShowInTaskbar(win);
            if (!old.HasValue)
            {
                Controls.AppendProperty.SetShowInTaskbar(win, win.ShowInTaskbar);
                old = win.ShowInTaskbar;
            }
            if (old == true)
                win.ShowInTaskbar = !(Data.Settings.Current.EnableWindowMiniHideTaskbar && !isshow);
        }

    }
}
