using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Grabacr07.KanColleViewer.Composition;
using System.Windows;

namespace AMing.WindowsNotifierEx
{
    [Export(typeof(INotifier))]
    [ExportMetadata("Title", "WindowsNotifier Extensions")]
    [ExportMetadata("Description", "适用不同情况下的信息通知效果")]
    [ExportMetadata("Version", "1.2")]
    [ExportMetadata("Author", "@AMing")]
    public class WindowsNotifier : INotifier
    {
        Modules.InitModules initModules;
        public void Initialize()
        {
            initModules = new Modules.InitModules();
            initModules.Initialize();
        }

        public void Show(NotifyType type, string header, string body, Action activated, Action<Exception> failed = null)
        {
            Modules.NotifierModules.Current.Notify(header, body, activated, failed);
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