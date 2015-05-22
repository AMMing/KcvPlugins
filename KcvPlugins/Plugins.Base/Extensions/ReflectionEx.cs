using System.Reflection;
using System;
using System.Text;
using System.Linq;

namespace AMing.Plugins.Base.Extensions
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

        public static string ToStringContent(this object data, int layout = 0, int maxlayout = 3)
        {
            StringBuilder sb = new StringBuilder();
            var type = data.GetType();
            var props = type.GetProperties();
            if (props != null)
            {
                props.ForEach(item =>
                {
                    #region Item
                    try
                    {
                        sb.Append('\t', layout);
                        var val = item.GetValue(data);
                        string val_string = string.Empty;
                        if (val != null &&
                            layout <= maxlayout &&
                            item.PropertyType != type &&
                            item.PropertyType != typeof(string) &&
                            !item.PropertyType.IsArray &&
                            item.PropertyType.IsClass)
                        {
                            val_string = string.Format("\n{0}", val.ToStringContent(layout + 1));
                        }
                        else
                        {
                            val_string = Convert.ToString(val ?? "null");
                        }
                        sb.AppendFormat("{0}:{1}\n", item.Name, val_string);
                    }
                    catch (Exception ex)
                    {
                        sb.AppendFormat("{0}:{1}\n", item.Name, ex.Message);
                    }
                    #endregion
                });
            }

            return sb.ToString();
        }
        public static string ToStringContentAndType(this object data, int layout = 0, int maxlayout = 3)
        {
            StringBuilder sb = new StringBuilder();
            var type = data.GetType();
            sb.AppendFormat("{0}\n", type.FullName);
            sb.Append(data.ToStringContent());

            return sb.ToString();
        }
    }
}
