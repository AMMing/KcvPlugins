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
    public class MissionViewModel : ViewModel
    {
        public int Index { get; set; }
        /// <summary>
        /// 地图名称
        /// </summary>
        public string MapName { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 航行路线
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// 捞到的舰娘
        /// </summary>
        public List<string> GetShips { get; set; }
        /// <summary>
        /// 第一次战斗时间
        /// </summary>
        public DateTime FirstBattleDate { get; set; }
        /// <summary>
        /// 最后一次战斗时间
        /// </summary>
        public DateTime LastBattleDate { get; set; }

        /// <summary>
        /// 每个地址图的详情
        /// </summary>
        public List<BattleResultViewModel> BattleResults { get; set; }

        public MissionViewModel(List<BattleResult> list)
        {
            if(list==null ||list.Count==0)return ;

            this.BattleResults = list.Select(x => new BattleResultViewModel(x)).OrderBy(x => x.ResultData.CreateDate).ToList();
            var first = this.BattleResults.FirstOrDefault();

            this.FirstBattleDate = first.ResultData.CreateDate;
            this.LastBattleDate = this.BattleResults.LastOrDefault().ResultData.CreateDate;
            this.GetShips = this.BattleResults.Where(x => x.ResultData.GetShip != null).Select(x => x.ResultData.GetShip.Name).ToList();

            this.MapName = first.ResultData.QuestName;
            this.Route= this.BattleResults.Select(x=>x.ResultData.DeckName).ToString()
        }
    }
}
