using AMing.Plugins.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMing.Plugins.Base.Extensions;
using System.Dynamic;

namespace AMing.Plugins.Base.Model
{
    /// <summary>
    /// 动态参数传递实体的基类
    /// </summary>
    public abstract class DynamicArgsBase : IDynamicArgs
    {
        /// <summary>
        /// 验证key
        /// </summary>
        public abstract string ValidationKey { get; }


        /// <summary>
        /// 验证参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Validation(string key)
        {
            return Validation(this.ValidationKey, key);
        }


        /// <summary>
        /// 验证参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Validation(string key1, string key2)
        {
            if (string.IsNullOrWhiteSpace(key1) || string.IsNullOrWhiteSpace(key2)) return false;

            return key1.Equals(key2);
        }
        /// <summary>
        /// 验证参数
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool Validation(IDynamicArgs args1, IDynamicArgs args2)
        {
            if (args1 == null || args2 == null) return false;

            return Validation(args1.ValidationKey, args2.ValidationKey);
        }
        /// <summary>
        /// 验证参数
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool Validation(dynamic args, string key)
        {
            if (args == null || string.IsNullOrWhiteSpace(key)) return false;
            if (!(args is IDynamicArgs)) return false;

            return args != null && Validation(args.ValidationKey, key);
        }

        /// <summary>
        /// 验证并转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="d_val"></param>
        /// <param name="r_val"></param>
        /// <returns></returns>
        public static bool Parse<T>(dynamic d_val, ref T r_val)
            where T : class, IDynamicArgs
        {
            if (!(d_val is IDynamicArgs)) return false;
            if (d_val is T)
            {
                r_val = (T)d_val;

                return true;
            }
            var val = d_val as IDynamicArgs;
            if (val == null || !Validation(r_val, val)) return false;

            r_val = (T)val.CopyTo<T>();

            return true;
        }
    }
}
