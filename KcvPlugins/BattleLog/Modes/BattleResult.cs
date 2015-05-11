using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models.Raw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMing.Plugins.Core.Extensions;
using Grabacr07.KanColleWrapper.Models;

namespace AMing.Logger.Modes
{
    public class BattleResult : IResult
    {
        public string Id { get; set; }

        public string AdmiralId { get; set; }

        public string QuestName { get; set; }

        public int QuestLevel { get; set; }

        public string DeckName { get; set; }

        public string WinRank { get; set; }

        public int Mvp { get; set; }
        public int MvpCombined { get; set; }

        public int GetExp { get; set; }

        public int GetBaseExp { get; set; }
        /// <summary>
        /// 提升的等级
        /// </summary>
        public int[] LvUpShips { get; set; }
        public int[] LvUpShipsCombined { get; set; }

        public SimpleShip GetShip { get; set; }

        public SimpleShip[] Fleet { get; set; }
        public SimpleShip[] FleetCombined { get; set; }

        public bool IsFirstBattle { get; set; }

        public DateTime CreateDate { get; set; }

        public int FleetType { get; set; }

        public BattleResult() { }

        public BattleResult(KanColleClient kanColleClient, kcsapi_battleresult br)
        {
            this.FleetType = (int)Enums.BattleType.Normal;
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

            this.AdmiralId = kanColleClient.Homeport.Admiral.MemberId;

            List<SimpleShip> fleet = new List<SimpleShip>();
            kanColleClient.Homeport.Organization.Fleets.Where(f =>
                f.Value.State.Situation.HasFlag(FleetSituation.Sortie)).ForEach(item =>

                item.Value.Ships.ForEach(s => fleet.Add(new SimpleShip(s)))
            );
            this.Fleet = fleet.ToArray();

            this.IsFirstBattle = false;
            this.CreateDate = DateTime.Now;
            this.Id = Guid.NewGuid().ToString();
        }

        public BattleResult(KanColleClient kanColleClient, kcsapi_combined_battle_battleresult br)
        {
            this.FleetType = (int)Enums.BattleType.Combined;
            this.QuestName = br.api_quest_name;
            this.QuestLevel = br.api_quest_level;
            if (br.api_enemy_info != null)
                this.DeckName = br.api_enemy_info.api_deck_name;
            this.WinRank = br.api_win_rank;
            this.GetExp = br.api_get_exp;
            this.GetBaseExp = br.api_get_base_exp;
            if (br.api_get_ship != null)
                this.GetShip = new SimpleShip(br.api_get_ship);

            this.AdmiralId = kanColleClient.Homeport.Admiral.MemberId;


            this.Mvp = br.api_mvp;
            this.MvpCombined = br.api_mvp_combined;

            this.LvUpShips = br.api_get_exp_lvup.Select(x => Math.Max(x.Length - 2, 0)).ToArray();
            this.LvUpShipsCombined = br.api_get_exp_lvup_combined.Select(x => Math.Max(x.Length - 2, 0)).ToArray();

            List<SimpleShip> fleet = new List<SimpleShip>();
            //既然是联合舰队肯定一二队都出击
            kanColleClient.Homeport.Organization.Fleets[1].Ships.ForEach(s => fleet.Add(new SimpleShip(s)));
            this.Fleet = fleet.ToArray();

            fleet.Clear();
            kanColleClient.Homeport.Organization.Fleets[2].Ships.ForEach(s => fleet.Add(new SimpleShip(s)));
            this.FleetCombined = fleet.ToArray();


            this.IsFirstBattle = false;
            this.CreateDate = DateTime.Now;
            this.Id = Guid.NewGuid().ToString();
        }


        #region method


        /// <summary>
        /// 当前数据是否还没设置战斗之后的HP
        /// </summary>
        /// <returns></returns>
        public bool IsSetAfterHP()
        {
            var result = true;
            if (this.Fleet != null)
            {
                foreach (var item in this.Fleet)
                {
                    if (item.HP_After == 0)
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }


        private IEnumerable<Ship> GetSortieFleet(KanColleClient kanColleClient)
        {
            return kanColleClient.Homeport.Organization.Fleets.Where(f =>
                    f.Value.State.Situation.HasFlag(FleetSituation.Sortie)).SelectMany(f => f.Value.Ships);
        }

        /// <summary>
        /// 设置战斗之后的HP
        /// </summary>
        /// <param name="kanColleClient"></param>
        /// <returns></returns>
        public bool SetFleetAfterHP(KanColleClient kanColleClient)
        {
            var result = false;
            if (this.AdmiralId == kanColleClient.Homeport.Admiral.MemberId)
            {
                var ships = GetSortieFleet(kanColleClient);
                if (ships.Count() == this.Fleet.Count())
                {
                    result = true;
                    ships.ForEach((item, i) =>
                    {
                        if (!this.Fleet[i].SetAfterHP(item))
                        {
                            result = false;
                        }
                    });
                }
            }

            return result;
        }
        #endregion
    }
}
