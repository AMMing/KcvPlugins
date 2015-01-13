using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nsModules = AMing.SettingsExtensions.Modules;

namespace AMing.SettingsExtensions.Models
{
    public class ModulesItem
    {
        #region MyRegion

        public ModulesItem() { }
        public ModulesItem(object modules, string key, string name)
        {
            this.Modules = modules;
            this.ModulesKey = string.Format(Entrance.PublicModulesKey + key);
            this.MessageKey = string.Format(Entrance.MessagerKey + key);
            this.ModulesName = name;
        }

        #endregion

        #region member

        /// <summary>
        /// 模块对象
        /// </summary>
        public object Modules { get; set; }
        /// <summary>
        /// 模块key
        /// </summary>
        public string ModulesKey { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModulesName { get; set; }
        /// <summary>
        /// 消息key
        /// </summary>
        public string MessageKey { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public Enums.ModulesType Type { get; set; }
        /// <summary>
        /// EnabelChange消息key
        /// </summary>
        public string EnabelChangeMessageKey
        {
            get
            {
                return string.Format("{0}.EnabelChange", this.MessageKey);
            }
        }

        private bool _isEnabel = true;
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabel
        {
            get { return _isEnabel; }
            set
            {
                if (_isEnabel != value)
                {
                    _isEnabel = value;
                    OnEnabelChange(value);
                }
            }
        }

        #endregion

        #region event and messager

        /// <summary>
        /// IsEnabel改变时触发
        /// </summary>
        public event EventHandler<bool> EnabelChange;

        private void OnEnabelChange(bool isenabel)
        {
            if (EnabelChange != null)
            {
                EnabelChange(this, isenabel);
                nsModules.MessagerModules.Current.Send<bool>(this.EnabelChangeMessageKey, isenabel);
            }
        }

        /// <summary>
        /// 注册模块消息接收回调
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        public void Register<T>(Action<T> callback)
        {
            nsModules.MessagerModules.Current.Register<T>(this.Modules, this.MessageKey, callback);
        }
        /// <summary>
        /// 注册模块消息接收回调
        /// </summary>
        /// <param name="callback"></param>
        public void Register(Action callback)
        {
            this.Register<object>(obj => callback());
        }

        /// <summary>
        /// 注册EnabelChange接收回调
        /// </summary>
        /// <param name="callback"></param>
        public void RegisterEnabelChangeCallbck(Action<bool> callback)
        {
            nsModules.MessagerModules.Current.Register<bool>(this.Modules, this.EnabelChangeMessageKey, callback);
        }

        #endregion
    }
}
