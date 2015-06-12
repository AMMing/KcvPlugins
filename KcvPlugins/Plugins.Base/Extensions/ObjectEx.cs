using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Base.Extensions
{
    public static class ObjectEx
    {
        #region ChangeType

        /// <summary>
        /// 转成指定类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="type">类型</param>
        /// <returns>转换的结果</returns>
        public static object ChangeType(object value, Type type)
        {
            if (value == null && type.IsGenericType) return Activator.CreateInstance(type);
            if (value == null) return null;
            if (type == value.GetType()) return value;
            if (type.IsEnum)
            {
                if (value is string)
                    return Enum.Parse(type, value as string);
                else
                    return Enum.ToObject(type, value);
            }
            if (!type.IsInterface && type.IsGenericType)
            {
                Type innerType = type.GetGenericArguments()[0];
                object innerValue = ChangeType(value, innerType);
                return Activator.CreateInstance(type, new object[] { innerValue });
            }
            if (value is string && type == typeof(Guid)) return new Guid(value as string);
            if (value is string && type == typeof(Version)) return new Version(value as string);
            if (!(value is IConvertible)) return value;
            return Convert.ChangeType(value, type);
        }

        /// <summary>
        /// 转成指定类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="val">值</param>
        /// <param name="defaultval">转换失败的设置的值</param>
        /// <returns>转换的结果</returns>
        public static T To<T>(this object val, T defaultval = default(T))
        {
            object obj = null;
            try
            {
                obj = ChangeType(val, typeof(T));
            }
            catch (Exception)
            {
            }
            obj = obj ?? defaultval;

            return (T)obj;
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// 复制object的值为新的类型
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="source_data"></param>
        /// <param name="toType"></param>
        /// <param name="to_data"></param>
        /// <returns></returns>
        public static object CopyTo(Type source_type, object source_data, Type to_type, object to_data)
        {
            var props = to_type.GetProperties();
            foreach (var item in props)
            {
                var s_item = source_type.GetProperty(item.Name);
                if (s_item == null) continue;

                object source_val = s_item.GetValue(source_data, null);
                if (source_val == null) continue;

                var newval = ObjectEx.ChangeType(source_val, item.PropertyType);
                item.SetValue(to_data, newval, null);
            }

            return to_data;
        }

        /// <summary>
        /// 复制object的值为新的类型
        /// </summary>
        /// <param name="source_data"></param>
        /// <param name="to_data"></param>
        /// <returns></returns>
        public static object CopyTo(object source_data, object to_data)
        {
            Type to_type = to_data.GetType();
            Type source_type = source_data.GetType();

            return CopyTo(source_type, source_data, to_type, to_data);
        }
        /// <summary>
        /// 复制object的值为新的类型
        /// </summary>
        /// <param name="source_data"></param>
        /// <param name="toType"></param>
        /// <returns></returns>
        public static object CopyTo(this object source_data, Type to_type)
        {
            Type source_type = source_data.GetType();
            var to_data = Activator.CreateInstance(to_type);

            return CopyTo(source_type, source_data, to_type, to_data);
        }

        /// <summary>
        /// 复制object的值为新的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source_data"></param>
        /// <returns></returns>
        public static object CopyTo<T>(this object source_data)
        {
            Type to_type = typeof(T);
            return CopyTo(source_data, to_type);
        }

        #endregion
    }
}
