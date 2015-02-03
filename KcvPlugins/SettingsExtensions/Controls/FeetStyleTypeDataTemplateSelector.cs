using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AMing.SettingsExtensions.Controls
{
    public class FeetStyleTypeDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Arc_1 { get; set; }
        public DataTemplate Arc_2 { get; set; }
        public DataTemplate Rectangle { get; set; }

        public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var type = (Enums.FeetStyleType)item;
            switch (type)
            {
                case AMing.SettingsExtensions.Enums.FeetStyleType.Arc_1:
                    return Arc_1;
                case AMing.SettingsExtensions.Enums.FeetStyleType.Arc_2:
                    return Arc_2;
                case AMing.SettingsExtensions.Enums.FeetStyleType.Rectangle:
                    return Rectangle;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
