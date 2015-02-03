using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AMing.WindowsNotifierEx
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class ToastItemControl : UserControl
    {
        public ToastItemControl()
        {
            this.InitializeComponent();
            this.InitStoryboard();
            this.btn_close.Click += btn_close_Click;
            this.MouseDown += ToastItemControl_MouseDown;
        }

        #region member

        public event EventHandler AnimationHideComplete;
        private void OnAnimationHideComplete()
        {
            if (AnimationHideComplete != null)
                AnimationHideComplete(this, EventArgs.Empty);
        }
        public event EventHandler ToastClick;
        private void OnToastClick()
        {
            if (ToastClick != null)
                ToastClick(this, EventArgs.Empty);
        }

        bool isPlayShowAnimation = false, isPlayHideAnimation = false;
        Storyboard storyboard_Show, storyboard_Hide;

        public bool IsPlaying
        {
            get
            {
                return isPlayShowAnimation || isPlayHideAnimation;
            }
        }
        #endregion

        #region method

        private void InitStoryboard()
        {
            storyboard_Show = (Storyboard)this.Resources["Storyboard_Show"];
            storyboard_Show.Completed += (sender, e) =>
            {
                isPlayShowAnimation = false;
                AnimationHide();
            };
            storyboard_Hide = (Storyboard)this.Resources["Storyboard_Hide"];
            storyboard_Hide.Completed += (sender, e) =>
            {
                isPlayHideAnimation = false;
                OnAnimationHideComplete();
            };
        }


        private void AnimationShow()
        {
            if (isPlayShowAnimation) return;
            isPlayShowAnimation = true;

            storyboard_Show.Begin();
        }
        private void AnimationHide()
        {
            if (isPlayHideAnimation) return;
            isPlayHideAnimation = true;

            storyboard_Hide.Begin();
        }



        public bool Show(ToastMessage toast)
        {
            if (this.IsPlaying)
            {
                return false;
            }
            this.tb_title.Text = toast.Title;
            this.tb_content.Text = toast.Content;
            SetStyle(toast.Type);
            this.AnimationShow();

            return true;
        }

        private void SetStyle(Enums.ToastType type)
        {
            if (type == Enums.ToastType.Warning)
            {
                rect_bg_notify.Visibility = System.Windows.Visibility.Collapsed;
                icon_notify.Visibility = System.Windows.Visibility.Collapsed;
                
                rect_bg_warning.Visibility = System.Windows.Visibility.Visible;
                icon_warning.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                rect_bg_warning.Visibility = System.Windows.Visibility.Collapsed;
                icon_warning.Visibility = System.Windows.Visibility.Collapsed;

                rect_bg_notify.Visibility = System.Windows.Visibility.Visible;
                icon_notify.Visibility = System.Windows.Visibility.Visible;
            }
        }
        #endregion


        #region event
        void btn_close_Click(object sender, RoutedEventArgs e)
        {
            storyboard_Show.Stop();
            isPlayShowAnimation = false;
            AnimationHide();
        }

        void ToastItemControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.btn_close_Click(null, null);
            this.OnToastClick();
        }
        #endregion
    }
}