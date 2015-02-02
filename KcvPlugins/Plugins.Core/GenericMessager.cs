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
        /// 发送消息
        /// </summary>
        public void Send(Enums.MessageType type, object val)
        {
            Modules.MessagerModules.Current.Send<object>(GetKey(type), val);
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        public void SendToMessage(Enums.MessageType type, Models.MessageItem val)
        {
            Modules.MessagerModules.Current.Send<Models.MessageItem>(GetKey(type), val);
        }

        /// <summary>
        /// 注册消息
        /// </summary>
        public void Register(object thisobj, Enums.MessageType type, Action<object> callback)
        {
            Modules.MessagerModules.Current.Register<object>(thisobj, GetKey(type), callback);
        }
        /// <summary>
        /// 注册消息
        /// </summary>
        public void RegisterForMessage(object thisobj, Enums.MessageType type, Action<Models.MessageItem> callback)
        {
            Modules.MessagerModules.Current.Register<Models.MessageItem>(thisobj, GetKey(type), callback);
        }

        #endregion
    }
}
