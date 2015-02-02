using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.WindowsNotifierForWin7.Modules
{
    public class InitModules : AMing.Plugins.Core.Modules.InitModules
    {
        public override void SetModules()
        {
            base.SetModules();

            ModulesList.Add(Modules.NotifierModules.Current);
        }
    }
}
