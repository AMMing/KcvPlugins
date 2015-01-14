using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AMing.SettingsExtensions.Helper
{
    public class KeysHelper
    {
        public static string ToName(ModifierKeys modifierKeys, Key key)
        {
            string modifierkey_txt = modifierKeys == ModifierKeys.None ? string.Empty : modifierKeys.ToString(),
                   key_txt = key == Key.None ? string.Empty : key.ToString();

            var keyText = string.Format("{0}{1}{2}",
                modifierkey_txt,
                (modifierKeys == ModifierKeys.None || key == Key.None) ? string.Empty : ",", key_txt);

            keyText = keyText.Replace(",", " + ");

            return keyText;
        }
    }
}
