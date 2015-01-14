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
    public class KeySettingNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Models.KeySetting)
            {
                var keySetting = value as Models.KeySetting;
                if (keySetting != null)
                {
                    if (keySetting.IsNotSetKey)
                    {
                        return TextResource.KeySetting_IsNotSetKey;
                    }

                    return Helper.KeysHelper.ToName(keySetting.ModifierKeys, keySetting.Key);
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
