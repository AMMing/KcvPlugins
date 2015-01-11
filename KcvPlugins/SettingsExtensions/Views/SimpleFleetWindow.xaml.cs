using Grabacr07.KanColleViewer.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AMing.SettingsExtensions.Views
{
    /// <summary>
    /// ContainerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SimpleFleetWindow
    {
        public SimpleFleetWindow()
        {
            InitializeComponent();

            this.DpiScaleTransform = null;

            Application.Current.MainWindow.Closed += (sender, args) =>
            {
                this.IsKcvClose = true;
                this.Close();
            };
            Application.Current.MainWindow.StateChanged += (sender, args) =>
            {
                switch (Application.Current.MainWindow.WindowState)
                {
                    case WindowState.Maximized:
                    case WindowState.Normal:
                        this.WindowState = WindowState.Normal;
                        break;
                    case WindowState.Minimized:
                        this.WindowState = WindowState.Minimized;
                        break;
                    default:
                        break;
                }
            };
            this.Loaded += SimpleFleetWindow_Loaded;
            this.Closing += (sender, args) =>
            {
                //Helper.MessagerHelper.Current.Unregister(this, Entrance.MessagerKey + "ShowHideWindow");
            };

            var WindowStateHelper = new Helper.WindowStateHelper();
            WindowStateHelper.Init(this);
            //Helper.MessagerHelper.Current.Register(this, Entrance.MessagerKey + "ShowHideWindow", WindowStateHelper.ShowHideWindow);
        }

        void SimpleFleetWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Width = this.MinWidth;
            this.Height = this.MinHeight;
        }

        public bool IsKcvClose { get; set; }

        //list_ScaleTransform
    }
}
