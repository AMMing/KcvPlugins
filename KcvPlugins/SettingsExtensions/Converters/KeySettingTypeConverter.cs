using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Grabacr07.KanColleViewer.ViewModels.Contents.Fleets;
using System.Windows.Input;

namespace AMing.SettingsExtensions.Converters
{
    public class KeySettingTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enums.KeyType)
            {
                var keySetting = (Enums.KeyType)value;
                var typename = string.Empty;
                switch (keySetting)
                {
                    case AMing.SettingsExtensions.Enums.KeyType.Normal:
                        typename = TextResource.KeyType_Normal;
                        break;
                    case AMing.SettingsExtensions.Enums.KeyType.HotKey:
                        typename = TextResource.KeyType_HotKey;
                        break;
                    default:
                        break;
                }

                return typename;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
