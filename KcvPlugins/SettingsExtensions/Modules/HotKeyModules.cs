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
using System.Windows.Input;
using kcv = Grabacr07.KanColleViewer;

namespace AMing.SettingsExtensions.Modules
{
    public class HotKeyModules : ModulesBase
    {
        #region Current

        private static HotKeyModules _current = new HotKeyModules();

        public static HotKeyModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        public Helper.HotKeyHelper HotKeyHelper { get; set; }
        public override void Initialize()
        {
            base.Initialize();

            HotKeyHelper = new HotKeyHelper(Application.Current.MainWindow);
            HotKeyHelper.HotKeyDown += HotKeyHelper_HotKeyDown;

            ResetHotKey();
        }

        public void ResetHotKey()
        {
            if (Data.Settings.Current.EnableHotKeyShowHide)
            {
                RegisterHotKey();
            }
            else
            {
                UnregisterHotKey();
            }
        }

        void RegisterHotKey()
        {
            try
            {
                HotKeyHelper.Register(
                    Data.Settings.Current.HotKey_ModifierKeys,
                    Data.Settings.Current.HotKey_Key);
            }
            catch (NotImplementedException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception)
            {
            }
        }
        void UnregisterHotKey()
        {
            try
            {
                HotKeyHelper.Unregister();
            }
            catch (Exception)
            {
            }
        }

        void HotKeyHelper_HotKeyDown(object sender, EventArgs e)
        {
            NotifyIconModules.Current.ShowHideWindow();
        }

        public override void Dispose()
        {
            base.Dispose();
            Helper.HotKeyHelper.UnregisterAll();
        }
    }
}
