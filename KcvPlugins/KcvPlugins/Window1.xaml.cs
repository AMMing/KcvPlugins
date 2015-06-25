using Grabacr07.KanColleWrapper.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AMing.Plugins.Base.Extensions;
using AMing.Plugins.Base.Model;

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
            //AMing.SettingsExtensions.Converters.ArcLimitedValueConverter ArcLimitedValueConverter = new AMing.SettingsExtensions.Converters.ArcLimitedValueConverter();
            //var val = ArcLimitedValueConverter.Convert(new LimitedValue(62, 120, 0), null, 87, null);
            dynamic args = new System.Dynamic.ExpandoObject();
            args.aaa = 111;
            args.bbb = 222;
            args.ccc = 333;

            dynamic t1 = new DynamicArgs<int, string>(123, "asdad");

            var result = AMing.Plugins.Base.Hub.MethodHub.Current.ExecuteMethod("test.key", t1);


            if (AMing.Plugins.Base.Model.DynamicArgs<int, string, string>.Validation(result))
            {
                MessageBox.Show(result.val2);
            }


            //var result = AMing.Plugins.Base.Hub.MethodHub.Current.ExecuteMethod("test.key", args);
            //var aaa = (TestModel2)result ;
            //var temp = ((object)result).CopyTo<TestModel2>();
            //var obj = result.data;
            //var m = result.data as TestModel2;
        }
    }
}