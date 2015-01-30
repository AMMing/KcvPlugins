using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMing.Plugins.Core.Modules
{
    /// <summary>
    /// 初始化模块
    /// </summary>
    public class InitModules : ModulesBase
    {
        public InitModules()
        {
            SetModules();
        }

        public virtual List<Modules.ModulesBase> ModulesList { get; set; }

        bool isActivated = false;

        #region method

        public virtual void SetModules()
        {
            ModulesList = new List<Modules.ModulesBase>();
        }


        public override void Initialize()
        {
            base.Initialize();
            Application.Current.Activated += Current_Activated;
            Application.Current.Exit += Current_Exit;
        }

        public override void Dispose()
        {
            base.Dispose();
            Application.Current.Activated -= Current_Activated;
            Application.Current.Exit -= Current_Exit;
            DisposeModulesList();
        }

        void InitModulesList()
        {
            ModulesList.ForEach(modules => modules.Initialize());
        }

        void DisposeModulesList()
        {
            ModulesList.ForEach(modules => modules.Dispose());
        }

        #endregion

        #region event

        void Current_Activated(object sender, EventArgs e)
        {
            if (isActivated)
            {
                return;
            }
            isActivated = true;
            InitModulesList();
        }
        void Current_Exit(object sender, ExitEventArgs e)
        {
            this.Dispose();
        }

        #endregion
    }
}