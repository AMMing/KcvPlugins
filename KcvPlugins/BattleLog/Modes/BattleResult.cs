using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models.Raw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMing.Plugins.Core.Extensions;

namespace AMing.Logger.Modes
{
    public class BattleResult
    {
        public Guid Id { get; set; }

        public string AdmiralId { get; set; }

        public string QuestName { get; set; }

        public int QuestLevel { get; set; }

        public string DeckName { get; set; }

        public string WinRank { get; set; }

        public int Mvp { get; set; }

        public int GetExp { get; set; }

        public int GetBaseExp { get; set; }
        /// <summary>
        /// 提升的等级
        /// </summary>
        public int[] LvUpShips { get; set; }

        public SimpleShip GetShip { get; set; }

        public SimpleShip[] Fleet { get; set; }

        public bool IsFirstBattle { get; set; }

        public DateTime CreateDate { get; set; }


        public BattleResult() { }

        public BattleResult(KanColleClient kanColleClient, kcsapi_battleresult br)
        {
            this.QuestName = br.api_quest_name;
            this.QuestLevel = br.api_quest_level;
            if (br.api_enemy_info != null)
                this.DeckName = br.api_enemy_info.api_deck_name;
            this.WinRank = br.api_win_rank;
            this.Mvp = br.api_mvp;
            this.GetExp = br.api_get_exp;
            this.GetBaseExp = br.api_get_base_exp;
            if (br.api_get_ship != null)
                this.GetShip = new SimpleShip(br.api_get_ship);
            this.LvUpShips = br.api_get_exp_lvup.Select(x => Math.Max(x.Length - 2, 0)).ToArray();


            List<SimpleShip> fleet = new List<SimpleShip>();
            this.AdmiralId = kanColleClient.Homeport.Admiral.MemberId;
            kanColleClient.Homeport.Organization.Fleets.Where(f =>
                f.Value.State == Grabacr07.KanColleWrapper.Models.FleetState.Sortie).ForEach(item =>

                item.Value.Ships.ForEach(s => fleet.Add(new SimpleShip(s)))
            );

            this.Fleet = fleet.ToArray();

            this.IsFirstBattle = false;
            this.CreateDate = DateTime.Now;
            this.Id = Guid.NewGuid();
        }
    }
}
