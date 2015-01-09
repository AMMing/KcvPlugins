using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMing.QuestsExtensions.Extensions;

namespace AMing.QuestsExtensions.Helper
{
    public class QuestHelper
    {
        public Dictionary<string, Data.LocalQuests> QuestsResource { get; set; }
        private Data.LocalQuestsSettings questsSettings = null;
        private Data.Settings settings = null;
        public void Init()
        {
            questsSettings = Data.LocalQuestsSettings.Current;
            settings = Data.Settings.Current;
            if (!questsSettings.IsLoad)
            {
                return;
            }
            QuestsResource = new Dictionary<string, Data.LocalQuests>();
            foreach (var item in questsSettings.QuestsResourceList)
            {
                var localQuests = new Data.LocalQuests(item);
                if (localQuests.XmlExist)
                {
                    if (localQuests.Load())
                    {
                        QuestsResource.Add(item, localQuests);
                    }
                }
            }
        }

        private Data.LocalQuests GetLocalQuests()
        {
            settings.CurrentShowLocalQuests = settings.CurrentShowLocalQuests ?? questsSettings.Default;
            var key = QuestsResource.ContainsKey(settings.CurrentShowLocalQuests) ? settings.CurrentShowLocalQuests : questsSettings.Default;//如果查找不到就使用默认的key
            if (QuestsResource.ContainsKey(key))
            {
                return QuestsResource[key];
            }

            return null;
        }


        public ViewModels.QuestViewModelEx Replace(ViewModels.QuestViewModelEx quest_vm)
        {
            if (questsSettings.IsLoad)
            {
                var localQuests = GetLocalQuests();
                if (localQuests != null)
                {
                    if (localQuests.QuestsResourceDictionary.ContainsKey(quest_vm.Id))
                    {
                        var quest = localQuests.QuestsResourceDictionary[quest_vm.Id];

                        return quest.ToViewModels();
                    }
                }
            }

            return quest_vm;
        }
    }
}
