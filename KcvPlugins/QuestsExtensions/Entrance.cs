using Grabacr07.KanColleViewer.Composition;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using kcv = Grabacr07.KanColleViewer;

namespace AMing.QuestsExtensions
{
    [Export(typeof(IToolPlugin))]
    [ExportMetadata("Title", "QuestsExtensions")]
    [ExportMetadata("Description", "KCV Quests Extensions")]
    [ExportMetadata("Version", "1.0")]
    [ExportMetadata("Author", "@AMing")]
    public class Entrance : IToolPlugin
    {
        private readonly ViewModels.QuestsViewModelEx questsViewModelEx = new ViewModels.QuestsViewModelEx();
        public string ToolName
        {
            get { return TextResource.Plugin_ToolName; }
        }

        public object GetToolView()
        {
            questsViewModelEx.Initialize();
            return new Views.QuestControl { DataContext = this.questsViewModelEx };
        }

        public object GetSettingsView()
        {
            return null;
        }

        public Entrance()
        {
            Init();
        }

        ~Entrance()
        {
            Exit();
        }
        private void Init()
        {
            Modules.InitModules.Current.Initialize();
            Data.Settings.FirstLoad();
            Data.LocalQuestsSettings.Load();
#if DEBUG
            //Data.LocalQuests LocalQuests = new Data.LocalQuests("zh-cn");
            //LocalQuests.Load();
            //LocalQuests.Save();
#endif
        }

        private void Exit()
        {
            //Data.Settings.Current.Save();
#if DEBUG
            //Data.LocalQuestsSettings.Current.Save();
#endif
        }
    }
}
