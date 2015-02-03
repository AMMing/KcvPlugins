using System.Reflection;

namespace AMing.Plugins.Core.Extensions
{

    public static class ReflectionEx
    {
        const BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        /// <summary>
        /// 获取私有字段
        /// </summary>
        public static T GetField<T>(this object data, string name)
        {
            var type = data.GetType();
            var field = type.GetField(name, flag);

            return (T)field.GetValue(data);
        }
        /// <summary>
        /// 获取私有属性
        /// </summary>
        public static T GetProperty<T>(this object data, string name)
        {
            var type = data.GetType();
            var prop = type.GetProperty(name, flag);

            return (T)prop.GetValue(data, null);
        }
        /// <summary>
        /// 获取私有方法
        /// </summary>
        public static T GetMethod<T>(this object data, string name, params object[] parameters)
        {
            var type = data.GetType();
            var method = type.GetMethod(name, flag);
            var result = method.Invoke(data, parameters);

            return (T)result;
        }
    }
}
