using Grabacr07.KanColleWrapper.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using AMing.Plugins.Core.Extensions;

namespace AMing.Warning.Views
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class StatusItemControl : UserControl, INotifyPropertyChanged
    {
        public StatusItemControl()
        {
            this.InitializeComponent();
            this.InitStoryboard();
            this.Loaded += StatusItemControl_Loaded;
            this.DataContext = this;
        }
        public StatusItemControl(Model.WarningShip ship)
            : this()
        {
            //this.Fleet = fleet;
            this.Ship = ship;
        }

        #region member

        //private Fleet _fleet;

        //public Fleet Fleet
        //{
        //    get { return _fleet; }
        //    set
        //    {
        //        if (_fleet != value)
        //        {
        //            _fleet = value;
        //            OnPropertyChanged("Fleet");
        //        }
        //    }
        //}

        private Model.WarningShip _ship;

        public Model.WarningShip Ship
        {
            get { return _ship; }
            set
            {
                if (_ship != value)
                {
                    _ship = value;
                    OnPropertyChanged("Ship");
                    OnPropertyChanged("ShipStatus");
                }
            }
        }

        public string ShipStatus
        {
            get
            {
                return this.Ship.HP.ShipStatus().ToString();
            }
        }

        bool isPlayShowAnimation = false, isPlayHideAnimation = false;
        Storyboard storyboard_Show, storyboard_Hide, storyboard_Background, storyboard_Font;
        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
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
                AnimationForever_Begin();
            };
            storyboard_Hide = (Storyboard)this.Resources["Storyboard_Hide"];
            storyboard_Hide.Completed += (sender, e) =>
            {
                isPlayHideAnimation = false;
                RemoveMe();
            };

            storyboard_Background = (Storyboard)this.Resources["Storyboard_Background"];
            storyboard_Font = (Storyboard)this.Resources["Storyboard_Font"];

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

            AnimationForever_Stop();
            storyboard_Hide.Begin();
        }
        private void AnimationForever_Begin()
        {
            //storyboard_Background.Begin();
            //storyboard_Font.Begin();
        }
        private void AnimationForever_Stop()
        {
            //storyboard_Background.Stop();
            //storyboard_Font.Stop();
        }

        private void RemoveMe()
        {
            var sp = this.Parent as StackPanel;
            if (sp != null)
            {
                sp.Children.Remove(this);
            }
            OnRemoveMeComplete();
        }

        public void Remove()
        {
            AnimationHide();
        }
        #endregion

        #region event

        public event EventHandler RemoveMeComplete;
        private void OnRemoveMeComplete()
        {
            if (RemoveMeComplete != null)
                RemoveMeComplete(this, EventArgs.Empty);
        }

        #endregion

        void StatusItemControl_Loaded(object sender, RoutedEventArgs e)
        {
            AnimationShow();
        }
    }
}