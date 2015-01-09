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
        private Data.LocalQuestsSettings settings = null;
        public void Init()
        {
            var settings = Data.LocalQuestsSettings.Current;
            if (!settings.IsLoad)
            {
                return;
            }
            QuestsResource = new Dictionary<string, Data.LocalQuests>();
            foreach (var item in settings.QuestsResourceList)
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
            var key = QuestsResource.ContainsKey(Data.Settings.Current.CurrentShowLocalQuests) ? Data.Settings.Current.CurrentShowLocalQuests : settings.Default;//如果查找不到就使用默认的key
            if (QuestsResource.ContainsKey(key))
            {
                return QuestsResource[key];
            }

            return null;
        }


        public ViewModels.QuestViewModelEx Replace(ViewModels.QuestViewModelEx quest_vm)
        {
            if (settings.IsLoad)
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
