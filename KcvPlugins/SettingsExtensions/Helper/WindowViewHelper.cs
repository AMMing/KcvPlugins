using AMing.SettingsExtensions.Views;
using Grabacr07.KanColleViewer.Views.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AMing.Plugins.Core.Extensions;
using AMing.Plugins.Core.Helper;
using AMing.SettingsExtensions.Extensions;

namespace AMing.SettingsExtensions.Helper
{
    public class WindowViewHelper
    {
        #region member


        #region ContainerWindow

        private ContainerWindow _ContainerWindow;

        public ContainerWindow ContainerWindow
        {
            get
            {
                if (this._ContainerWindow == null)
                {
                    this._ContainerWindow = new Views.ContainerWindow
                    {
                        DataContext = Application.Current.MainWindow.DataContext
                    };
                    this._ContainerWindow.ShowHide += (sender, args) =>
                    {
                        this.SplitWindowButton.BtnIsEnabled = !args;
                    };
                }
                return _ContainerWindow;
            }
            set { _ContainerWindow = value; }
        }

        #endregion

        public SplitWindowButton SplitWindowButton { get; set; }

        public TabsWindowButton TabsWindowButton { get; set; }
        #endregion

        #region method

        #region Split

        /// <summary>
        /// 拆分窗体
        /// </summary>
        /// <returns></returns>
        public bool SplitWindow()
        {
            if (!KcvMainWindowControlHelper.Current.IsInit) return false;
            try
            {
                StartLayout();
                #region ContainerWindow

                KcvMainWindowControlHelper.Current.Grid_Content.Children.Remove(KcvMainWindowControlHelper.Current.ContentControl_ToolControl);

                this.ContainerWindow.WindowContent = KcvMainWindowControlHelper.Current.ContentControl_ToolControl;
                this.ContainerWindow.Show();

                #endregion

                this.SplitWindowButton = new SplitWindowButton { BtnIsEnabled = false };
                KcvMainWindowControlHelper.Current.StackPanel_WindowCaptionBar.Children.Insert(0, this.SplitWindowButton);
                this.SplitWindowButton.Click += (sender, args) =>
                {
                    if (this.ContainerWindow != null)
                    {
                        this.ContainerWindow.OnShowHide(true);
                    }
                };

                KcvMainWindowControlHelper.Current.Grid_Content.RowDefinitions.Clear();
                KcvMainWindowControlHelper.Current.Grid_Content.ColumnDefinitions.Clear();
                EndLayout();

                KcvMainWindowControlHelper.Current.KanColleHost.SetMiniWindow(KcvMainWindowControlHelper.Current.Grid_WindowCaptionBar.ActualHeight);
                Application.Current.MainWindow.WindowState = WindowState.Normal;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 合并窗体
        /// </summary>
        /// <returns></returns>
        public bool MergeWindow()
        {
            if (!KcvMainWindowControlHelper.Current.IsInit) return false;
            try
            {
                this.ContainerWindow.WindowContent = null;
                KcvMainWindowControlHelper.Current.Grid_Content.Children.Add(KcvMainWindowControlHelper.Current.ContentControl_ToolControl);
                this.ContainerWindow.Hide();
                KcvMainWindowControlHelper.Current.StackPanel_WindowCaptionBar.Children.Remove(this.SplitWindowButton);
                Application.Current.MainWindow.WindowState = WindowState.Maximized;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Tabs

        bool isTabsMode = false;
        public bool TabsWindow()
        {
            if (!KcvMainWindowControlHelper.Current.IsInit) return false;
            try
            {
                KcvMainWindowControlHelper.Current.Grid_Content.RowDefinitions.Clear();
                KcvMainWindowControlHelper.Current.Grid_Content.ColumnDefinitions.Clear();

                this.TabsWindowButton = new TabsWindowButton();
                KcvMainWindowControlHelper.Current.StackPanel_WindowCaptionBar.Children.Insert(0, this.TabsWindowButton);
                this.TabsWindowButton.GameVisibility += (sender, args) => KcvMainWindowControlHelper.Current.KanColleHost.Visibility = args;
                this.TabsWindowButton.ToolVisibility += (sender, args) => KcvMainWindowControlHelper.Current.ContentControl_ToolControl.Visibility = args;

                KcvMainWindowControlHelper.Current.KanColleHost.Visibility = Visibility.Visible;
                KcvMainWindowControlHelper.Current.ContentControl_ToolControl.Visibility = Visibility.Collapsed;
                isTabsMode = true;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ResetTabsWindow()
        {
            if (!KcvMainWindowControlHelper.Current.IsInit) return false;
            try
            {
                KcvMainWindowControlHelper.Current.StackPanel_WindowCaptionBar.Children.Remove(this.TabsWindowButton);

                KcvMainWindowControlHelper.Current.KanColleHost.Visibility = Visibility.Visible;
                KcvMainWindowControlHelper.Current.Grid_Content.Visibility = Visibility.Visible;
                isTabsMode = false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void ChangeTabs()
        {
            if (this.TabsWindowButton != null && this.TabsWindowButton.IsInitialized && isTabsMode)
            {
                this.TabsWindowButton.TriggerClick();
            }
        }
        #endregion

        #region Top Bottom Left Right

        /// <summary>
        /// 工具栏居上
        /// </summary>
        /// <returns></returns>
        public bool TopWindow()
        {
            if (!KcvMainWindowControlHelper.Current.IsInit) return false;
            try
            {
                StartLayout();
                SetRowDefinitions();
                KcvMainWindowControlHelper.Current.Grid_Content.ColumnDefinitions.Clear();

                Grid.SetRow(KcvMainWindowControlHelper.Current.KanColleHost, 1);
                Grid.SetRow(KcvMainWindowControlHelper.Current.ContentControl_ToolControl, 0);

                RowDefinition_1.Height = gridLengthStar;
                RowDefinition_2.Height = gridLengthAuto;
                EndLayout();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 工具栏居底部
        /// </summary>
        /// <returns></returns>
        public bool BottomWindow()
        {
            if (!KcvMainWindowControlHelper.Current.IsInit) return false;
            try
            {
                StartLayout();
                SetRowDefinitions();
                KcvMainWindowControlHelper.Current.Grid_Content.ColumnDefinitions.Clear();

                Grid.SetRow(KcvMainWindowControlHelper.Current.KanColleHost, 0);
                Grid.SetRow(KcvMainWindowControlHelper.Current.ContentControl_ToolControl, 1);

                RowDefinition_1.Height = gridLengthAuto;
                RowDefinition_2.Height = gridLengthStar;
                EndLayout();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 工具栏居左
        /// </summary>
        /// <returns></returns>
        public bool LeftWindow()
        {
            if (!KcvMainWindowControlHelper.Current.IsInit) return false;
            try
            {
                StartLayout();
                KcvMainWindowControlHelper.Current.ContentControl_ToolControl.Visibility = Visibility.Hidden;
                SetColumnDefinitions();
                KcvMainWindowControlHelper.Current.Grid_Content.RowDefinitions.Clear();

                Grid.SetColumn(KcvMainWindowControlHelper.Current.KanColleHost, 1);
                Grid.SetColumn(KcvMainWindowControlHelper.Current.ContentControl_ToolControl, 0);

                ColumnDefinition_1.Width = gridLengthStar;
                ColumnDefinition_2.Width = gridLengthAuto;
                EndLayout();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 工具栏居右
        /// </summary>
        /// <returns></returns>
        public bool RightWindow()
        {
            if (!KcvMainWindowControlHelper.Current.IsInit) return false;
            try
            {
                StartLayout();
                SetColumnDefinitions();
                KcvMainWindowControlHelper.Current.Grid_Content.RowDefinitions.Clear();

                Grid.SetColumn(KcvMainWindowControlHelper.Current.KanColleHost, 0);
                Grid.SetColumn(KcvMainWindowControlHelper.Current.ContentControl_ToolControl, 1);

                ColumnDefinition_1.Width = gridLengthAuto;
                ColumnDefinition_2.Width = gridLengthStar;
                EndLayout();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region layout

        public RowDefinition RowDefinition_1 { get; set; }
        public RowDefinition RowDefinition_2 { get; set; }
        public ColumnDefinition ColumnDefinition_1 { get; set; }
        public ColumnDefinition ColumnDefinition_2 { get; set; }

        GridLength gridLengthAuto = GridLength.Auto;
        GridLength gridLengthStar = new GridLength(1, GridUnitType.Star);



        public void InitLayout()
        {
            RowDefinition_1 = new RowDefinition { Height = gridLengthAuto };
            RowDefinition_2 = new RowDefinition { Height = gridLengthStar };
            ColumnDefinition_1 = new ColumnDefinition { Width = gridLengthStar };
            ColumnDefinition_2 = new ColumnDefinition { Width = gridLengthStar };

        }
        private void SetRowDefinitions()
        {
            KcvMainWindowControlHelper.Current.Grid_Content.RowDefinitions.Clear();
            KcvMainWindowControlHelper.Current.Grid_Content.RowDefinitions.Add(RowDefinition_1);
            KcvMainWindowControlHelper.Current.Grid_Content.RowDefinitions.Add(RowDefinition_2);
        }

        private void SetColumnDefinitions()
        {
            KcvMainWindowControlHelper.Current.Grid_Content.ColumnDefinitions.Clear();
            KcvMainWindowControlHelper.Current.Grid_Content.ColumnDefinitions.Add(ColumnDefinition_1);
            KcvMainWindowControlHelper.Current.Grid_Content.ColumnDefinitions.Add(ColumnDefinition_2);
        }
        public void StartLayout()
        {
            KcvMainWindowControlHelper.Current.KanColleHost.Visibility = Visibility.Collapsed;
            KcvMainWindowControlHelper.Current.ContentControl_ToolControl.Visibility = Visibility.Collapsed;
        }
        public void EndLayout()
        {
            KcvMainWindowControlHelper.Current.KanColleHost.Visibility = Visibility.Visible;
            KcvMainWindowControlHelper.Current.ContentControl_ToolControl.Visibility = Visibility.Visible;
        }

        #endregion

        #endregion

    }
}
