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
        public BattleResult ResultData { get; set; }

        public List<SimpleShipViewModel> Fleet { get; set; }

        public SimpleShipViewModel Mvp { get; set; }


        public SimpleShipViewModel Flagship { get; set; }

        public string GetShipName { get; set; }

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

            if (this.Fleet.Count > this.ResultData.Mvp)
            {
                this.Mvp = this.Fleet[this.ResultData.Mvp];
            }
            if (this.ResultData.GetShip != null)
            {
                this.GetShipName = this.ResultData.GetShip.Name;
            }

            this.Flagship = this.Fleet[0];
        }
    }
}
