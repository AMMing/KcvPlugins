using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.ExpeditionEx.Data
{
    public class Group : JsonDataBase<List<Model.ShipTypeGroup>>
    {
        protected override string FileName { get { return "groups.json"; } }

        private static Group _current = new Group();

        public static Group Current
        {
            get { return _current; }
            set { _current = value; }
        }


        public Model.ShipTypeGroup Get(int id)
        {
            if (this.Data == null) return null;

            return this.Data.FirstOrDefault(x => x.ID == id);
        }
    }
}
