using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMing.ExpeditionEx.Extension;

namespace AMing.ExpeditionEx.Data
{
    public class Expedition : JsonDataBase<List<Model.ExpeditionInfo>>
    {
        protected override string FileName { get { return "ExpeditionEx.expeditions.json"; } }

        public Model.DataResult<List<Model.ExpeditionInfoSimple>> DataResult { get; set; }

        protected override List<Model.ExpeditionInfo> Deserialize(string content)
        {
            var result = Plugins.Core.Helper.JsonHelper.Deserialize<Model.DataResult<List<Model.ExpeditionInfoSimple>>>(content);

            return result == null ?
                null :
                result.Data.Select(x => x.ToModel()).ToList();
        }

        protected override string Serialize(object obj)
        {
            if (this.DataResult == null)
            {
                this.DataResult = new Model.DataResult<List<Model.ExpeditionInfoSimple>>
                {
                    Guid = Guid.NewGuid().ToString(),
                    Data = this.Data.Select(x => x.ToSimple()).ToList()
                };
            }

            return Plugins.Core.Helper.JsonHelper.Serialize(this.DataResult);
        }


        private static Expedition _current = new Expedition();

        public static Expedition Current
        {
            get { return _current; }
            set { _current = value; }
        }
    }
}
