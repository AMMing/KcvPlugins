using AMing.SettingsExtensions.Views;
using Grabacr07.KanColleViewer.Views.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        public ContainerWindow ContainerWindow { get; set; }
        public ShowMainInfoViewButton ShowMainInfoViewButton { get; set; }

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

        /// <summary>
        /// 拆分窗体
        /// </summary>
        /// <returns></returns>
        public bool SplitWindow()
        {
            if (!isInit) return false;
            try
            {
                #region ContainerWindow

                this.Grid_Content.Children.Remove(this.ContentControl_ToolControl);

                this.ContainerWindow = new Views.ContainerWindow
                {
                    DataContext = Application.Current.MainWindow.DataContext,
                    WindowContent = this.ContentControl_ToolControl
                };
                this.ContainerWindow.ShowHide += (sender, args) =>
                {
                    this.ShowMainInfoViewButton.BtnIsEnabled = !args;
                };
                this.ContainerWindow.Show();

                #endregion

                this.ShowMainInfoViewButton = new ShowMainInfoViewButton { BtnIsEnabled = false };
                this.StackPanel_WindowCaptionBar.Children.Insert(0, this.ShowMainInfoViewButton);
                this.ShowMainInfoViewButton.Click += (sender, args) =>
                {
                    if (this.ContainerWindow != null)
                    {
                        this.ContainerWindow.OnShowHide(true);
                    }
                }; 

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
                this.ContainerWindow.IsClose = true;
                this.ContainerWindow.Close();
                this.StackPanel_WindowCaptionBar.Children.Remove(this.ShowMainInfoViewButton);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        #endregion
    }
}
