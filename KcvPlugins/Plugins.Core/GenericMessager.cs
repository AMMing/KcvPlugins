using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Core
{
    public class GenericMessager
    {
        #region Current

        private static GenericMessager _current = new GenericMessager();

        public static GenericMessager Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        #region method
        private string GetKey(Enums.MessageType type)
        {
            return string.Format("GenericMessager_Type_{0}", type);
        }

        /// <summary>
        /// 发送日志消息
        /// </summary>
        public void SendToLogs(string val)
        {
            Modules.MessagerModules.Current.Send<string>(GetKey(Enums.MessageType.Logs), val);
        }
        /// <summary>
        /// 发送消息(只能用于传递Warning或者Notification)
        /// </summary>
        public void SendToMessage(Enums.MessageType type, Models.MessageItem val)
        {
            if (type != Enums.MessageType.Warning || type != Enums.MessageType.Notification)
            {
                throw new ArgumentException();
            }
            Modules.MessagerModules.Current.Send<Models.MessageItem>(GetKey(type), val);
        }
        /// <summary>
        /// 发送异常消息
        /// </summary>
        public void SendToException(Exception ex)
        {
            Modules.MessagerModules.Current.Send<Exception>(GetKey(Enums.MessageType.Error), ex);
        }

        /// <summary>
        /// 注册日志消息接受
        /// </summary>
        public void RegisterForLogs(object thisobj, Action<string> callback)
        {
            Modules.MessagerModules.Current.Register<string>(thisobj, GetKey(Enums.MessageType.Logs), callback);
        }
        /// <summary>
        /// 注册消息接受(只能用于传递Warning或者Notification)
        /// </summary>
        public void RegisterForMessage(object thisobj, Enums.MessageType type, Action<Models.MessageItem> callback)
        {
            if (type != Enums.MessageType.Warning && type != Enums.MessageType.Notification)
            {
                throw new ArgumentException();
            }
            Modules.MessagerModules.Current.Register<Models.MessageItem>(thisobj, GetKey(type), callback);
        }
        /// <summary>
        /// 注册异常消息接受
        /// </summary>
        public void RegisterForException(object thisobj, Action<Exception> callback)
        {
            Modules.MessagerModules.Current.Register<Exception>(thisobj, GetKey(Enums.MessageType.Error), callback);
        }

        #endregion
    }
}
