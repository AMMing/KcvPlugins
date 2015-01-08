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
    public class ThemeModules : ModulesBase
    {
        #region Current

        private static ThemeModules _current = new ThemeModules();

        public static ThemeModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion
        public Helper.ThemeHelper ThemeHelper { get; set; }

        public ThemeModules()
        {
            ThemeHelper = new ThemeHelper();
        }

        #region method

        public void ChangeTheme()
        {
            if (MetroRadiance.ThemeService.Current.Theme != Data.Settings.Current.WindowTheme_Theme)
            {
                MetroRadiance.ThemeService.Current.ChangeTheme(Data.Settings.Current.WindowTheme_Theme);
            }
        }
        public void ChangeTheme(Theme theme)
        {
            Data.Settings.Current.WindowTheme_Theme = theme;
            ChangeTheme();
        }

        public void ChangeAccent()
        {
            if (MetroRadiance.ThemeService.Current.Accent != Data.Settings.Current.WindowTheme_Accent)
            {
                MetroRadiance.ThemeService.Current.ChangeAccent(Data.Settings.Current.WindowTheme_Accent);
            }
        }
        public void ChangeAccent(Accent accent)
        {
            Data.Settings.Current.WindowTheme_Accent = accent;
            ChangeAccent();
        }


        #endregion

        public override void Initialize()
        {
            base.Initialize();
            ChangeAccent();
            ChangeTheme();
        }


        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
