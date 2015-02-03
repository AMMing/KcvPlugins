using Grabacr07.KanColleViewer.Composition;
using System;
using System.Collections;
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
            //this.AllowsTransparency = true;
            //this.WindowStyle = System.Windows.WindowStyle.None;
            //this.Topmost = true;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var temp = AMing.Plugins.Core.Helper.KcvMainWindowControlHelper.Current.StatusBar;

            var grid = this.statusBar.Content as Grid;
            var content = grid.Children.OfType<ContentControl>().FirstOrDefault();

            var result = content.Resources.Cast<DictionaryEntry>().Where((item, i) =>
                (item.Key is DataTemplateKey) &&
                (item.Key as DataTemplateKey).DataType.Equals(typeof(Grabacr07.KanColleViewer.ViewModels.Contents.Fleets.FleetsViewModel))
            ).FirstOrDefault();

        }
    }
}



