using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMing.Plugins.Core.Helper
{
    public class UIHelper
    {
        /// <summary>
        /// 判断是否是指定类型的控件，是的话调用回调函数
        /// </summary>
        /// <typeparam name="T">判断控件类型</typeparam>
        /// <param name="obj">控件</param>
        /// <param name="callback">成功之后的回调</param>
        public static void GetControl<T>(object obj, Action<T> callback)
            where T : UIElement
        {
            if (obj is T)
            {
                callback(obj as T);
            }

        }
    }
}
