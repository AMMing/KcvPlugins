using AMing.Logger.Enums;
using AMing.Logger.ViewModels.Item;
using Grabacr07.Desktop.Metro.Controls;
using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Logger.ViewModels.Sortable
{

    public abstract class SortableColumnViewModel : ViewModel
    {
        public BattleSortTarget Target { get; private set; }

        #region Direction 変更通知プロパティ

        private SortDirection _Direction = SortDirection.None;

        public SortDirection Direction
        {
            get { return this._Direction; }
            set
            {
                if (this._Direction != value)
                {
                    this._Direction = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        protected SortableColumnViewModel(BattleSortTarget target)
        {
            this.Target = target;
        }

        public abstract IEnumerable<BattleResultViewModel> Sort(IEnumerable<BattleResultViewModel> list);
    }

    public class NoneColumnViewModel : SortableColumnViewModel
    {
        public NoneColumnViewModel() : base(BattleSortTarget.None) { }

        public override IEnumerable<BattleResultViewModel> Sort(IEnumerable<BattleResultViewModel> list)
        {
            return list;
        }
    }

    public class DateColumnViewModel : SortableColumnViewModel
    {
        public DateColumnViewModel() : base(BattleSortTarget.Date) { }

        public override IEnumerable<BattleResultViewModel> Sort(IEnumerable<BattleResultViewModel> list)
        {
            if (this.Direction == SortDirection.Ascending)
            {
                return list.OrderBy(x => x.ResultData.CreateDate);
            }
            if (this.Direction == SortDirection.Descending)
            {
                return list.OrderByDescending(x => x.ResultData.CreateDate);
            }
            return list;
        }
    }

    public class MvpColumnViewModel : SortableColumnViewModel
    {
        public MvpColumnViewModel() : base(BattleSortTarget.Mvp) { }

        public override IEnumerable<BattleResultViewModel> Sort(IEnumerable<BattleResultViewModel> list)
        {
            if (this.Direction == SortDirection.Ascending)
            {
                return list.OrderBy(x => x.Mvp.Id);
            }
            if (this.Direction == SortDirection.Descending)
            {
                return list.OrderByDescending(x => x.Mvp.Id);
            }
            return list;
        }
    }

    public class FlagshipColumnViewModel : SortableColumnViewModel
    {
        public FlagshipColumnViewModel() : base(BattleSortTarget.Flagship) { }

        public override IEnumerable<BattleResultViewModel> Sort(IEnumerable<BattleResultViewModel> list)
        {
            if (this.Direction == SortDirection.Ascending)
            {
                return list.OrderBy(x => x.Flagship.Id);
            }
            if (this.Direction == SortDirection.Descending)
            {
                return list.OrderByDescending(x => x.Flagship.Id);
            }
            return list;
        }
    }
    public class GetShipColumnViewModel : SortableColumnViewModel
    {
        public GetShipColumnViewModel() : base(BattleSortTarget.GetShip) { }

        public override IEnumerable<BattleResultViewModel> Sort(IEnumerable<BattleResultViewModel> list)
        {
            if (this.Direction == SortDirection.Ascending)
            {
                return list.OrderBy(x => x.GetShipName);
            }
            if (this.Direction == SortDirection.Descending)
            {
                return list.OrderByDescending(x => x.GetShipName);
            }
            return list;
        }
    }
    public class WinRankColumnViewModel : SortableColumnViewModel
    {
        public WinRankColumnViewModel() : base(BattleSortTarget.WinRank) { }

        public override IEnumerable<BattleResultViewModel> Sort(IEnumerable<BattleResultViewModel> list)
        {
            if (this.Direction == SortDirection.Ascending)
            {
                return list.OrderBy(x => x.ResultData.WinRank);
            }
            if (this.Direction == SortDirection.Descending)
            {
                return list.OrderByDescending(x => x.ResultData.WinRank);
            }
            return list;
        }
    }
    public class QuestNameColumnViewModel : SortableColumnViewModel
    {
        public QuestNameColumnViewModel() : base(BattleSortTarget.QuestName) { }

        public override IEnumerable<BattleResultViewModel> Sort(IEnumerable<BattleResultViewModel> list)
        {
            if (this.Direction == SortDirection.Ascending)
            {
                return list.OrderBy(x => x.ResultData.QuestName);
            }
            if (this.Direction == SortDirection.Descending)
            {
                return list.OrderByDescending(x => x.ResultData.QuestName);
            }
            return list;
        }
    }
}
