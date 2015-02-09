using AMing.Logger.ViewModels.Item;
using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Logger.ViewModels.Sortable
{

    public abstract class ShipCatalogFilter : NotificationObject
    {
        private readonly Action action;

        public abstract bool Predicate(BattleResultViewModel ship);

        protected ShipCatalogFilter(Action updateAction)
        {
            this.action = updateAction;
        }

        protected void Update()
        {
            if (this.action != null) this.action();
        }
    }

    public class GetShipFilter : ShipCatalogFilter
    {
        #region Both 変更通知プロパティ

        private bool _Both;

        public bool Both
        {
            get { return this._Both; }
            set
            {
                if (this._Both != value)
                {
                    this._Both = value;
                    this.RaisePropertyChanged();
                    this.Update();
                }
            }
        }

        #endregion

        #region GetShip 変更通知プロパティ

        private bool _GetShip;

        public bool GetShip
        {
            get { return this._GetShip; }
            set
            {
                if (this._GetShip != value)
                {
                    this._Both = false;
                    this._GetShip = value;
                    this.RaisePropertyChanged();
                    this.Update();
                }
            }
        }

        #endregion


        public GetShipFilter(Action updateAction)
            : base(updateAction)
        {
            this._Both = true;
            this._GetShip = false;
        }

        public override bool Predicate(BattleResultViewModel ship)
        {
            if (this.Both) return true;
            if (this.GetShip && string.IsNullOrEmpty(ship.GetShipName)) return false;

            return true;
        }
    }

    public class WinRankFilter : ShipCatalogFilter
    {
        #region Both 変更通知プロパティ

        private bool _Both;

        public bool Both
        {
            get { return this._Both; }
            set
            {
                if (this._Both != value)
                {
                    this._Both = value;
                    this.RaisePropertyChanged();
                    this.Update();
                }
            }
        }

        #endregion

        #region Rank_S 変更通知プロパティ

        private bool _Rank_S;

        public bool Rank_S
        {
            get { return this._Rank_S; }
            set
            {
                if (this._Rank_S != value)
                {
                    this._Both = false;
                    this._Rank_S = value;
                    this.RaisePropertyChanged();
                    this.Update();
                }
            }
        }

        #endregion

        #region Rank_A 変更通知プロパティ

        private bool _Rank_A;

        public bool Rank_A
        {
            get { return this._Rank_A; }
            set
            {
                if (this._Rank_A != value)
                {
                    this._Both = false;
                    this._Rank_A = value;
                    this.RaisePropertyChanged();
                    this.Update();
                }
            }
        }

        #endregion

        #region Rank_B 変更通知プロパティ

        private bool _Rank_B;

        public bool Rank_B
        {
            get { return this._Rank_B; }
            set
            {
                if (this._Rank_B != value)
                {
                    this._Both = false;
                    this._Rank_B = value;
                    this.RaisePropertyChanged();
                    this.Update();
                }
            }
        }

        #endregion

        #region Rank_C 変更通知プロパティ

        private bool _Rank_C;

        public bool Rank_C
        {
            get { return this._Rank_C; }
            set
            {
                if (this._Rank_C != value)
                {
                    this._Both = false;
                    this._Rank_C = value;
                    this.RaisePropertyChanged();
                    this.Update();
                }
            }
        }

        #endregion

        #region Rank_D 変更通知プロパティ

        private bool _Rank_D;

        public bool Rank_D
        {
            get { return this._Rank_D; }
            set
            {
                if (this._Rank_D != value)
                {
                    this._Both = false;
                    this._Rank_D = value;
                    this.RaisePropertyChanged();
                    this.Update();
                }
            }
        }

        #endregion

        public WinRankFilter(Action updateAction)
            : base(updateAction)
        {
            this._Both = true;
            this._Rank_S = true;
            this._Rank_A = true;
            this._Rank_B = true;
            this._Rank_C = true;
            this._Rank_D = true;
        }

        public override bool Predicate(BattleResultViewModel ship)
        {
            if (this.Both) return true;
            if (this.Rank_S && ship.ResultData.WinRank.ToUpper().Contains("S")) return true;
            if (this.Rank_A && ship.ResultData.WinRank.ToUpper().Contains("A")) return true;
            if (this.Rank_B && ship.ResultData.WinRank.ToUpper().Contains("B")) return true;
            if (this.Rank_C && ship.ResultData.WinRank.ToUpper().Contains("C")) return true;
            if (this.Rank_D && ship.ResultData.WinRank.ToUpper().Contains("D")) return true;

            return false;
        }
    }

    public class DateFilter : ShipCatalogFilter
    {
        #region Both 変更通知プロパティ

        private bool _Both;

        public bool Both
        {
            get { return this._Both; }
            set
            {
                if (this._Both != value)
                {
                    this._Both = value;
                    this.RaisePropertyChanged();
                    this.Update();
                }
            }
        }

        #endregion

        #region StartDate 変更通知プロパティ

        private DateTime _StartDate;

        public DateTime StartDate
        {
            get { return this._StartDate; }
            set
            {
                if (this._StartDate != value)
                {
                    this._Both = false;
                    this._StartDate = value;
                    this.RaisePropertyChanged();
                    this.Update();
                }
            }
        }

        #endregion

        #region EndDate 変更通知プロパティ

        private DateTime _EndDate;

        public DateTime EndDate
        {
            get { return this._EndDate; }
            set
            {
                if (this._EndDate != value)
                {
                    this._Both = false;
                    this._EndDate = value;
                    this.RaisePropertyChanged();
                    this.Update();
                }
            }
        }

        #endregion


        public DateFilter(Action updateAction)
            : base(updateAction)
        {
            this._StartDate = DateTime.Today;
            this._EndDate = DateTime.Today.AddDays(1);
        }

        public override bool Predicate(BattleResultViewModel ship)
        {
            if (this.Both) return true;
            if (this.StartDate < ship.ResultData.CreateDate && ship.ResultData.CreateDate < this.EndDate) return true;

            return false;
        }
    }
}
