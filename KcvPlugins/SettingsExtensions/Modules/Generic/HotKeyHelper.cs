using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMing.SettingsExtensions.Modules.Generic
{
    public class HotKeyHelper
    {
        #region Current

        private static HotKeyHelper _current = new HotKeyHelper();

        public static HotKeyHelper Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        #region member

        #endregion

        #region method

        public void Set(Models.KeyModulesItem item)
        {
            if (item.ModulesIsInvalid) return;

            if (item.HotKeyHelper == null)
            {
                item.HotKeyHelper = new Helper.HotKeyHelper(Application.Current.MainWindow);
                item.HotKeyHelper.HotKeyDown += (sender, e) => Modules.MessagerModules.Current.Send(item.ModulesItem.MessageKey);
            }
            if (item.KeySetting.IsNotSetKey)
            {
                UnregisterHotKey(item);
            }
            else
            {
                RegisterHotKey(item);
            }
        }


        void RegisterHotKey(Models.KeyModulesItem item)
        {
            if (item.KeySetting.IsNotSetKey) return;
            try
            {
                item.HotKeyHelper.Register(
                    item.KeySetting.ModifierKeys,
                    item.KeySetting.Key);
            }
            catch (NotImplementedException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception)
            {
            }
        }
        void UnregisterHotKey(Models.KeyModulesItem item)
        {
            try
            {
                item.HotKeyHelper.Unregister();
            }
            catch (Exception)
            {
            }
        }

        #endregion

        public HotKeyHelper()
        {
        }

        ~HotKeyHelper()
        {
            Helper.HotKeyHelper.UnregisterAll();
        }
    }
}
