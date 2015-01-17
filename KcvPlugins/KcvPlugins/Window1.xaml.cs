using Grabacr07.KanColleWrapper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KcvPlugins
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            this.InitializeComponent();

            // 在此点之下插入创建对象所需的代码。
            AMing.SettingsExtensions.Converters.ArcLimitedValueConverter ArcLimitedValueConverter = new AMing.SettingsExtensions.Converters.ArcLimitedValueConverter();
            var val = ArcLimitedValueConverter.Convert(new LimitedValue(62, 120, 0), null, 87, null);
        }
    }
}