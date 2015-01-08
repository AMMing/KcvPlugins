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



        #endregion

        public override void Initialize()
        {
            base.Initialize();
            WindowViewHelper.GetMainWindowControls();
#if DEBUG
            WindowViewHelper.SplitWindow();
#endif
        }


        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
