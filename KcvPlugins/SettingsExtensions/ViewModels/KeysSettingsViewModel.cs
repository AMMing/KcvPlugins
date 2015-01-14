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
using System.Windows;
using System.Windows.Input;

namespace AMing.SettingsExtensions.ViewModels
{
    public class KeysSettingsViewModel : ViewModel
    {
        public KeysSettingsViewModel()
        {
            #region init

            ModulesList.GetListFunc = GetModulesList;

            Modules.KeysModules.Current.KeysPublicModulesChange += (sender, e) =>
            {
                this.ModulesList.List = GetModulesList();
                this.RaisePropertyChanged("ModulesList");
            };
            #endregion
        }

        #region ModulesList

        private ListViewModels<KeyModulesItemViewModel> _ModulesList = new ListViewModels<KeyModulesItemViewModel>();

        public ListViewModels<KeyModulesItemViewModel> ModulesList
        {
            get { return _ModulesList; }
            set
            {
                if (_ModulesList != value)
                {
                    _ModulesList = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        private List<KeyModulesItemViewModel> GetModulesList()
        {
            var list = new List<KeyModulesItemViewModel>();
            foreach (var item in Modules.KeysModules.Current.CurrentPublicModules)
            {
                var keyModulesItem = Modules.KeysModules.Current.GetKeyModulesItem(item);
                var vmKeyModulesItem = new KeyModulesItemViewModel(keyModulesItem);
                vmKeyModulesItem.SetKeyAction = SetKey;
                list.Add(vmKeyModulesItem);
            }

            return list;
        }

        #endregion

        #region ShowKeySettingGrid

        private bool _ShowKeySettingGrid = false;

        public bool ShowKeySettingGrid
        {
            get { return _ShowKeySettingGrid; }
            set
            {
                if (_ShowKeySettingGrid != value)
                {
                    _ShowKeySettingGrid = value;
                    this.RaisePropertyChanged();
                }
            }
        }


        #endregion

        #region KeyTypeList

        private List<Enums.KeyType> _KeyTypeList;

        public List<Enums.KeyType> KeyTypeList
        {
            get
            {
                if (_KeyTypeList == null)
                {
                    _KeyTypeList = new List<Enums.KeyType>();
                    EnumEx.ForEach<Enums.KeyType>(item => _KeyTypeList.Add(item));
                }
                return _KeyTypeList;
            }
            set
            {
                if (_KeyTypeList != value)
                {
                    _KeyTypeList = value;
                    this.RaisePropertyChanged();
                }
            }
        }


        #endregion

        private Enums.KeyType _SelectKeyType;

        public Enums.KeyType SelectKeyType
        {
            get { return _SelectKeyType; }
            set
            {
                if (_SelectKeyType != value)
                {
                    _SelectKeyType = value;
                    this.RaisePropertyChanged();
                }
            }
        }


        #region HotKey_KeyText

        private string _Current_KeyText;

        public string Current_KeyText
        {
            get
            {
                return _Current_KeyText;
            }
            set
            {
                if (_Current_KeyText != value)
                {
                    _Current_KeyText = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region HotKey event

        ModifierKeys temp_ModifierKeys = ModifierKeys.None;
        Key temp_Key = Key.None;
        public void KeyTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
            {
                switch (e.Key)
                {
                    case Key.None:
                    case Key.System:
                    case Key.LeftAlt:
                    case Key.LeftCtrl:
                    case Key.LeftShift:
                    case Key.LWin:
                    case Key.RightAlt:
                    case Key.RightCtrl:
                    case Key.RightShift:
                    case Key.RWin:
                        return;
                }
            }
            temp_ModifierKeys = e.KeyboardDevice.Modifiers;
            temp_Key = e.Key;

            UpdateHotKey_KeyText();
        }
        public void KeysConfirm()
        {
            try
            {
                Modules.KeysModules.Current.SetKey(new Models.KeySetting
                {
                    ModulesKey = ModulesList.SelectedItem.KeyModulesItem.ModulesItem.ModulesKey,
                    ModifierKeys = temp_ModifierKeys,
                    Key = temp_Key,
                    Type = SelectKeyType,
                    IsNotSetKey = temp_ModifierKeys == ModifierKeys.None && temp_Key == Key.None
                });
                GoBack();
            }
            catch (NotImplementedException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception)
            {
            }
        }
        public void KeysReset()
        {
            temp_ModifierKeys = ModulesList.SelectedItem.KeyModulesItem.KeySetting.ModifierKeys;
            temp_Key = ModulesList.SelectedItem.KeyModulesItem.KeySetting.Key;

            UpdateHotKey_KeyText();
        }
        public void KeysDisable()
        {
            temp_ModifierKeys = ModifierKeys.None;
            temp_Key = Key.None;

            KeysConfirm();
        }

        private void UpdateHotKey_KeyText()
        {
            Current_KeyText = Helper.KeysHelper.ToName(temp_ModifierKeys, temp_Key);
        }

        public void SetKey(KeyModulesItemViewModel viewModel)
        {
            ModulesList.SelectedItem = viewModel;
            ShowKeySettingGrid = true;

            SelectKeyType = viewModel.KeyModulesItem.KeySetting.Type;
            temp_Key = viewModel.KeyModulesItem.KeySetting.Key;
            temp_ModifierKeys = viewModel.KeyModulesItem.KeySetting.ModifierKeys;

            UpdateHotKey_KeyText();
        }
        public void GoBack()
        {
            ShowKeySettingGrid = false;
        }

        #endregion
    }
}
