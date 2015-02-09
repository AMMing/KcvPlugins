using AMing.Logger.Enums;
using AMing.Logger.ViewModels.Item;
using Grabacr07.Desktop.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMing.Plugins.Core.Extensions;

namespace AMing.Logger.ViewModels.Sortable
{

    public class BattleSortWorker
    {
        private readonly List<SortableColumnViewModel> sortableColumns;
        private readonly NoneColumnViewModel noneColumn = new NoneColumnViewModel();
        private SortableColumnViewModel currentSortTarget;

        public DateColumnViewModel DateColumn { get; private set; }
        public MvpColumnViewModel MvpColumn { get; private set; }
        public FlagshipColumnViewModel FlagshipColumn { get; private set; }
        public GetShipColumnViewModel GetShipColumn { get; private set; }
        public WinRankColumnViewModel WinRankColumn { get; private set; }
        public QuestNameColumnViewModel QuestNameColumn { get; private set; }

        public BattleSortWorker()
        {
            this.DateColumn = new DateColumnViewModel();
            this.MvpColumn = new MvpColumnViewModel();
            this.FlagshipColumn = new FlagshipColumnViewModel();
            this.GetShipColumn = new GetShipColumnViewModel();
            this.WinRankColumn = new WinRankColumnViewModel();
            this.QuestNameColumn = new QuestNameColumnViewModel();

            this.sortableColumns = new List<SortableColumnViewModel>
			{
				this.noneColumn,
				this.DateColumn,
				this.MvpColumn,
				this.FlagshipColumn,
				this.GetShipColumn,
				this.WinRankColumn,
				this.QuestNameColumn
			};

            this.currentSortTarget = this.noneColumn;
        }

        public void SetTarget(BattleSortTarget sortTarget, bool reverse)
        {
            var target = this.sortableColumns.FirstOrDefault(x => x.Target == sortTarget);
            if (target == null) return;

            if (reverse)
            {
                switch (target.Direction)
                {
                    case SortDirection.None:
                        target.Direction = SortDirection.Descending;
                        break;
                    case SortDirection.Descending:
                        target.Direction = SortDirection.Ascending;
                        break;
                    case SortDirection.Ascending:
                        target = this.noneColumn;
                        break;
                }
            }
            else
            {
                switch (target.Direction)
                {
                    case SortDirection.None:
                        target.Direction = SortDirection.Ascending;
                        break;
                    case SortDirection.Ascending:
                        target.Direction = SortDirection.Descending;
                        break;
                    case SortDirection.Descending:
                        target = this.noneColumn;
                        break;
                }
            }

            this.currentSortTarget = target;
            this.sortableColumns.Where(x => x.Target != target.Target).ForEach(x => x.Direction = SortDirection.None);
        }

        public IEnumerable<BattleResultViewModel> Sort(IEnumerable<BattleResultViewModel> shipList)
        {
            return this.currentSortTarget.Sort(shipList);
        }
    }
}
