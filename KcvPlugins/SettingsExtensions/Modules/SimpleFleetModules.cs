using AMing.Plugins.Core.Helper;
using AMing.Plugins.Core.Models;
using AMing.Plugins.Core.Modules;
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
    public class SimpleFleetModules : ModulesBase
    {
        #region Current

        private static SimpleFleetModules _current = new SimpleFleetModules();

        public static SimpleFleetModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        public SimpleFleetModules()
        {
        }
        public override void Initialize()
        {
            base.Initialize();

            if (Data.Settings.Current.EnableSimpleFleet)
            {
                ShowSimpleFleetWindow();
            }

            InitPublicModules();
        }


        public override void Dispose()
        {
            base.Dispose();
        }



        #region method

        //private readonly ViewModels.SimpleFleetViewModel simpleFleetViewModel = new ViewModels.SimpleFleetViewModel();

        public Views.SimpleFleetWindow SimpleFleetWindow { get; set; }

        private void ShowSimpleFleetWindow()
        {
            if (this.SimpleFleetWindow == null)
            {
                this.SimpleFleetWindow = new Views.SimpleFleetWindow { DataContext = Application.Current.MainWindow.DataContext };
                this.SimpleFleetWindow.Closed += (sender, e) =>
                {
                    if (!this.SimpleFleetWindow.IsKcvClose)
                    {
                        Data.Settings.Current.EnableSimpleFleet = false;
                        this.OnEnableSimpleFleetChange();
                        this.SimpleFleetWindow = null;
                    }
                };
            }
            this.SimpleFleetWindow.Show();
        }
        private void CloseSimpleFleetWindow()
        {
            if (this.SimpleFleetWindow != null && this.SimpleFleetWindow.IsInitialized)
            {
                this.SimpleFleetWindow.Close();
                this.SimpleFleetWindow = null;
            }
        }

        public void EnableSimpleFleet(bool isenabel)
        {
            if (Data.Settings.Current.EnableSimpleFleet != isenabel)
            {
                Data.Settings.Current.EnableSimpleFleet = isenabel;
                if (isenabel)
                {
                    ShowSimpleFleetWindow();
                }
                else
                {
                    CloseSimpleFleetWindow();
                }
                OnEnableSimpleFleetChange();
            }
        }

        public event EventHandler<bool> EnableSimpleFleetChange;

        private void OnEnableSimpleFleetChange()
        {
            if (EnableSimpleFleetChange != null)
            {
                EnableSimpleFleetChange(this, Data.Settings.Current.EnableSimpleFleet);
            }
        }

        public void ShowHideSimpleFleet()
        {
            EnableSimpleFleet(!Data.Settings.Current.EnableSimpleFleet);
        }

        private void GhostSimpleFleet()
        {
            if (this.SimpleFleetWindow != null && this.SimpleFleetWindow.IsInitialized)
            {
                this.SimpleFleetWindow.IsGhostMode = !this.SimpleFleetWindow.IsGhostMode;
            }
            else
            {
                MessageBoxDialog.Show(string.Format("{0}{1}", TextResource.NeedToOpen, TextResource.SimpleFleet));
            }
        }


        public void ChangeStyle(Enums.FeetStyleType type)
        {
            if (Data.Settings.Current.SimpleFeetStyleType != type)
            {
                Data.Settings.Current.SimpleFeetStyleType = type;
                OnFeetStyleChange();
            }
        }
        public event EventHandler<Enums.FeetStyleType> FeetStyleChange;

        private void OnFeetStyleChange()
        {
            if (FeetStyleChange != null)
            {
                FeetStyleChange(this, Data.Settings.Current.SimpleFeetStyleType);
            }
        }
        #endregion


        #region PublicModules


        void InitPublicModules()
        {
            var modulesItem_EnableSimpleFleet = new ModulesItem(
                this,
                PublicModulesKeys.EnableSimpleFleet,
                string.Format("{0}/{1}{2}", TextResource.Show, TextResource.Hide, TextResource.SimpleFleet));
            modulesItem_EnableSimpleFleet.Register(ShowHideSimpleFleet);

            PublicModules.Current.Add(modulesItem_EnableSimpleFleet);

            var modulesItem_GhostSimpleFleet = new ModulesItem(
                this,
                PublicModulesKeys.GhostSimpleFleet,
                string.Format("{0}{1}", TextResource.SimpleFleet, TextResource.ChangeGhostMode));
            modulesItem_GhostSimpleFleet.Register(GhostSimpleFleet);

            PublicModules.Current.Add(modulesItem_GhostSimpleFleet);
        }

        #endregion
    }
}
