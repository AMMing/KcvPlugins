using AMing.SettingsExtensions.Helper;
using Grabacr07.Desktop.Metro.Controls;
using MetroRadiance;
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
    public class WindowViewModules : ModulesBase
    {
        #region Current

        private static WindowViewModules _current = new WindowViewModules();

        public static WindowViewModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion
        public Helper.WindowViewHelper WindowViewHelper { get; set; }

        public WindowViewModules()
        {
            WindowViewHelper = new WindowViewHelper();
        }

        #region method

        public void Change(Enums.WindowViewType type)
        {
            System.Threading.Thread.Sleep(200);//太快导致左右切换重复触发
            if (Data.Settings.Current.WindowViewType != type)
            {
                if (Data.Settings.Current.WindowViewType == Enums.WindowViewType.Split)
                {
                    WindowViewHelper.MergeWindow();
                }
                Data.Settings.Current.WindowViewType = type;
                SetWindow();
            }
        }

        private void SetWindow()
        {
            switch (Data.Settings.Current.WindowViewType)
            {
                case AMing.SettingsExtensions.Enums.WindowViewType.Bottom:
                    WindowViewHelper.BottomWindow();
                    break;
                case AMing.SettingsExtensions.Enums.WindowViewType.Top:
                    WindowViewHelper.TopWindow();
                    break;
                case AMing.SettingsExtensions.Enums.WindowViewType.Left:
                    WindowViewHelper.LeftWindow();
                    break;
                case AMing.SettingsExtensions.Enums.WindowViewType.Right:
                    WindowViewHelper.RightWindow();
                    break;
                case AMing.SettingsExtensions.Enums.WindowViewType.Split:
                    WindowViewHelper.SplitWindow();
                    break;
                default:
                    break;
            }
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();
            WindowViewHelper.GetMainWindowControls();
            WindowViewHelper.InitLayout();
            if (Data.Settings.Current.WindowViewType != Enums.WindowViewType.Bottom)
            {
                SetWindow();
            }
        }


        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
