using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Base.Interface
{
    /// <summary>
    /// 收听者接口
    /// </summary>
    public interface IListenerMember
    {
        /// <summary>
        /// 监听的对象
        /// </summary>
        object ListenerObject { get; }
        /// <summary>
        /// 是否发送广播
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsSend(string key);
        /// <summary>
        /// 广播的接收方法
        /// </summary>
        Action<dynamic> Receive { get; }
        /// <summary>
        /// 发送广播
        /// </summary>
        /// <param name="args"></param>
        void OnReceive(dynamic args);
    }
}
