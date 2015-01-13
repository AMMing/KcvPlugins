using AMing.SettingsExtensions.Helper;
using Grabacr07.Desktop.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using kcv = Grabacr07.KanColleViewer;

namespace AMing.SettingsExtensions.Modules
{
    public class PublicModules
    {
        #region Current

        private static PublicModules _current = new PublicModules();

        public static PublicModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        #region member

        public List<Models.ModulesItem> PublicModulesList { get; set; }

        public event EventHandler<Models.ModulesChangeEventArgs> ModulesChange;

        private void OnModulesChange(Models.ModulesChangeEventArgs args)
        {
            if (ModulesChange != null)
                ModulesChange(this, args);
        }

        #endregion
        public PublicModules()
        {
            PublicModulesList = new List<Models.ModulesItem>();
        }

        #region method
        /// <summary>
        /// 添加公开模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisobj"></param>
        /// <param name="name"></param>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        public void Add(Models.ModulesItem modules)
        {
            this.PublicModulesList.Add(modules);
            OnModulesChange(new Models.ModulesChangeEventArgs(modules, Enums.ModulesChangeEventArgsType.Add));
        }
        /// <summary>
        /// 删除公开模块
        /// </summary>
        /// <param name="thisobj"></param>
        /// <param name="name"></param>
        public void Remove(object thisobj, string key)
        {
            var result = this.PublicModulesList.Where(modules_item => modules_item.ModulesKey == key && modules_item.Modules.Equals(thisobj));
            if (result != null)
            {
                var temp = result.ToList();
                foreach (var item in temp)
                {
                    this.PublicModulesList.Remove(item);
                }
                OnModulesChange(new Models.ModulesChangeEventArgs(temp, Enums.ModulesChangeEventArgsType.Remove));
            }
        }

        #endregion

    }
}
