using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AMing.SettingsExtensions.Models
{
    public class KeySetting
    {
        /// <summary>
        /// 模块key
        /// </summary>
        public string ModulesKey { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        public Key Key { get; set; }
        /// <summary>
        /// 组合键
        /// </summary>
        public ModifierKeys ModifierKeys { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public Enums.KeyType Type { get; set; }
    }
}
