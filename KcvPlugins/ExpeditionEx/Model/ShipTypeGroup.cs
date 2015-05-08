using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.ExpeditionEx.Model
{
    public class ShipTypeGroup
    {
        public int ID { get; set; }

        public string Remark { get; set; }

        public List<ShipType> ShipTypes { get; set; }
    }
}
