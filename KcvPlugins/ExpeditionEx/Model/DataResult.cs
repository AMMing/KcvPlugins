using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.ExpeditionEx.Model
{
    public class DataResult<T>
    {
        public string Id { get; set; }
        public string Version { get; set; }
        public string Guid { get; set; }
        public string UpdateDate { get; set; }
        public string Remark { get; set; }
        public string From { get; set; }
        public T Data { get; set; }
    }
}
