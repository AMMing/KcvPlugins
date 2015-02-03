using AMing.Warning.Data;
using Livet;
using Livet.Commands;
using MetroRadiance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AMing.Warning.ViewModels
{
    public class SettingsViewModel : AMing.Plugins.Core.ViewModels.ViewModelEx
    {
        public SettingsViewModel()
        {
            #region init

            var assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            PluginInfo = string.Format("{0} Version {1}",
                assembly.Name,
                assembly.Version.ToString());

            #endregion
        }

        #region PluginInfo

        private string _pluginInfo;

        public string PluginInfo
        {
            get { return _pluginInfo; }
            set { base.RaisePropertyChanged(ref _pluginInfo, value); }
        }

        #endregion



        #region EnableWarning

        public bool EnableWarning
        {
            get { return Settings.Current.EnableWarning; }
            set
            {
                if (Settings.Current.EnableWarning != value)
                {
                    Settings.Current.EnableWarning = value;
                    base.RaisePropertyChanged();
                    Modules.WarningModules.Current.EnableWarning();
                }
            }
        }

        #endregion

        #region EnableThemeWarning

        public bool EnableThemeWarning
        {
            get { return Settings.Current.EnableThemeWarning; }
            set
            {
                if (Settings.Current.EnableThemeWarning != value)
                {
                    Settings.Current.EnableThemeWarning = value;
                    base.RaisePropertyChanged();
                    Modules.WarningModules.Current.EnableThemeWarning();
                }
            }
        }

        #endregion

        #region EnableWindows

        public bool EnableWindows
        {
            get { return Settings.Current.EnableWindows; }
            set
            {
                if (Settings.Current.EnableWindows != value)
                {
                    Settings.Current.EnableWindows = value;
                    base.RaisePropertyChanged();
                    Modules.WarningModules.Current.EnableWindows();
                }
            }
        }

        #endregion

        #region EnableWarningEx

        public bool EnableWarningEx
        {
            get { return Settings.Current.EnableWarningEx; }
            set
            {
                if (Settings.Current.EnableWarningEx != value)
                {
                    Settings.Current.EnableWarningEx = value;
                    base.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region FilterInRepairing

        public bool FilterInRepairing
        {
            get { return Settings.Current.FilterInRepairing; }
            set
            {
                if (Settings.Current.FilterInRepairing != value)
                {
                    Settings.Current.FilterInRepairing = value;
                    base.RaisePropertyChanged();
                    Modules.WarningModules.Current.Filter();
                }
            }
        }

        #endregion

        #region EnableFleet1

        public bool EnableFleet1
        {
            get { return Settings.Current.EnableFleet1; }
            set
            {
                if (Settings.Current.EnableFleet1 != value)
                {
                    Settings.Current.EnableFleet1 = value;
                    base.RaisePropertyChanged();
                    Modules.WarningModules.Current.Filter();
                }
            }
        }

        #endregion

        #region EnableFleet2

        public bool EnableFleet2
        {
            get { return Settings.Current.EnableFleet2; }
            set
            {
                if (Settings.Current.EnableFleet2 != value)
                {
                    Settings.Current.EnableFleet2 = value;
                    base.RaisePropertyChanged();
                    Modules.WarningModules.Current.Filter();
                }
            }
        }

        #endregion

        #region EnableFleet3

        public bool EnableFleet3
        {
            get { return Settings.Current.EnableFleet3; }
            set
            {
                if (Settings.Current.EnableFleet3 != value)
                {
                    Settings.Current.EnableFleet3 = value;
                    base.RaisePropertyChanged();
                    Modules.WarningModules.Current.Filter();
                }
            }
        }

        #endregion

        #region EnableFleet4

        public bool EnableFleet4
        {
            get { return Settings.Current.EnableFleet4; }
            set
            {
                if (Settings.Current.EnableFleet4 != value)
                {
                    Settings.Current.EnableFleet4 = value;
                    base.RaisePropertyChanged();
                    Modules.WarningModules.Current.Filter();
                }
            }
        }

        #endregion

    }
}
