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
//using AMing.Plugins.Core.Extensions;
using Codeplex.Data;

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

            //var msg = AMing.Plugins.Core.Modules.MessagerModules.Current.ToStringContent();

            //var text = AMing.Plugins.Core.Helper.TextFileHelper.TxtFileRead(@"C:\Program Files\KanColleViewer\Plugins\Logger\Battle\logs_last.json.txt");


            ////var aaa = new AMing.Logger.Modes.BattleResultList();
            ////var json = AMing.Logger.Helper.JsonHelper.Serialize(aaa);
            //var bbb = AMing.Logger.Helper.JsonHelper.Deserialize<AMing.Logger.Modes.BattleResultList>(text);
            //var json = DynamicJson.Serialize(bbb);
            //var aaa = DynamicJson.Parse(json);
            ////var ccc = new AMing.Logger.Modes.BattleResultList(aaa);
            //var asdasd = (BattleResultList22)aaa;

            //AMing.Logger.Helper.BattleLogsHelper.Current.Append(list =>
            //{
            //    return true;
            //});
            //IList<AMing.Logger.Modes.BattleResult> list;
            //IList<AMing.Logger.Modes.SimpleAdmiral> alist;
            //DateTime lastUpdateDate;
            //AMing.Logger.Helper.BattleLogsHelper.Current.GetInfo(out list, out alist, out lastUpdateDate);

            //var item = AMing.Logger.Helper.BattleLogsHelper.Current.GetLastItem();
            //item.Fleet[0].HP_After=123;
            //item.Fleet[1].HP_After = 233;
            //AMing.Logger.Helper.BattleLogsHelper.Current.UpdateItem(item);
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
            //var temp = AMing.Plugins.Core.Helper.KcvMainWindowControlHelper.Current.StatusBar;

            //var grid = this.statusBar.Content as Grid;
            //var content = grid.Children.OfType<ContentControl>().FirstOrDefault();

            //var result = content.Resources.Cast<DictionaryEntry>().Where((item, i) =>
            //    (item.Key is DataTemplateKey) &&
            //    (item.Key as DataTemplateKey).DataType.Equals(typeof(Grabacr07.KanColleViewer.ViewModels.Contents.Fleets.FleetsViewModel))
            //).FirstOrDefault();
        }
    }
}



