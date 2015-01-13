using Grabacr07.KanColleViewer.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AMing.SettingsExtensions.Views
{
    /// <summary>
    /// ContainerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ContainerWindow
    {
        public ContainerWindow()
        {
            InitializeComponent();
            IsClose = false;
            Application.Current.MainWindow.Closed += (sender, args) =>
            {
                this.IsClose = true;
                this.Close();
            };
            this.Closing += (sender, args) =>
            {
                if (!this.IsClose)
                {
                    args.Cancel = true;
                    OnShowHide(false);
                }
            };
        }

        public bool IsClose { get; set; }
        public object WindowContent
        {
            get { return contentControl.Content; }
            set { contentControl.Content = value; }
        }

        public event EventHandler<bool> ShowHide;
        public void OnShowHide(bool isshow)
        {
            if (isshow)
            {
                this.Show();
            }
            else
            {
                this.Hide();
            }
            if (ShowHide != null)
            {
                ShowHide(this, isshow);
            }
        }

    }
}
