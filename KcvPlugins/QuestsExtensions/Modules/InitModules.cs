using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMing.QuestsExtensions.Modules
{
    public class InitModules : ModulesBase
    {
        #region Current

        private static InitModules _current = new InitModules();

        public static InitModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        public InitModules()
        {
            ModulesList = new List<Modules.ModulesBase>();

            ModulesList.Add(Modules.QuestsModules.Current);
        }

        public List<Modules.ModulesBase> ModulesList { get; set; }
        bool isActivated = false;

        #region method

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
