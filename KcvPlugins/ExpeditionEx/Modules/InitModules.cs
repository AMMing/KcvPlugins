using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.ExpeditionEx.Modules
{
    public class InitModules : AMing.Plugins.Core.Modules.InitModules
    {
        public override void SetModules()
        {
            base.SetModules();

            //ModulesList.Add(Modules.MainWindowModules.Current);
            //ModulesList.Add(Modules.ExitTipModules.Current);
            //ModulesList.Add(Modules.NotifyIconModules.Current);
            //ModulesList.Add(Modules.KeysModules.Current);
            //ModulesList.Add(Modules.ThemeModules.Current);
            //ModulesList.Add(Modules.WindowViewModules.Current);
            //ModulesList.Add(Modules.SimpleFleetModules.Current);
        }
    }
}
