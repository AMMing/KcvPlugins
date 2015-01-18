using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMing.SettingsExtensions.Controls
{
    public class AppendProperty
    {
        public static bool? GetShowInTaskbar(DependencyObject obj)
        {
            return (bool?)obj.GetValue(ShowInTaskbarProperty);
        }

        public static void SetShowInTaskbar(DependencyObject obj, bool? value)
        {
            obj.SetValue(ShowInTaskbarProperty, value);
        }

        // Using a DependencyProperty as the backing store for ShowInTaskbar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowInTaskbarProperty =
            DependencyProperty.RegisterAttached("ShowInTaskbar", typeof(bool?), typeof(AppendProperty), new PropertyMetadata(null));


    }
}
