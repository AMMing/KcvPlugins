using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livet;
using Grabacr07.KanColleViewer.ViewModels.Contents;
using kcvModels = Grabacr07.KanColleWrapper.Models;

namespace AMing.QuestsExtensions.ViewModels
{
    public class QuestViewModelEx : QuestViewModel
    {
        #region Id 変更通知プロパティ

        private int _Id;

        public int Id
        {
            get { return this._Id; }
            set
            {
                if (this._Id != value)
                {
                    this._Id = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion


        public QuestViewModelEx(kcvModels.Quest quest)
            : base(quest)
        {
            if (quest == null)
            {
                this.IsUntaken = true;
            }
            else
            {
                this.Id = quest.Id;
            }

        }
    }
}
