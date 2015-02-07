using Grabacr07.KanColleWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Logger.Modes
{
    public class SimpleAdmiral
    {
        public string Id { get; set; }

        public string Nickname { get; set; }

        public int Level { get; set; }

        public SimpleAdmiral() { }
        public SimpleAdmiral(KanColleClient kanColleClient)
        {
            this.Id = kanColleClient.Homeport.Admiral.MemberId;
            this.Nickname = kanColleClient.Homeport.Admiral.Nickname;
            this.Level = kanColleClient.Homeport.Admiral.Level;
        }
    }
}
