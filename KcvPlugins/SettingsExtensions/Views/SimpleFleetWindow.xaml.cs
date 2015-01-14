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
        }



        #region member

        public bool IsKcvClose { get; set; }

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
