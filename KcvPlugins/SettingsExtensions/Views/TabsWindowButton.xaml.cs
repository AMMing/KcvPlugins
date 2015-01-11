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

namespace AMing.SettingsExtensions.Views
{
    /// <summary>
    /// ShowMainInfoViewButton.xaml 的交互逻辑
    /// </summary>
    public partial class TabsWindowButton : UserControl
    {
        public TabsWindowButton()
        {
            InitializeComponent();
            this.Loaded += ShowMainInfoViewButton_Loaded;
        }

        void ShowMainInfoViewButton_Loaded(object sender, RoutedEventArgs e)
        {
            this.btn_tool.Click += btn_tool_Click;
            this.btn_game.Click += btn_game_Click;
        }

        void btn_game_Click(object sender, RoutedEventArgs e)
        {
            if (GameVisibility != null)
                GameVisibility(this, Visibility.Collapsed);
            if (ToolVisibility != null)
                ToolVisibility(this, Visibility.Visible);

            this.btn_game.Visibility = System.Windows.Visibility.Collapsed;
            this.btn_tool.Visibility = System.Windows.Visibility.Visible;
        }

        void btn_tool_Click(object sender, RoutedEventArgs e)
        {
            if (GameVisibility != null)
                GameVisibility(this, Visibility.Visible);
            if (ToolVisibility != null)
                ToolVisibility(this, Visibility.Collapsed);

            this.btn_game.Visibility = System.Windows.Visibility.Visible;
            this.btn_tool.Visibility = System.Windows.Visibility.Collapsed;
        }

        public event EventHandler<Visibility> GameVisibility;

        public event EventHandler<Visibility> ToolVisibility;

        public void TriggerClick()
        {
            if (this.btn_game.Visibility == System.Windows.Visibility.Visible)
            {
                btn_game_Click(null, null);
            }
            else
            {
                btn_tool_Click(null, null);
            }
        }
    }
}
