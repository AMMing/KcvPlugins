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
using MetroRadiance.Core;

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

            dpi = Dpi.Default;
            dpi = new Dpi((uint)((double)dpi.X * 1.5d), (uint)((double)dpi.Y * 1.5d));

            var WindowStateHelper = new Helper.WindowStateHelper();
            WindowStateHelper.Init(this);

            Helper.MessagerHelper.Current.Register(this, Entrance.MessagerKey + "FirstFleetInit", ReserSize);
        }

        void SimpleFleetWindow_Loaded(object sender, RoutedEventArgs e)
        {
            content_ScaleTransform.ScaleX = dpi.ScaleX;
            content_ScaleTransform.ScaleY = dpi.ScaleY;
            ReserSize();
        }

        void ReserSize()
        {
            this.Width = 175 * dpi.ScaleX;
            this.Height = 225 * dpi.ScaleY;
        }

        public bool IsKcvClose { get; set; }
        Dpi dpi = Dpi.Default;
    }
}
