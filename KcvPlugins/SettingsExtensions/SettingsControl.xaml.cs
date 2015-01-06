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

namespace AMing.SettingsExtensions
{
    /// <summary>
    /// SettingsControl.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
            this.Loaded += SettingsControl_Loaded;
        }
        #region Helpers


        #endregion

        void SettingsControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitHelpers();
        }


        private void InitHelpers()
        {
            Helper.ExitTipHelper.Current.Init();
            Helper.NotifyIconHelper.Current.Init();
        }

    }
}
