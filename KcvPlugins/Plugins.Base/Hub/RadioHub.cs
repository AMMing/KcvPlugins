using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMing.Plugins.Base.Extensions;

namespace AMing.Plugins.Base.Hub
{
    /// <summary>
    /// 广播中心
    /// </summary>
    public class RadioHub
    {
        #region current

        private readonly static RadioHub _current = new RadioHub();
        /// <summary>
        /// RadioHub的实例
        /// </summary>
        public static RadioHub Current
        {
            get { return _current; }
        }


        #endregion

        #region member


        private List<Interface.IListenerMember> _listenerMemberList = new List<Interface.IListenerMember>();

        /// <summary>
        /// 所有收听者的集合
        /// </summary>
        public List<Interface.IListenerMember> ListenerMemberList
        {
            get { return _listenerMemberList; }
            private set { _listenerMemberList = value; }
        }


        #endregion

        #region method
        /// <summary>
        /// 是否存在监听者
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="receive"></param>
        /// <returns></returns>
        public bool HasListenerMember(object obj, Action<dynamic> receive)
        {
            return this.ListenerMemberList.FirstOrDefault(x => x.ListenerObject.Equals(obj) && x.Receive.Equals(receive)) != null;
        }
        /// <summary>
        /// 是否存在监听者
        /// </summary>
        /// <param name="listenerMember">收听者对象</param>
        /// <returns></returns>
        public bool HasListenerMember(Interface.IListenerMember listenerMember)
        {
            if (this.ListenerMemberList.Contains(listenerMember)) return true;

            return this.HasListenerMember(listenerMember.ListenerObject, listenerMember.Receive);
        }

        /// <summary>
        /// 注册收听者
        /// </summary>
        /// <param name="listenerMember">收听者对象</param>
        /// <returns></returns>
        public bool Register(Interface.IListenerMember listenerMember)
        {
            this.ListenerMemberList.Add(listenerMember);

            return true;
        }
        /// <summary>
        /// 注册收听者
        /// </summary>
        /// <param name="obj">监听的对象</param>
        /// <param name="key">符合key就收听</param>
        /// <param name="receive">广播的接收方法</param>
        /// <returns></returns>
        public bool Register(object obj, string key, Action<dynamic> receive)
        {
            var listenerMember = new ListenerMember()
            {
                ListenerObject = obj,
                ListenerKey = key,
                Receive = receive
            };

            return Register(listenerMember);
        }

        /// <summary>
        /// 注销收听者
        /// </summary>
        /// <param name="obj">监听的对象</param>
        /// <returns>移除的数量</returns>
        public int Unregister(object obj)
        {
            return this.ListenerMemberList.RemoveAll(x => x.ListenerObject.Equals(obj));
        }
        /// <summary>
        /// 注销全部收听者
        /// </summary>
        public void Unregister()
        {
            this.ListenerMemberList.Clear();
        }

        /// <summary>
        /// 发送给收听了指定的key的收听者
        /// </summary>
        /// <param name="key"></param>
        /// <param name="args"></param>
        public void Send(string key, dynamic args)
        {
            this.ListenerMemberList.Where(x => x.IsSend(key)).
                ForEach(x => x.OnReceive(args));
        }
        /// <summary>
        /// 发送一条字符串消息给全部收听者，除了拒收的之外
        /// </summary>
        /// <param name="val">字符串消息内容</param>
        public void SendAll(string val)
        {
            dynamic args = new DynamicArgs<string>(val);
            this.Send(Send_All, args);
        }
        /// <summary>
        /// 发送异常
        /// </summary>
        /// <param name="ex">异常</param>
        public void SendException(Exception ex)
        {
            dynamic args = new DynamicArgs<Exception>(ex);
            this.Send(Send_Exception, args);
        }

        /// <summary>
        /// 发送给全部收听者，除了拒收的之外
        /// </summary>
        public const string Send_All = "RadioHub.Send_All";
        /// <summary>
        /// 发送异常
        /// </summary>
        public const string Send_Exception = "RadioHub.Send_Exception";

        /// <summary>
        /// 是否是全部通知的消息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsSendAll(string key)
        {
            return Send_All.Equals(key);
        }
        /// <summary>
        /// 是否是异常的消息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsSendException(string key)
        {
            return Send_Exception.Equals(key);
        }
        #endregion
    }
}
