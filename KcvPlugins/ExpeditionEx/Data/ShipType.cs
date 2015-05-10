using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.ExpeditionEx.Data
{
    public class ShipType : JsonDataBase<List<Model.ShipType>>
    {
        protected override string FileName { get { return "ExpeditionEx.shiptype.json"; } }


        private static ShipType _current = new ShipType();

        public static ShipType Current
        {
            get { return _current; }
            set { _current = value; }
        }
    }
}
