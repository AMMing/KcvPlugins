using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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

namespace KcvPlugins
{
    /// <summary>
    /// OpenToastWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OpenToastWindow : Window
    {
        public OpenToastWindow()
        {
            InitializeComponent();
        }
        private static readonly string filePathFormat = System.IO.Path.Combine(
           Environment.CurrentDirectory,
            //"Plugins",
           "sounds",
           "notify.wav");


        MediaPlayer MediaPlayer = new MediaPlayer();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var soundPlayer = new SoundPlayer(fs);
            //soundPlayer.Play();
            MediaPlayer.Open(new Uri(filePathFormat, UriKind.Absolute));
            MediaPlayer.Play();
        }
    }
}
