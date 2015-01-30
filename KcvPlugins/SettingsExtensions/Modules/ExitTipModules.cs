using AMing.Plugins.Core.Helper;
using AMing.Plugins.Core.Modules;
using AMing.SettingsExtensions.Helper;
using Grabacr07.Desktop.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using kcv = Grabacr07.KanColleViewer;

namespace AMing.SettingsExtensions.Modules
{
    public class ExitTipModules : ModulesBase
    {
        #region Current

        private static ExitTipModules _current = new ExitTipModules();

        public static ExitTipModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        public override void Initialize()
        {
            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);
        }
        public override void Dispose()
        {
        }

        void MainWindow_Closing(object o, CancelEventArgs e)
        {
            if (!Data.Settings.Current.EnableExitTip)
            {
                return;
            }

            if (!MessageBoxDialog.Show(TextResource.Exit_Msg_Content, TextResource.Exit_Msg_Title))
            {
                e.Cancel = true;
            }

        }

    }
}
