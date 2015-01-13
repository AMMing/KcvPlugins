using System;
using System.Windows;
using System.Windows.Input;
using System.Collections;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace AMing.SettingsExtensions.Helper
{
    public class HotKeyHelper : IDisposable
    {
        public HotKeyHelper(Window win)
        {
            this.Window = win;
            this.Handle = new WindowInteropHelper(this.Window).Handle;

            InstallHotKeyHook();
        }

        #region member
        /// <summary>
        /// 热键编号
        /// </summary>
        public int KeyID { get; set; }
        /// <summary>
        /// 窗体句柄
        /// </summary>
        public IntPtr Handle { get; private set; }
        /// <summary>
        /// 热键所在窗体
        /// </summary>
        public Window Window { get; private set; }
        /// <summary>
        /// 热键控制键
        /// </summary>
        public uint ControlKey { get; private set; }
        /// <summary>
        /// 热键主键
        /// </summary>
        public uint Key { get; private set; }

        #endregion

        #region event
        /// <summary>
        /// 按下热键触发的事件
        /// </summary>
        public event EventHandler HotKeyDown;
        protected virtual void OnHotKeyDown()
        {
            if (HotKeyDown != null)
            {
                HotKeyDown(this, EventArgs.Empty);
            }
        }

        #endregion

        #region static member

        private static Dictionary<int, HotKeyHelper> _keyIDList = new Dictionary<int, HotKeyHelper>();
        /// <summary>
        /// 注册的热键列表
        /// </summary>
        public static Dictionary<int, HotKeyHelper> KeyIDList
        {
            get { return _keyIDList; }
            set { _keyIDList = value; }
        }

        private static List<IntPtr> _addHookWindow = new List<IntPtr>();
        /// <summary>
        /// 注册过窗口消息事件的窗体
        /// </summary>
        public static List<IntPtr> AddHookWindow
        {
            get { return _addHookWindow; }
            set { _addHookWindow = value; }
        }

        /// <summary>
        /// 热键消息编号
        /// </summary>
        private const int WM_HOTKEY = 0x0312;

        #endregion

        #region static method

        /// <summary>
        /// 解除全部热键
        /// </summary>
        public static void UnregisterAll()
        {
            foreach (var item in KeyIDList)
            {
                Win32.HotKey.UnregisterHotKey(item.Value.Handle, item.Value.KeyID);
                item.Value.KeyID = 0;
            }
            KeyIDList.Clear();
        }

        #endregion

        #region method

        #region 注册窗口消息事件
        private HwndSource GetHwndSource()
        {
            if (this.Window == null || this.Handle == IntPtr.Zero)
            {
                return null;
            }

            return HwndSource.FromHwnd(this.Handle);
        }


        private void InstallHotKeyHook()
        {
            var source = GetHwndSource();
            if (source != null)
            {
                source.AddHook(HotKeyHook);
                AddHookWindow.Add(this.Handle);
            }
        }

        private void RemoveHotKeyHook()
        {
            var source = GetHwndSource();
            if (source != null)
            {
                source.RemoveHook(HotKeyHook);
                AddHookWindow.Remove(this.Handle);
            }
        }

        private IntPtr HotKeyHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            try
            {
                if (msg == WM_HOTKEY && wParam.ToInt32() == this.KeyID)
                {
                    OnHotKeyDown();
                }
            }
            catch (Exception)
            {
            }
            return IntPtr.Zero;
        }

        #endregion


        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="control"></param>
        /// <param name="key"></param>
        public void Register(ModifierKeys control, Key key)
        {
            Unregister();
            ControlKey = (uint)control;
            Key = (uint)KeyInterop.VirtualKeyFromKey(key);
            KeyID = (int)ControlKey + (int)Key * 10;

            if (KeyIDList.ContainsKey(KeyID))
            {
                throw new NotImplementedException(TextResource.Hotkey_Is_Already_Registered);
            }
            //注册热键
            if (!Win32.HotKey.RegisterHotKey(Handle, KeyID, ControlKey, Key))
            {
                throw new NotImplementedException(TextResource.Registration_HotKey_Failure);
            }

            KeyIDList.Add(KeyID, this);
        }

        /// <summary>
        /// 解除热键
        /// </summary>
        /// <param name="control"></param>
        /// <param name="key"></param>
        public void Unregister()
        {
            if (KeyID > 0)
            {
                Win32.HotKey.UnregisterHotKey(this.Handle, this.KeyID);
            }
            KeyIDList.Remove(KeyID);
            KeyID = 0;
        }


        public void Dispose()
        {
            Unregister();
            RemoveHotKeyHook();
        }

        #endregion

        /// <summary>
        /// 析构函数,解除热键
        /// </summary>
        ~HotKeyHelper()
        {
            Dispose();
        }
    }
}
