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
using AMing.Plugins.Core.Enums;

namespace AMing.SettingsExtensions.Converters
{
    public class FleetsColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LimitedValue)
            {
                var limitedValue = (LimitedValue)value;

                Color color;
                switch (limitedValue.ShipStatus())
                {
                    case ShipStatus.Normal:
                        color = Color.FromRgb(64, 200, 32);
                        break;
                    case ShipStatus.MinorDamage:
                        color = Color.FromRgb(240, 240, 0);
                        break;
                    case ShipStatus.ModerateDamage:
                        color = Color.FromRgb(240, 128, 32);
                        break;
                    default:
                        color = Color.FromRgb(255, 32, 32);
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
