using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models.Raw;
using Livet;

namespace Warning
{
    public class MissionCounter : CounterBase
    {
        public MissionCounter(KanColleProxy proxy)
        {
            proxy.api_req_mission_result
                .TryParse<kcsapi_mission_result>()
                .Where(x => x.IsSuccess)
                .Where(x => x.Data.api_clear_result == 1 || x.Data.api_clear_result == 2)
                .Subscribe(_ => this.Count++);

        }
    }
}
