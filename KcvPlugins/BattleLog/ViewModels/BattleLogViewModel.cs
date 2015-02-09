using Grabacr07.KanColleViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMing.Plugins.Core.Extensions;
using AMing.Logger.ViewModels.Sortable;

namespace AMing.Logger.ViewModels
{
    public class BattleLogViewModel : WindowViewModel
    {
        #region BattleList

        private IList<Item.BattleResultViewModel> _battleList;

        public IList<Item.BattleResultViewModel> BattleList
        {
            get { return _battleList; }
            set
            {
                if (this._battleList != value)
                {
                    this._battleList = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region IsReloading

        private bool _isReloading;

        public bool IsReloading
        {
            get { return _isReloading; }
            set
            {
                if (this._isReloading != value)
                {
                    this._isReloading = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region IsOpenSettings

        private bool _IsOpenSettings;

        public bool IsOpenSettings
        {
            get { return this._IsOpenSettings; }
            set
            {
                if (this._IsOpenSettings != value)
                {
                    this._IsOpenSettings = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region member


        private IList<Modes.BattleResult> allBattleResult = new List<Modes.BattleResult>();
        private IList<Modes.SimpleAdmiral> allAdmiral = new List<Modes.SimpleAdmiral>();

        private DateTime lastBattle = DateTime.MinValue;


        public BattleSortWorker SortWorker { get; private set; }

        public GetShipFilter GetShipFilter { get; private set; }

        public WinRankFilter WinRankFilter { get; private set; }

        public DateFilter DateFilter { get; private set; }

        #endregion

        public BattleLogViewModel()
        {
            this.IsOpenSettings = true;
            this.Title = "战斗记录查看";
            this.SortWorker = new BattleSortWorker();
            this.SortWorker.SetTarget(Enums.BattleSortTarget.Date, true);

            this.GetShipFilter = new GetShipFilter(this.Update);
            this.WinRankFilter = new WinRankFilter(this.Update);
            this.DateFilter = new DateFilter(this.Update);
        }
        public void GetFileData()
        {
            Helper.BattleLogsHelper.Current.GetInfo(out this.allBattleResult, out  this.allAdmiral, out  this.lastBattle);
        }
        public void Update()
        {
            this.UpdateCore();
        }
        public void Update(Enums.BattleSortTarget sortTarget)
        {
            this.SortWorker.SetTarget(sortTarget, false);
            this.Update();
        }
        public void UpdateReverse(Enums.BattleSortTarget sortTarget)
        {
            this.SortWorker.SetTarget(sortTarget, true);
            this.Update();
        }

        private void UpdateCore()
        {
            this.IsReloading = true;

            var allListViewModel = this.allBattleResult.Select(x => new Item.BattleResultViewModel(x)).ToList();

            var list = allListViewModel
                .Where(this.GetShipFilter.Predicate)
                .Where(this.WinRankFilter.Predicate)
                .Where(this.DateFilter.Predicate);

            this.BattleList = this.SortWorker.Sort(list).ToList();

            this.IsReloading = false;
        }

        public override void Initialize()
        {
            base.Initialize();
            this.GetFileData();
            this.Update();
        }
    }
}
