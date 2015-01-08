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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AMing.SettingsExtensions.Views
{
    /// <summary>
    /// ShowMainInfoViewButton.xaml 的交互逻辑
    /// </summary>
    public partial class ShowMainInfoViewButton : UserControl
    {
        public ShowMainInfoViewButton()
        {
            InitializeComponent();
            this.Loaded += ShowMainInfoViewButton_Loaded;
        }

        void ShowMainInfoViewButton_Loaded(object sender, RoutedEventArgs e)
        {
            this.btn.Click += (btn_sender, btn_e) =>
            {
                if (Click != null)
                    Click(btn_sender, btn_e);
            };
        }

        public event RoutedEventHandler Click;

        public bool BtnIsEnabled
        {
            get { return this.btn.IsEnabled; }
            set
            {
                if (this.btn.IsEnabled != value)
                {
                    this.btn.IsEnabled = value;
                    this.btn.Opacity = value ? 1 : 0.6d;
                }
            }
        }

    }
}
