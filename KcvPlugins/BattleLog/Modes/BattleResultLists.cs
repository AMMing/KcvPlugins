using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Logger.Modes
{
    public class BattleResultList
    {
        public DateTime UpdateDate { get; set; }

        public List<SimpleAdmiral> AdmiralList { get; set; }

        public List<BattleResult> List { get; set; }

    }
}
