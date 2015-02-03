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

namespace AMing.SoundNotifier
{
    [Export(typeof(INotifier))]
    [ExportMetadata("Title", "Sound Notifier")]
    [ExportMetadata("Description", "声音提示")]
    [ExportMetadata("Version", "1.2")]
    [ExportMetadata("Author", "@AMing")]
    public class SoundNotifier : INotifier
    {
        Modules.InitModules initModules;
        public void Initialize()
        {
            initModules = new Modules.InitModules();
            initModules.Initialize();
        }

        public void Show(NotifyType type, string header, string body, Action activated, Action<Exception> failed = null)
        {
            Modules.SoundsModules.Current.Notify(failed);
        }

        public object GetSettingsView()
        {
            return null;
        }
        public void Dispose()
        {
        }

    }
}