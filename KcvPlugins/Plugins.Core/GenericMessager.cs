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

        #region Send Message

        /// <summary>
        /// 发送日志消息
        /// </summary>
        public void SendToLogs(string val)
        {
            Modules.MessagerModules.Current.Send<string>(GetKey(Enums.MessageType.Logs), val);
        }
        /// <summary>
        /// 发送通知消息
        /// </summary>
        public void SendToNotification(Models.MessageItem val)
        {
            Modules.MessagerModules.Current.Send<Models.MessageItem>(GetKey(Enums.MessageType.Notification), val);
        }
        /// <summary>
        /// 发送警告消息
        /// </summary>
        public void SendToWarning(Models.MessageItem val)
        {
            Modules.MessagerModules.Current.Send<Models.MessageItem>(GetKey(Enums.MessageType.Warning), val);
        }
        /// <summary>
        /// 发送异常消息
        /// </summary>
        public void SendToException(Exception ex)
        {
            Modules.MessagerModules.Current.Send<Exception>(GetKey(Enums.MessageType.Error), ex);
        }


        #endregion

        #region  Register Message


        /// <summary>
        /// 注册日志消息接受
        /// </summary>
        public void RegisterForLogs(object thisobj, Action<string> callback)
        {
            Modules.MessagerModules.Current.Register<string>(thisobj, GetKey(Enums.MessageType.Logs), callback);
        }
        /// <summary>
        /// 注册通知消息接受
        /// </summary>
        public void RegisterForNotification(object thisobj, Action<Models.MessageItem> callback)
        {
            Modules.MessagerModules.Current.Register<Models.MessageItem>(thisobj, GetKey(Enums.MessageType.Notification), callback);
        }

        /// <summary>
        /// 注册警告消息接受
        /// </summary>
        public void RegisterForWarning(object thisobj, Action<Models.MessageItem> callback)
        {
            Modules.MessagerModules.Current.Register<Models.MessageItem>(thisobj, GetKey(Enums.MessageType.Warning), callback);
        }


        /// <summary>
        /// 注册异常消息接受
        /// </summary>
        public void RegisterForException(object thisobj, Action<Exception> callback)
        {
            Modules.MessagerModules.Current.Register<Exception>(thisobj, GetKey(Enums.MessageType.Error), callback);
        }

        #endregion

        #endregion
    }
}
