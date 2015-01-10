using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AMing.SettingsExtensions.Controls
{
    public class WindowViewTypeDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Bottom { get; set; }
        public DataTemplate Top { get; set; }
        public DataTemplate Left { get; set; }
        public DataTemplate Right { get; set; }
        public DataTemplate Split { get; set; }

        public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var type = (Enums.WindowViewType)item;
            switch (type)
            {
                case AMing.SettingsExtensions.Enums.WindowViewType.Bottom:
                    return this.Bottom;
                case AMing.SettingsExtensions.Enums.WindowViewType.Top:
                    return this.Top;
                case AMing.SettingsExtensions.Enums.WindowViewType.Left:
                    return this.Left;
                case AMing.SettingsExtensions.Enums.WindowViewType.Right:
                    return this.Right;
                case AMing.SettingsExtensions.Enums.WindowViewType.Split:
                    return this.Split;
                default:
                    break;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
