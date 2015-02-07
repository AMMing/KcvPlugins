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
            AMing.Plugins.Core.Helper.ToolWindowHelper.SetToolWindow(this);

            Dpi dpi = this.GetSystemDpi() ?? Dpi.Default;
            this.Top = forms.Screen.PrimaryScreen.WorkingArea.Height / dpi.ScaleY - this.Height;
            this.Left = forms.Screen.PrimaryScreen.WorkingArea.Width / dpi.ScaleX - this.Width;


            //AMing.Plugins.Core.Helper.PenetrateHelper.SetPenetrate(this);
            //this.Opacity = 0.8;
        }
        #region method

        private StatusItemControl AddShip(Ship ship)
        {
            var statusItemControl = new StatusItemControl(ship);
            sp_status.Children.Add(statusItemControl);

            return statusItemControl;
        }
        private void UpdateShip(StatusItemControl itemControl, Ship ship)
        {
            //itemControl.Fleet = fleet;
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

        public void UpdateFleet(List<Ship> ships)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                List<Ship> showlist = new List<Ship>(ships);
                sp_status.Children.OfType<StatusItemControl>().ToList().ForEach(item =>
                {
                    var ship = showlist.FirstOrDefault(s => s.Id == item.Ship.Id);
                    if (ship != null)//存在就更新控件里面的信息，然后从队列中移除
                    {
                        UpdateShip(item, ship);
                        showlist.Remove(ship);
                    }
                    else
                    {
                        item.Remove();//如果已经不存在舰队里面就移除掉控件
                    }
                });
                showlist.ForEach(ship => AddShip(ship));//添加剩余的船 

            }));
        }
        #endregion



    }
}
