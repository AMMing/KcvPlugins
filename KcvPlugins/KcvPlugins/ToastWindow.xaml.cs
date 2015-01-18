using MetroRadiance.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KcvPlugins
{
    /// <summary>
    /// ToastWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ToastWindow : Window
    {
        public ToastWindow()
        {
            InitializeComponent();
            this.InitToast();
            this.Loaded += ToastWindow_Loaded;

        }

        void ToastWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Dpi dpi = this.GetSystemDpi() ?? Dpi.Default;
            this.Top = 0;
            this.Left = Screen.PrimaryScreen.Bounds.Width / dpi.ScaleX - this.Width;
        }

        int index = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Show(string.Format("title_{0}", index), string.Format("content_{0}", index));
            index++;
        }

        #region Toasts

        private List<ToastItemControl> ToastItems;

        private void InitToast()
        {
            var list = sp_toasts.Children.OfType<ToastItemControl>();
            ToastItems = list.ToList();

            ToastItems.ForEach(item => item.AnimationHideComplete += item_AnimationHideComplete);
        }

        void item_AnimationHideComplete(object sender, EventArgs e)
        {
            ShowLastMessage();
        }

        private ToastItemControl GetIdleToast()
        {
            return ToastItems.FirstOrDefault(item => !item.IsPlaying);
        }
        #endregion

        #region method

        private Queue _msgList = new Queue();

        public Queue MessageList
        {
            get { return _msgList; }
            private set { _msgList = value; }
        }

        private void ShowLastMessage()
        {
            var toast = GetIdleToast();
            if (toast != null && MessageList.Count > 0)
            {
                var msg = (ToastMessage)MessageList.Dequeue();
                toast.Show(msg.Title, msg.Content);
            }
        }

        private void Show(string title, string content)
        {
            MessageList.Enqueue(new ToastMessage
            {
                Title = title,
                Content = content
            });

            ShowLastMessage();
        }


        #endregion
    }

    public struct ToastMessage
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
