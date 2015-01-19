using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Grabacr07.KanColleViewer.Composition;
using System.Windows;
using System.Media;
using System.IO;
using System.Windows.Media;

namespace WindowsNotifierForWin7
{
    [Export(typeof(INotifier))]
    [ExportMetadata("Title", "Sound Notifier")]
    [ExportMetadata("Description", "声音提示")]
    [ExportMetadata("Version", "1.1")]
    [ExportMetadata("Author", "@AMing")]
    public class WindowsNotifier : INotifier
    {
        private static readonly string soundFilePath = System.IO.Path.Combine(
            Environment.CurrentDirectory,
            "Plugins",
            "sounds",
            "notify.{0}");

        public void Dispose()
        {
        }

        public void Initialize()
        {
        }
        MediaPlayer mediaPlayer;
        public void Show(NotifyType type, string header, string body, Action activated, Action<Exception> failed = null)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    var path = string.Format(soundFilePath, "wav");
                    if (!System.IO.File.Exists(path))
                    {
                        path = string.Format(soundFilePath, "mp3");
                    }
                    if (!System.IO.File.Exists(path))
                    {
                        throw new System.IO.FileNotFoundException();
                    }
                    if (mediaPlayer == null)
                    {
                        mediaPlayer = new MediaPlayer { Volume = 1 };
                    }
                    mediaPlayer.Open(new Uri(path, UriKind.Absolute));
                    mediaPlayer.Play();
                }
                catch (Exception ex)
                {
                    if (failed != null)
                        failed(ex);
                }
            }));
        }

        public object GetSettingsView()
        {
            return null;
        }
    }
}