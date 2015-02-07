using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Logger.Helper
{

    /// <summary>
    /// Json序列化/反序列化
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 序列化Json
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 反序列化Json
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="jsonData">Json数据</param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonData, T defaultVal = default(T))
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonData);
            }
            catch (Exception ex)
            {
                return defaultVal;
            }
        }
    }
}
