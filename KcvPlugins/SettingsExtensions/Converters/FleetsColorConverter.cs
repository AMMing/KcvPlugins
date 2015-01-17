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
using AMing.SettingsExtensions.Extensions;

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
                var percentage = limitedValue.Percentage();

                // 0.25 以下のとき、「大破」
                if (percentage <= 0.25) color = Color.FromRgb(255, 32, 32);

                // 0.5 以下のとき、「中破」
                else if (percentage <= 0.5) color = Color.FromRgb(240, 128, 32);

                // 0.75 以下のとき、「小破」
                else if (percentage <= 0.75) color = Color.FromRgb(240, 240, 0);

                // 0.75 より大きいとき、「小破未満」
                else color = Color.FromRgb(64, 200, 32);

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
