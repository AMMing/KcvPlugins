using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMing.Plugins.Base.Extensions;
using System.Windows;

namespace AMing.Plugins.Base.Generic
{
    public class ModulesHelper
    {
        #region current

        private static readonly ModulesHelper _current = new ModulesHelper();

        public static ModulesHelper Current
        {
            get { return _current; }
        }

        #endregion

        #region member

        /// <summary>
        /// 全部的模块
        /// </summary>
        public Dictionary<string, Interface.IModules> ModulesList { get; private set; }

        #endregion

        #region method

        public ModulesHelper()
        {
            this.ModulesList = new Dictionary<string, Interface.IModules>();
        }
        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="modules"></param>
        /// <returns></returns>
        public bool AddModules(Interface.IModules modules)
        {
            if (this.ModulesList.ContainsKey(modules.Key)) return false;

            this.ModulesList.Add(modules.Key, modules);

            return true;
        }

        /// <summary>
        /// 添加多个模块
        /// </summary>
        /// <param name="modules_list"></param>
        internal void AddModulesList(IEnumerable<Interface.IModules> modules_list)
        {
            if (modules_list == null) return;

            modules_list.ForEach(x =>
            {
                if (this.AddModules(x))
                {
                    InitModules(x);
                }
            });
        }


        /// <summary>
        /// 初始化配置模块
        /// </summary>
        /// <param name="modules"></param>
        private void InitModules(Interface.IModules modules)
        {
            modules.Initialize_Start();

            modules.IsInitialization = true;

            modules.Initialize_End();
        }

        /// <summary>
        /// 释放所有模块
        /// </summary>
        public void DisposeModulesList()
        {
            ModulesList.ForEach(x => x.Value.Dispose());
        }

        #endregion

        #region event

        //bool isActivated = false;
        //private void Current_Activated(object sender, EventArgs e)
        //{
        //    if (isActivated)
        //    {
        //        return;
        //    }
        //    isActivated = true;
        //    //InitModulesList();
        //}
        //private void Current_Exit(object sender, ExitEventArgs e)
        //{
        //    this.DisposeModulesList();
        //}

        #endregion
    }
}
