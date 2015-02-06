using AMing.Plugins.Core.Extensions;
using AMing.SettingsExtensions.Data;
using AMing.SettingsExtensions.Extensions;
using AMing.SettingsExtensions.ViewModels.Collections;
using AMing.SettingsExtensions.ViewModels.Items;
using Livet;
using Livet.Commands;
using MetroRadiance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AMing.SettingsExtensions.ViewModels
{
    public class SettingsViewModel : ViewModel
    {
        public SettingsViewModel()
        {
            #region init

            var assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            PluginInfo = string.Format("{0} Version {1}",
                assembly.Name,
                assembly.Version.ToString());

            WindowThemeList.GetListFunc = () =>
            {
                var list = new List<ThemeItemViewModel<Theme, Models.ThemeItem<Theme>>>();
                foreach (var item in Modules.ThemeModules.Current.ThemeHelper.ThemeList)
                {
                    var vm = new ThemeItemViewModel<Theme, Models.ThemeItem<Theme>>(item.Value);
                    list.Add(vm);

                    if (vm.Type == Data.Settings.Current.WindowTheme_Theme)
                    {
                        WindowThemeList.SelectedItem = vm;
                    }
                }

                return list;
            };

            WindowAccentList.GetListFunc = () =>
            {
                var list = new List<ThemeItemViewModel<Accent, Models.ThemeItem<Accent>>>();
                foreach (var item in Modules.ThemeModules.Current.ThemeHelper.AccentList)
                {
                    var vm = new ThemeItemViewModel<Accent, Models.ThemeItem<Accent>>(item.Value);
                    list.Add(vm);

                    if (vm.Type == Data.Settings.Current.WindowTheme_Accent)
                    {
                        WindowAccentList.SelectedItem = vm;
                    }
                }

                return list;
            };
            WindowViewTypeList.GetListFunc = () =>
            {
                var list = new List<WindowViewTypeViewModel>();
                EnumEx.ForEach<Enums.WindowViewType>(item =>
                {
                    var vm = new WindowViewTypeViewModel(item);
                    list.Add(vm);

                    if (item == Data.Settings.Current.WindowViewType)
                    {
                        WindowViewTypeList.SelectedItem = vm;
                    }
                });
                return list;
            };


            FeetStyleTypeList.GetListFunc = () =>
            {
                var list = new List<FeetStyleTypeViewModel>();
                EnumEx.ForEach<Enums.FeetStyleType>(item =>
                {
                    var vm = new FeetStyleTypeViewModel(item);
                    list.Add(vm);

                    if (item == Data.Settings.Current.SimpleFeetStyleType)
                    {
                        FeetStyleTypeList.SelectedItem = vm;
                    }
                });
                return list;
            };

            WindowThemeList.SelectedChange += (sender, e) =>
            {
                Modules.ThemeModules.Current.ChangeTheme(e.Type);
            };
            WindowAccentList.SelectedChange += (sender, e) =>
            {
                Modules.ThemeModules.Current.ChangeAccent(e.Type);
            };
            WindowViewTypeList.SelectedChange += (sender, e) =>
            {
                Modules.WindowViewModules.Current.Change(e.Type);
            };
            FeetStyleTypeList.SelectedChange += (sender, e) =>
            {
                Modules.SimpleFleetModules.Current.ChangeStyle(e.Type);
            };


            Modules.SimpleFleetModules.Current.EnableSimpleFleetChange += (sender, e) => this.RaisePropertyChanged("EnableSimpleFleet");
            #endregion
        }

        #region PluginInfo

        private string _pluginInfo;

        public string PluginInfo
        {
            get { return _pluginInfo; }
            set
            {
                if (_pluginInfo != value)
                {
                    _pluginInfo = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region EnableExitTip

        public bool EnableExitTip
        {
            get { return Settings.Current.EnableExitTip; }
            set
            {
                if (Settings.Current.EnableExitTip != value)
                {
                    Settings.Current.EnableExitTip = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region EnableNotifyIcon

        public bool EnableNotifyIcon
        {
            get { return Settings.Current.EnableNotifyIcon; }
            set
            {
                if (Settings.Current.EnableNotifyIcon != value)
                {
                    Settings.Current.EnableNotifyIcon = value;
                    Modules.NotifyIconModules.Current.ResetNotifyIconVisible();
                    if (!Settings.Current.EnableNotifyIcon)
                    {
                        EnableWindowMiniHideTaskbar = false;
                    }
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region EnableWindowMiniHideTaskbar

        public bool EnableWindowMiniHideTaskbar
        {
            get { return Settings.Current.EnableWindowMiniHideTaskbar; }
            set
            {
                if (Settings.Current.EnableWindowMiniHideTaskbar != value)
                {
                    Settings.Current.EnableWindowMiniHideTaskbar = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region WindowThemeList

        private ThemeListViewModels<Theme> _windowThemeList = new ThemeListViewModels<Theme>();

        public ThemeListViewModels<Theme> WindowThemeList
        {
            get
            {
                return _windowThemeList;
            }
            set
            {
                if (_windowThemeList != value)
                {
                    _windowThemeList = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region WindowAccentList

        private ThemeListViewModels<Accent> _windowAccentList = new ThemeListViewModels<Accent>();

        public ThemeListViewModels<Accent> WindowAccentList
        {
            get
            {
                return _windowAccentList;
            }
            set
            {
                if (_windowAccentList != value)
                {
                    _windowAccentList = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region WindowViewTypeList

        private ListViewModels<WindowViewTypeViewModel> _WindowViewTypeList = new ListViewModels<WindowViewTypeViewModel>();

        public ListViewModels<WindowViewTypeViewModel> WindowViewTypeList
        {
            get { return _WindowViewTypeList; }
            set
            {
                if (_WindowViewTypeList != value)
                {
                    _WindowViewTypeList = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region EnableSimpleFleet

        public bool EnableSimpleFleet
        {
            get { return Settings.Current.EnableSimpleFleet; }
            set
            {
                if (Settings.Current.EnableSimpleFleet != value)
                {
                    Modules.SimpleFleetModules.Current.EnableSimpleFleet(value);
                }
            }
        }

        #endregion

        #region FeetStyleTypeList

        private ListViewModels<FeetStyleTypeViewModel> _feetStyleTypeList = new ListViewModels<FeetStyleTypeViewModel>();

        public ListViewModels<FeetStyleTypeViewModel> FeetStyleTypeList
        {
            get
            {
                return _feetStyleTypeList;
            }
            set
            {
                if (_feetStyleTypeList != value)
                {
                    _feetStyleTypeList = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region SimpleFeetStyleWindowOpacity

        public int SimpleFeetStyleWindowOpacity
        {
            get { return Settings.Current.SimpleFeetStyleWindowOpacity; }
            set
            {
                if (Settings.Current.SimpleFeetStyleWindowOpacity != value)
                {
                    Settings.Current.SimpleFeetStyleWindowOpacity = value;
                    this.RaisePropertyChanged();
                    Modules.SimpleFleetModules.Current.ChangeWindowOpacity();
                }
            }
        }

        #endregion
        #region SimpleFeetStyleWindowOpacity

        public bool GhostEnableOpacity
        {
            get { return Settings.Current.GhostEnableOpacity; }
            set
            {
                if (Settings.Current.GhostEnableOpacity != value)
                {
                    Settings.Current.GhostEnableOpacity = value;
                    this.RaisePropertyChanged();
                    Modules.SimpleFleetModules.Current.ChangeWindowOpacity();
                }
            }
        }

        #endregion
    }
}
