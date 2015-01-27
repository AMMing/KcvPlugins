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
        public DataTemplate Arc { get; set; }
        public DataTemplate Arc_Mini { get; set; }
        public DataTemplate Rectangle { get; set; }

        public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var type = (Enums.FeetStyleType)item;
            switch (type)
            {
                case AMing.SettingsExtensions.Enums.FeetStyleType.Arc:
                    return Arc;
                case AMing.SettingsExtensions.Enums.FeetStyleType.Arc_Mini:
                    return Arc_Mini;
                case AMing.SettingsExtensions.Enums.FeetStyleType.Rectangle:
                    return Rectangle;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
