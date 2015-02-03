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

namespace AMing.SettingsExtensions.Views.Simple
{
    /// <summary>
    /// FleetControl.xaml 的交互逻辑
    /// </summary>
    public partial class FleetControl : UserControl
    {
        public FleetControl()
        {
            InitializeComponent();
            this.Loaded += FleetControl_Loaded;
        }

        void FleetControl_Loaded(object sender, RoutedEventArgs e)
        {
            Modules.SimpleFleetModules.Current.FeetStyleChange += Current_FeetStyleChange;
            if (Data.Settings.Current.SimpleFeetStyleType != Enums.FeetStyleType.Arc_1)
            {
                ChangeStyle(Data.Settings.Current.SimpleFeetStyleType);
            }
        }

        void Current_FeetStyleChange(object sender, Enums.FeetStyleType e)
        {
            ChangeStyle(e);
        }

        public void ChangeStyle(Enums.FeetStyleType type)
        {
            var key = string.Format("FleetItem_{0}", type);
            var dataTemplate = this.Resources[key] as DataTemplate;
            if (dataTemplate != null)
            {
                itemsControl.ItemTemplate = dataTemplate;
            }
        }
    }
}
