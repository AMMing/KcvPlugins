using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AMing.SettingsExtensions.Models
{
    public class PublicModulesItem
    {
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
        /// 回调
        /// </summary>
        public Action<object> Callback { get; set; }

        private bool _isEnabel;
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

        /// <summary>
        /// IsEnabel改变时触发
        /// </summary>
        public event EventHandler<bool> EnabelChange;

        private void OnEnabelChange(bool isenabel)
        {
            if (EnabelChange != null)
                EnabelChange(this, isenabel);
        }
    }
}
