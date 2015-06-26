using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Base.Hub
{
    public class MethodHub
    {
        #region current

        private readonly static MethodHub _current = new MethodHub();
        /// <summary>
        /// MethodHub的实例
        /// </summary>
        public static MethodHub Current
        {
            get { return _current; }
        }


        #endregion

        #region member


        private Dictionary<string, Func<dynamic, dynamic>> _methodDictionary = new Dictionary<string, Func<dynamic, dynamic>>();

        /// <summary>
        /// 保存所有方法集合
        /// </summary>
        public Dictionary<string, Func<dynamic, dynamic>> MethodDictionary
        {
            get { return _methodDictionary; }
            private set { _methodDictionary = value; }
        }


        #endregion

        #region method

        /// <summary>
        /// 是否存在方法
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public bool HasMethod(string key)
        {
            return this.MethodDictionary.ContainsKey(key);
        }

        /// <summary>
        /// 注册方法
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="func">方法</param>
        /// <returns></returns>
        public bool Register(string key, Func<dynamic, dynamic> func)
        {
            if (this.MethodDictionary.ContainsKey(key)) return false;

            this.MethodDictionary.Add(key, func);

            return true;
        }

        /// <summary>
        /// 注销方法
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public bool Unregister(string key)
        {
            if (!this.MethodDictionary.ContainsKey(key)) return false;

            this.MethodDictionary.Remove(key);

            return true;
        }

        /// <summary>
        /// 获取方法
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>方法，key不存在时返回null</returns>
        public Func<dynamic, dynamic> GetMethod(string key)
        {
            if (!this.MethodDictionary.ContainsKey(key)) return null;

            return this.MethodDictionary[key];
        }
        /// <summary>
        /// 获取方法并执行
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="args">参数</param>
        /// <returns>方法运行结果，key不存在时返回null</returns>
        public dynamic ExecuteMethod(string key, dynamic args)
        {
            var func = this.GetMethod(key);
            if (func == null)
                return null;

            return func(args);
        }

        /// <summary>
        /// 获取方法并执行
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="args">参数</param>
        /// <param name="ret_val">返回值</param>
        /// <returns>方法是否存在或者发生异常</returns>
        public bool TryExecuteMethod(string key, dynamic args, ref dynamic ret_val)
        {
            try
            {
                var func = this.GetMethod(key);
                if (func == null)
                    return false;

                ret_val = func(args);

                return true;
            }
            catch (Exception ex)
            {
                RadioHub.Current.SendException(ex);
            }

            return false;
        }

        #endregion
    }
}
