using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Core.Models
{
    public class ModulesChangeEventArgs : EventArgs
    {
        public List<Models.ModulesItem> ChangeList { get; set; }

        public Enums.ModulesChangeEventArgsType Type { get; set; }

        public ModulesChangeEventArgs(Models.ModulesItem modules, Enums.ModulesChangeEventArgsType type)
        {
            var list = new List<Models.ModulesItem>();
            list.Add(modules);
            this.ChangeList = list;
            this.Type = type;
        }
        public ModulesChangeEventArgs(List<Models.ModulesItem> list, Enums.ModulesChangeEventArgsType type)
        {
            this.ChangeList = list;
            this.Type = type;
        }
    }
}
