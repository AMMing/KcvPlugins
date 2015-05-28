using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Base.Generic
{
    public class PluginHelper
    {
        #region current

        private static readonly PluginHelper _current = new PluginHelper();

        public static PluginHelper Current
        {
            get { return _current; }
        }

        #endregion

        #region member

        /// <summary>
        /// 全部的插件（包含手动注册和自动注册）
        /// </summary>
        public Dictionary<string, Interface.IPluginBase> AllPlugins { get; private set; }

        /// <summary>
        /// 自动注册的插件
        /// </summary>
        public Dictionary<string, Interface.IPlugin> Plugins { get; private set; }

        #endregion

        #region event

        /// <summary>
        /// 当有新插件注册时
        /// </summary>
        public event EventHandler<Interface.IPluginBase> PluginBaseRegister;

        private void OnPluginBaseRegister(Interface.IPluginBase plugin)
        {
            if (PluginBaseRegister == null) return;
            PluginBaseRegister(this, plugin);
        }

        #endregion

        #region method

        public PluginHelper()
        {
            this.AllPlugins = new Dictionary<string, Interface.IPluginBase>();
            this.Plugins = new Dictionary<string, Interface.IPlugin>();
        }


        /// <summary>
        /// 注册插件
        /// </summary>
        /// <param name="plugin"></param>
        /// <returns></returns>
        public bool Register_PluginBase(Interface.IPluginBase plugin)
        {
            if (this.AllPlugins.ContainsKey(plugin.PluginKey)) return false;

            this.AllPlugins.Add(plugin.PluginKey, plugin);
            OnPluginBaseRegister(plugin);

            return true;
        }
        /// <summary>
        /// 注册插件（自动）
        /// </summary>
        /// <param name="plugin"></param>
        /// <returns></returns>
        internal bool Register_Plugin(Interface.IPlugin plugin)
        {
            if (this.AllPlugins.ContainsKey(plugin.PluginKey) || this.Plugins.ContainsKey(plugin.PluginKey)) return false;

            this.Plugins.Add(plugin.PluginKey, plugin);
            this.AllPlugins.Add(plugin.PluginKey, plugin);

            this.InitPlugin(plugin);

            OnPluginBaseRegister(plugin);

            return true;
        }

        /// <summary>
        /// 是否注册了某个插件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool HasPlugin(string key)
        {
            return this.AllPlugins.ContainsKey(key);
        }
        /// <summary>
        /// 获取某个插件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Interface.IPluginBase GetPlugin(string key)
        {
            if (this.AllPlugins.ContainsKey(key))
            {
                return this.AllPlugins[key];
            }

            return null;
        }

        /// <summary>
        /// 初始化插件中的一些配置和模块
        /// </summary>
        /// <param name="plugin"></param>
        private void InitPlugin(Interface.IPlugin plugin)
        {
            plugin.Initialize_Start();

            if (plugin.Settings != null)
            {
                SettingsHelper.Current.AddSettings(plugin.Settings);
            }
            if (plugin.Modules != null)
            {
                ModulesHelper.Current.AddModulesList(plugin.Modules);
            }

            plugin.Initialize_End();
        }
        #endregion

    }
}
