using Grabacr07.KanColleWrapper;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AMing.Plugins.Core.Extensions;
using AMing.ExpeditionEx.Extension;

namespace AMing.ExpeditionEx.Views
{
    /// <summary>
    /// SettingsControl.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var expinfo = Data.Expedition.Current.Data.FirstOrDefault(x => x.Id == 35);
            var fleets = KanColleClient.Current.Homeport.Organization.Fleets[1];

            var result = Helper.CheckFleet.Current.Check(fleets, expinfo);

            AMing.Plugins.Core.GenericMessager.Current.SendToLogs(result.ToStringContentAndType());

            if (!result.Claims.ClaimsIsAccord())
            {
                MessageBox.Show(string.Join("\n", result.Claims.Select(x => x.ErrorMessage)));
            }
            MessageBox.Show(result.Claims.ClaimsIsAccord().ToString());
        }

    }
}
