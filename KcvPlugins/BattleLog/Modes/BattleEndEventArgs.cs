using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models.Raw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Logger.Modes
{
    public class BattleEndEventArgs : EventArgs
    {
        public KanColleClient KanColleClient { get; set; }
        public kcsapi_battleresult BattleResult { get; set; }
        public bool IsFirstBattle { get; set; }

    }
}
