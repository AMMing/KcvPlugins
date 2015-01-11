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

        #region method

        private readonly ViewModels.SimpleFleetViewModel simpleFleetViewModel = new ViewModels.SimpleFleetViewModel();

        public Views.SimpleFleetWindow SimpleFleetWindow { get; set; }

        private void ShowSimpleFleetWindow()
        {
            if (this.SimpleFleetWindow == null)
            {
                this.SimpleFleetWindow = new Views.SimpleFleetWindow { DataContext = simpleFleetViewModel };
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
            if (this.SimpleFleetWindow != null)
            {
                this.SimpleFleetWindow.Close();
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


        #endregion


        public override void Initialize()
        {
            base.Initialize();

            if (Data.Settings.Current.EnableSimpleFleet)
            {
                ShowSimpleFleetWindow();
            }
        }


        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
