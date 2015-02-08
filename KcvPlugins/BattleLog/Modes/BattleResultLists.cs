using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Logger.Modes
{
    public class BattleResultList : ILogsList<BattleResult>
    {
        public DateTime UpdateDate { get; set; }

        public SimpleAdmiral[] AdmiralList { get; set; }

        public BattleResult[] List { get; set; }

        public BattleResultList() { }
    }
}
