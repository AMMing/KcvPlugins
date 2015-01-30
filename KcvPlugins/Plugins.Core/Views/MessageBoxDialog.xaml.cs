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

namespace AMing.Plugins.Core.Views
{
    /// <summary>
    /// ContainerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBoxDialog
    {
        public MessageBoxDialog()
        {
            InitializeComponent();

            this.btn_ok.Click += (btn_ok_sender, btn_ok_e) =>
            {
                this.DialogResult = true;
                this.Close();
            };

            this.btn_cancel.Click += (btn_cancel_sender, btn_cancel_e) =>
            {
                this.DialogResult = false;
                this.Close();
            };

        }
        public MessageBoxDialog(string msg)
            : this()
        {
            this.Title = string.Empty;
            this.tb_content.Text = msg;
            this.btn_ok.Content = TextResource.Ok;
            this.btn_cancel.Visibility = System.Windows.Visibility.Collapsed;
            this.sp_btns.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
        }

        public MessageBoxDialog(string msg, string caption)
            : this()
        {
            this.Title = caption;
            this.tb_content.Text = msg;
            this.btn_ok.Content = TextResource.Yes;
            this.btn_cancel.Content = TextResource.No;
            this.btn_cancel.Visibility = System.Windows.Visibility.Visible;

            this.btn_cancel.Focus();
        }

    }
}
