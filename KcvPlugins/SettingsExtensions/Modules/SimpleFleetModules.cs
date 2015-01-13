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
            Application.Current.MainWindow.KeyDown += MainWindow_KeyDown;

            InitPublicModules();
        }


        public override void Dispose()
        {
            base.Dispose();
        }

        #region event

        void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.M && e.KeyboardDevice.Modifiers == System.Windows.Input.ModifierKeys.Control)
            {
                ShowHideSimpleFleet();
            }
        }

        #endregion


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

        #endregion


        #region PublicModules

        Models.ModulesItem PublicModulesItem_EnableSimpleFleet;

        void InitPublicModules()
        {
            PublicModulesItem_EnableSimpleFleet = new Models.ModulesItem(
                this,
                "EnableSimpleFleet",
                string.Format("{0}/{1}{2}", TextResource.Show, TextResource.Hide, TextResource.SimpleFleet));
            PublicModulesItem_EnableSimpleFleet.Register(ShowHideSimpleFleet);

            Modules.Generic.PublicModules.Current.Add(PublicModulesItem_EnableSimpleFleet);
        }

        #endregion
    }
}
