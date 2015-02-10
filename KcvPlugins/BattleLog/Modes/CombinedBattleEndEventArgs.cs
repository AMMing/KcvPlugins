using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models.Raw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Logger.Modes
{
    public class CombinedBattleEndEventArgs : EventArgs
    {
        public KanColleClient KanColleClient { get; set; }
        public kcsapi_combined_battle_battleresult BattleResult { get; set; }
        public bool IsFirstBattle { get; set; }

    }
}
