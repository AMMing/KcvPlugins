using AMing.Logger.Modes;
using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMing.Plugins.Core.Extensions;

namespace AMing.Logger.ViewModels.Item
{
    public class BattleResultViewModel : ViewModel
    {
        public int Index { get; set; }

        public BattleResult ResultData { get; set; }

        public List<SimpleShipViewModel> Fleet { get; set; }

        public List<SimpleShipViewModel> FleetCombined { get; set; }

        public SimpleShipViewModel Mvp { get; set; }

        public SimpleShipViewModel MvpCombined { get; set; }


        public SimpleShipViewModel Flagship { get; set; }

        public SimpleShipViewModel FlagshipCombined { get; set; }


        public string Mvps { get; set; }

        public string Flagships { get; set; }

        public string GetShipName { get; set; }

        public string FleetType { get; set; }

        public BattleResultViewModel(BattleResult br)
        {
            this.ResultData = br;
            this.Fleet = new List<SimpleShipViewModel>();
            int index = 0;
            foreach (var item in this.ResultData.Fleet)
            {
                this.Fleet.Add(new SimpleShipViewModel(item, this.ResultData.LvUpShips.Get(index) ?? 0));
                index++;
            }

            if (this.Fleet.Count >= this.ResultData.Mvp)
            {
                this.Mvp = this.Fleet[this.ResultData.Mvp - 1];
            }
            if (this.ResultData.GetShip != null)
            {
                this.GetShipName = this.ResultData.GetShip.Name;
            }
            this.Flagship = this.Fleet[0];
            this.Flagships = this.Flagship.Name;
            this.Mvps = this.Mvp.Name;

            this.FleetType = "普通舰队";

            if (this.ResultData.FleetType == 1)
            {
                this.FleetType = "联合舰队";

                this.FleetCombined = new List<SimpleShipViewModel>();
                index = 0;
                foreach (var item in this.ResultData.FleetCombined)
                {
                    this.FleetCombined.Add(new SimpleShipViewModel(item, this.ResultData.LvUpShipsCombined.Get(index) ?? 0));
                    index++;
                }
                if (this.ResultData.FleetCombined.Length >= this.ResultData.MvpCombined)
                {
                    this.MvpCombined = this.FleetCombined[this.ResultData.MvpCombined - 1];
                }

                this.FlagshipCombined = this.FleetCombined[0];


                this.Flagships += string.Format(" , {0}", this.FlagshipCombined.Name);
                this.Mvps += string.Format(" , {0}", this.MvpCombined.Name);
            }
        }
    }
}
