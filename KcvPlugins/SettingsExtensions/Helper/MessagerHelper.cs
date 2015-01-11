using Livet.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.SettingsExtensions.Helper
{
    public class MessagerHelper
    {
        #region Current

        private static MessagerHelper _current = new MessagerHelper();

        public static MessagerHelper Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion
        public MessagerHelper()
        {
            MessengerEventData = new List<Messager.MessageAction>();
        }
        #region member

        public List<Messager.MessageAction> MessengerEventData { get; set; }


        #endregion

        #region method

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void Send<T>(string key, T val)
        {
            var result = this.MessengerEventData.Where(msg_item => msg_item.MessageKey == key);
            if (result != null)
            {
                foreach (var item in result)
                {
                    item.OnMessengerTrigger(key, val);
                }
            }
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void Send(string key)
        {
            this.Send<object>(key, null);
        }

        /// <summary>
        /// 注册消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        public void Register<T>(object thisobj, string key, Action<T> callback)
        {
            var msgAction = new Messager.MessageAction { MessageObject = thisobj, MessageKey = key };
            msgAction.MessengerTrigger += (sender, e) => callback((T)sender);
            this.MessengerEventData.Add(msgAction);
        }

        public void Register(object thisobj, string key, Action callback)
        {
            this.Register<object>(thisobj, key, obj => callback());
        }

        /// <summary>
        /// 注销消息
        /// </summary>
        /// <param name="key"></param>
        public void Unregister(object thisobj, string key)
        {
            var result = this.MessengerEventData.Where(msg_item => msg_item.MessageKey == key && msg_item.MessageObject.Equals(thisobj));
            if (result != null)
            {
                var temp = result.ToList();
                foreach (var item in temp)
                {
                    this.MessengerEventData.Remove(item);
                }
            }
        }

        /// <summary>
        /// 注销注册对象的消息全部
        /// </summary>
        public void Unregister(object thisobj)
        {
            var result = this.MessengerEventData.Where(msg_item => msg_item.MessageObject.Equals(thisobj));
            if (result != null)
            {
                foreach (var item in result)
                {
                    this.MessengerEventData.Remove(item);
                }
            }
        }

        /// <summary>
        /// 注销消息全部
        /// </summary>
        public void Unregister()
        {
            this.MessengerEventData.Clear();
        }

        #endregion
    }
}
