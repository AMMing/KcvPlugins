using Grabacr07.KanColleWrapper.Models;
using MetroRadiance.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AMing.Plugins.Core.Extensions;
using forms = System.Windows.Forms;

namespace AMing.Warning.Views
{
    /// <summary>
    /// ToastWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StatusWindow : Window
    {
        public StatusWindow()
        {
            InitializeComponent();
            this.Loaded += StatusWindow_Loaded;
            Application.Current.MainWindow.Closing += (sender, e) => this.Close();
            Application.Current.MainWindow.StateChanged += MainWindow_StateChanged;
        }

        void MainWindow_StateChanged(object sender, EventArgs e)
        {
            this.WindowState = Application.Current.MainWindow.WindowState;
        }

        void StatusWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Dpi dpi = this.GetSystemDpi() ?? Dpi.Default;
            this.Top = forms.Screen.PrimaryScreen.WorkingArea.Height / dpi.ScaleY - this.Height;
            this.Left = forms.Screen.PrimaryScreen.WorkingArea.Width / dpi.ScaleX - this.Width;


            //AMing.Plugins.Core.Helper.PenetrateHelper.SetPenetrate(this);
            //this.Opacity = 0.8;
        }
        #region method

        private StatusItemControl AddShip(Fleet fleet, Ship ship)
        {
            var statusItemControl = new StatusItemControl(fleet, ship);
            sp_status.Children.Add(statusItemControl);

            return statusItemControl;
        }
        private void UpdateShip(StatusItemControl itemControl, Fleet fleet, Ship ship)
        {
            itemControl.Fleet = fleet;
            itemControl.Ship = ship;
        }
        private void RemoveShip(Ship ship)
        {
            var itemControl = sp_status.Children.OfType<StatusItemControl>().FirstOrDefault(item => item.Ship.Id == ship.Id);
            if (null != itemControl)
            {
                itemControl.Remove();
            }
        }
        private void ClearShip()
        {
            sp_status.Children.OfType<StatusItemControl>().ToList().ForEach(item => item.Remove());
        }

        public void UpdateFleet(Fleet fleet)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                var showlist = fleet.Ships.Where(s =>
                    //s.HP.ShipStatus() == Plugins.Core.Enums.ShipStatus.ModerateDamage ||
                    s.HP.ShipStatus() == Plugins.Core.Enums.ShipStatus.SevereDamage).ToList();//条件

                sp_status.Children.OfType<StatusItemControl>().Where(item => item.Fleet.Id == fleet.Id).ToList().ForEach(item =>
                {
                    var ship = showlist.FirstOrDefault(s => s.Id == item.Ship.Id);
                    if (ship != null)//存在就更新控件里面的信息，然后从队列中移除
                    {
                        UpdateShip(item, fleet, ship);
                        showlist.Remove(ship);
                    }
                    else
                    {
                        item.Remove();//如果已经不存在舰队里面就移除掉控件
                    }
                });
                showlist.ForEach(ship => AddShip(fleet, ship).RemoveMeComplete += (sender, e) => ThemeServiceEx.Current.IsWarning = sp_status.Children.Count > 0);//添加剩余的船 

                ThemeServiceEx.Current.IsWarning = sp_status.Children.Count > 0;
            }));
        }
        #endregion


    }
}
