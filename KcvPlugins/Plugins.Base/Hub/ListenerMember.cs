using AMing.Plugins.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Base.Hub
{
    /// <summary>
    /// 收听者
    /// </summary>
    public class ListenerMember : ListenerMemberBase
    {
        /// <summary>
        /// 符合key就收听
        /// </summary>
        public string ListenerKey { get; set; }
        /// <summary>
        /// 是否发送广播
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override bool IsSend(string key)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(this.ListenerKey)) return false;

            if (RadioHub.IsSendAll(key)) return true;//默认接受全部消息

            return this.ListenerKey.Equals(key);
        }

        /// <summary>
        /// 发送广播
        /// </summary>
        /// <param name="args"></param>
        public override void OnReceive(dynamic args)
        {
            if (this.Receive != null)
                this.Receive(args);
        }
    }
}
