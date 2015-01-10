using Grabacr07.KanColleViewer.Views.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMing.SettingsExtensions.Extensions
{
    public static class KanColleHostEx
    {
        public static void SetMiniWindow(this KanColleHost kanColleHost, double captionHeight)
        {
            if (kanColleHost.WebBrowser == null) return;

            kanColleHost.Update();
            Window win = Window.GetWindow(kanColleHost);

            win.Width = kanColleHost.WebBrowser.ActualWidth;
            win.MinHeight = kanColleHost.WebBrowser.ActualHeight + captionHeight + 23;
            win.Height = win.MinHeight;
        }
    }
}
