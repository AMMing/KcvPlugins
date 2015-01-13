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
        public Data.KeySetting Setting { get; set; }


        public override void Initialize()
        {
            base.Initialize();

            Data.KeySetting.Load();
            Setting = Data.KeySetting.Current;

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

        public void Init()
        {
            foreach (var item in Setting.KeySettingList)
            {
                switch (item.Type)
                {
                    case AMing.SettingsExtensions.Enums.KeyType.Normal:
                        InitNormalkey(item);
                        break;
                    case AMing.SettingsExtensions.Enums.KeyType.HotKey:
                        InitHotkey(item);
                        break;
                    default:
                        break;
                }
            }
        }
        private void InitNormalkey(Models.KeySetting key)
        {

        }
        private void InitHotkey(Models.KeySetting key)
        {

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
            Modules.Generic.MessagerHelper.Current.Send(Entrance.MessagerKey + "ShowHideWindow");
        }

        public override void Dispose()
        {
            base.Dispose();
            Data.KeySetting.Current.Save();
            Helper.HotKeyHelper.UnregisterAll();
        }
    }
}
