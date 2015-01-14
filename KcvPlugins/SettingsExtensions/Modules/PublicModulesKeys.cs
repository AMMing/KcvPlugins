using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.SettingsExtensions.Modules
{
    public class PublicModulesKeys
    {
        public const string HideAllWindows = "HideAllWindows";
        public const string ShowAllWindows = "ShowAllWindows";
        public const string ChangeAllWindowsByMainWindow = "ChangeAllWindowsByMainWindow";
        public const string ChangeTabs = "ChangeTabs";

        public const string EnableSimpleFleet = "EnableSimpleFleet";
        public const string GhostSimpleFleet = "GhostSimpleFleet";


        public static string GetModulesKey(string key)
        {
            return string.Format(Entrance.PublicModulesKey + key);
        }
    }
}
