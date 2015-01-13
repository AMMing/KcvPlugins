using Livet.Behaviors.Messaging;
using Livet.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.SettingsExtensions.Models
{
    public class MessageAction
    {
        public object MessageObject { get; set; }
        public string MessageKey { get; set; }
        /// <summary>
        /// 消息被触发
        /// </summary>
        public event EventHandler<string> MessengerTrigger;

        public void OnMessengerTrigger(string key, object val)
        {
            if (MessengerTrigger != null)
                MessengerTrigger(val, key);
        }
    }
}
