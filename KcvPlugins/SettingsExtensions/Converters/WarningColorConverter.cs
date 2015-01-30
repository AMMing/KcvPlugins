using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Grabacr07.KanColleViewer.ViewModels.Contents.Fleets;
using Grabacr07.KanColleWrapper.Models;
using AMing.Plugins.Core.Extensions;

namespace AMing.SettingsExtensions.Converters
{
    public class WarningColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LimitedValue)
            {
                var limitedValue = (LimitedValue)value;

                Color color;

                switch (limitedValue.ShipStatus())
                {
                    case AMing.Plugins.Core.Enums.ShipStatus.SevereDamage:
                        color = Colors.Red;
                        break;
                    default:
                        color = Colors.White;
                        break;
                }

                return new SolidColorBrush(color);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
