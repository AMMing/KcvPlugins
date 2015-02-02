using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMing.Plugins.Core.Helper
{
    public class ThreadHelper
    {
        /// <summary>
        /// 延迟执行(多线程)
        /// </summary>
        /// <param name="time">延迟时间</param>
        /// <param name="method">执行方法</param>
        public void DeferredExecution(double time, Action method)
        {
            System.Threading.Thread thread = new System.Threading.Thread((obj) =>
            {
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(time));
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    method();
                }));
            });
            thread.IsBackground = true;
            thread.Name = "延迟用线程";
            thread.Start();
        }

        /// <summary>
        /// 后台处理(多线程)
        /// </summary>
        /// <param name="method">执行方法</param>
        public void Background(Action method)
        {
            System.Threading.Thread thread = new System.Threading.Thread((obj) =>
            {
                method();
            });
            thread.IsBackground = true;
            thread.Name = "后台处理用线程";
            thread.Start();
        }
    }
}
