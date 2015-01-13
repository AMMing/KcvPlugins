using Grabacr07.KanColleViewer.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KcvPlugins
{
    /// <summary>
    /// KcvSimulationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class KcvSimulationWindow
    {
        public KcvSimulationWindow()
        {
            this.AllowsTransparency = true;
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.Topmost = true;
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            App.PluginList.ForEach(pugin => AddPlugin(pugin));
        }




        private void AddPlugin(IToolPlugin plugin)
        {
            var tabitem = new TabItem
            {
                Header = plugin.ToolName,
                Content = plugin.GetToolView()
            };

            tabControl.Items.Add(tabitem);
        }
    }
}



