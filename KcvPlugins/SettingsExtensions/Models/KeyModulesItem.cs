using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.SettingsExtensions.Models
{
    public class KeyModulesItem
    {
        public ModulesItem ModulesItem { get; set; }
        public KeySetting KeySetting { get; set; }
        public Helper.HotKeyHelper HotKeyHelper { get; set; }

        public bool ModulesIsInvalid { get; set; }
    }
}
