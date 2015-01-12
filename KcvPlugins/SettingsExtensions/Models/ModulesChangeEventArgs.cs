using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.SettingsExtensions.Models
{
    public class ModulesChangeEventArgs : EventArgs
    {
        public List<Models.PublicModulesItem> ChangeList { get; set; }

        public Enums.ModulesChangeEventArgsType Type { get; set; }

        public ModulesChangeEventArgs(Models.PublicModulesItem modules, Enums.ModulesChangeEventArgsType type)
        {
            var list = new List<Models.PublicModulesItem>();
            list.Add(modules);
            this.ChangeList = list;
            this.Type = type;
        }
        public ModulesChangeEventArgs(List<Models.PublicModulesItem> list, Enums.ModulesChangeEventArgsType type)
        {
            this.ChangeList = list;
            this.Type = type;
        }
    }
}
