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
                WindowShowHideForTaskBar(true);
                this.OldwinState = this.CurrentWindow.WindowState;
            }
            else
            {
                WindowShowHideForTaskBar(false);
            }
        }
        /// <summary>
        /// 显示隐藏窗体
        /// </summary>
        public void ShowHideWindow()
        {
            if (!IsInit) return;
            if (this.CurrentWindow.WindowState == WindowState.Minimized)
            {
                WindowShowHideForTaskBar(true);
                this.CurrentWindow.WindowState = this.OldwinState;
                this.CurrentWindow.Focus();
            }
            else
            {
                this.CurrentWindow.WindowState = WindowState.Minimized;
            }
        }
        void WindowShowHideForTaskBar(bool isshow)
        {
            this.CurrentWindow.ShowInTaskbar = !(Data.Settings.Current.EnableWindowMiniHideTaskbar && !isshow);
        }

    }
}
