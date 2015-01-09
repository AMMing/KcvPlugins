using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kcvModels = Grabacr07.KanColleWrapper.Models;

namespace AMing.QuestsExtensions.Extensions
{
    public static class QuestEx
    {
        public static kcvModels.Raw.kcsapi_quest ToKcvApi(this Models.Quest quest)
        {
            return new kcvModels.Raw.kcsapi_quest
            {
                api_no = quest.Id,
                api_category = quest.Category,
                api_type = quest.Type,
                api_title = quest.Title,
                api_detail = quest.Detail
            };
        }
        public static kcvModels.Quest ToKcvModels(this Models.Quest quest)
        {
            return new kcvModels.Quest(quest.ToKcvApi());
        }
        public static ViewModels.QuestViewModelEx ToViewModels(this Models.Quest quest)
        {
            return new ViewModels.QuestViewModelEx(quest.ToKcvModels());
        }
    }
}
