using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.ExpeditionEx.Data
{
    public class Area : JsonDataBase<List<Model.Area>>
    {
        protected override string FileName { get { return "areas.json"; } }

        private static Area _current = new Area();

        public static Area Current
        {
            get { return _current; }
            set { _current = value; }
        }

    }
}
