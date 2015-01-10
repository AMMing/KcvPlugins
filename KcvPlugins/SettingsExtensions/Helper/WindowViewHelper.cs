using AMing.SettingsExtensions.Views;
using Grabacr07.KanColleViewer.Views.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AMing.SettingsExtensions.Extensions;

namespace AMing.SettingsExtensions.Helper
{
    public class WindowViewHelper
    {
        #region member

        public Grid Grid_Layout { get; set; }
        public Grid Grid_WindowCaptionBar { get; set; }
        public Grid Grid_Content { get; set; }
        public StackPanel StackPanel_WindowCaptionBar { get; set; }
        public KanColleHost KanColleHost { get; set; }
        public ContentControl ContentControl_ToolControl { get; set; }

        bool isInit = false;

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
        /// <summary>
        /// 获得KcvMainWindow上的指定控件
        /// </summary>
        public void GetMainWindowControls()
        {
            try
            {
                UIHelper.GetControl<Grid>(Application.Current.MainWindow.Content, grid_layout =>
                {
                    this.Grid_Layout = grid_layout;
                    var border_WindowCaptionBar = this.Grid_Layout.Children.OfType<Border>().First();
                    UIHelper.GetControl<Grid>(border_WindowCaptionBar.Child, grid_caption =>
                    {
                        this.Grid_WindowCaptionBar = grid_caption;
                        this.StackPanel_WindowCaptionBar = this.Grid_WindowCaptionBar.Children.OfType<StackPanel>().First();
                    });
                    this.Grid_Content = this.Grid_Layout.Children.OfType<Grid>().First();
                    this.KanColleHost = this.Grid_Content.Children.OfType<KanColleHost>().First();
                    this.ContentControl_ToolControl = this.Grid_Content.Children.OfType<ContentControl>().First();
                    isInit = true;
                });
            }
            catch (Exception)
            {
                isInit = false;
            }
        }

        #region Split

        /// <summary>
        /// 拆分窗体
        /// </summary>
        /// <returns></returns>
        public bool SplitWindow()
        {
            if (!isInit) return false;
            try
            {
                StartLayout();
                #region ContainerWindow

                this.Grid_Content.Children.Remove(this.ContentControl_ToolControl);

                this.ContainerWindow.WindowContent = this.ContentControl_ToolControl;
                this.ContainerWindow.Show();

                #endregion

                this.SplitWindowButton = new SplitWindowButton { BtnIsEnabled = false };
                this.StackPanel_WindowCaptionBar.Children.Insert(0, this.SplitWindowButton);
                this.SplitWindowButton.Click += (sender, args) =>
                {
                    if (this.ContainerWindow != null)
                    {
                        this.ContainerWindow.OnShowHide(true);
                    }
                };

                this.Grid_Content.RowDefinitions.Clear();
                this.Grid_Content.ColumnDefinitions.Clear();
                EndLayout();

                this.KanColleHost.SetMiniWindow(this.Grid_WindowCaptionBar.ActualHeight);
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
            if (!isInit) return false;
            try
            {
                this.ContainerWindow.WindowContent = null;
                this.Grid_Content.Children.Add(this.ContentControl_ToolControl);
                this.ContainerWindow.Hide();
                this.StackPanel_WindowCaptionBar.Children.Remove(this.SplitWindowButton);
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

        public bool TabsWindow()
        {
            if (!isInit) return false;
            try
            {
                this.Grid_Content.RowDefinitions.Clear();
                this.Grid_Content.ColumnDefinitions.Clear();

                this.TabsWindowButton = new TabsWindowButton();
                this.StackPanel_WindowCaptionBar.Children.Insert(0, this.TabsWindowButton);
                this.TabsWindowButton.GameVisibility += (sender, args) => this.KanColleHost.Visibility = args;
                this.TabsWindowButton.ToolVisibility += (sender, args) => this.ContentControl_ToolControl.Visibility = args;

                this.KanColleHost.Visibility = Visibility.Visible;
                this.ContentControl_ToolControl.Visibility = Visibility.Collapsed;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ResetTabsWindow()
        {
            if (!isInit) return false;
            try
            {
                this.StackPanel_WindowCaptionBar.Children.Remove(this.TabsWindowButton);

                this.KanColleHost.Visibility = Visibility.Visible;
                this.Grid_Content.Visibility = Visibility.Visible;

                return true;
            }
            catch (Exception)
            {
                return false;
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
            if (!isInit) return false;
            try
            {
                StartLayout();
                SetRowDefinitions();
                this.Grid_Content.ColumnDefinitions.Clear();

                Grid.SetRow(this.KanColleHost, 1);
                Grid.SetRow(this.ContentControl_ToolControl, 0);

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
            if (!isInit) return false;
            try
            {
                StartLayout();
                SetRowDefinitions();
                this.Grid_Content.ColumnDefinitions.Clear();

                Grid.SetRow(this.KanColleHost, 0);
                Grid.SetRow(this.ContentControl_ToolControl, 1);

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
            if (!isInit) return false;
            try
            {
                StartLayout();
                this.ContentControl_ToolControl.Visibility = Visibility.Hidden;
                SetColumnDefinitions();
                this.Grid_Content.RowDefinitions.Clear();

                Grid.SetColumn(this.KanColleHost, 1);
                Grid.SetColumn(this.ContentControl_ToolControl, 0);

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
            if (!isInit) return false;
            try
            {
                StartLayout();
                SetColumnDefinitions();
                this.Grid_Content.RowDefinitions.Clear();

                Grid.SetColumn(this.KanColleHost, 0);
                Grid.SetColumn(this.ContentControl_ToolControl, 1);

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
            this.Grid_Content.RowDefinitions.Clear();
            this.Grid_Content.RowDefinitions.Add(RowDefinition_1);
            this.Grid_Content.RowDefinitions.Add(RowDefinition_2);
        }

        private void SetColumnDefinitions()
        {
            this.Grid_Content.ColumnDefinitions.Clear();
            this.Grid_Content.ColumnDefinitions.Add(ColumnDefinition_1);
            this.Grid_Content.ColumnDefinitions.Add(ColumnDefinition_2);
        }
        public void StartLayout()
        {
            this.KanColleHost.Visibility = Visibility.Collapsed;
            this.ContentControl_ToolControl.Visibility = Visibility.Collapsed;
        }
        public void EndLayout()
        {
            this.KanColleHost.Visibility = Visibility.Visible;
            this.ContentControl_ToolControl.Visibility = Visibility.Visible;
        }

        #endregion

        #endregion

    }
}
