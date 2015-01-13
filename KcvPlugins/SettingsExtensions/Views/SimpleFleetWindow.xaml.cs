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
            this.AllowsTransparency = true;
            this.WindowStyle = System.Windows.WindowStyle.None;
            //this.Opacity = 0.6;

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

            Modules.MessagerModules.Current.Register(this, Entrance.MessagerKey + "FirstFleetInit", ReserSize);
        }

        void SimpleFleetWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.ItemContentTransform = new ScaleTransform(dpi.ScaleX, dpi.ScaleY);

            ReserSize();
        }

        void ReserSize()
        {
            this.Width = 175 * dpi.ScaleX;
            this.Height = 225 * dpi.ScaleY;
        }

        #region member

        public bool IsKcvClose { get; set; }
        Dpi dpi = Dpi.Default;

        public Transform ItemContentTransform
        {
            get { return (Transform)GetValue(ItemContentTransformProperty); }
            set { SetValue(ItemContentTransformProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemContentTransform.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemContentTransformProperty =
            DependencyProperty.Register("ItemContentTransform", typeof(Transform), typeof(SimpleFleetWindow), new UIPropertyMetadata(Transform.Identity));


        #region Ghost

        public bool IsGhostMode
        {
            get { return (bool)GetValue(IsGhostModeProperty); }
            set { SetValue(IsGhostModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsGhostMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsGhostModeProperty =
            DependencyProperty.Register("IsGhostMode", typeof(bool), typeof(SimpleFleetWindow), new PropertyMetadata(false, OnIsGhostModeChanged));
        private static void OnIsGhostModeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                var win = obj as SimpleFleetWindow;
                if (win != null)
                {
                    if (win.IsGhostMode)
                    {
                        Helper.PenetrateHelper.SetPenetrate(win);
                        win.Opacity = 0.6;
                    }
                    else
                    {
                        Helper.PenetrateHelper.CancelPenetrate(win);
                        win.Opacity = 1;
                    }
                }
            }
        }

        #endregion


        #endregion

    }
}
