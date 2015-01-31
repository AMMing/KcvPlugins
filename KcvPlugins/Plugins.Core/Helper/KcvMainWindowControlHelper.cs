using Grabacr07.KanColleViewer.Views.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AMing.Plugins.Core.Helper
{
    public class KcvMainWindowControlHelper
    {
        #region Current

        private static KcvMainWindowControlHelper _current = new KcvMainWindowControlHelper();

        public static KcvMainWindowControlHelper Current
        {
            get
            {
                if (!_current.IsInit)
                {
                    _current.GetMainWindowControls();
                }
                return _current;
            }
            set { _current = value; }
        }

        #endregion

        #region member

        public Grid Grid_Layout { get; set; }
        public Grid Grid_WindowCaptionBar { get; set; }
        public Grid Grid_Content { get; set; }
        public StackPanel StackPanel_WindowCaptionBar { get; set; }
        public KanColleHost KanColleHost { get; set; }
        public ContentControl ContentControl_ToolControl { get; set; }

        public bool IsInit { get; set; }

        #endregion

        #region method
        /// <summary>
        /// 获得KcvMainWindow上的指定控件
        /// </summary>
        private void GetMainWindowControls()
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
                    this.IsInit = true;
                });
            }
            catch (Exception)
            {
                this.IsInit = false;
            }
        }

        #endregion
    }
}
