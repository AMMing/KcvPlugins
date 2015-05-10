using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMing.ExpeditionEx.Extension;
using Grabacr07.KanColleWrapper.Models;

namespace AMing.ExpeditionEx.Model
{
    public class ExpeditionShipTypesClaim : Claim
    {
        public List<GroupClaim> Groups { get; set; }

        public override string ErrorMessage
        {
            get
            {
                if (this.Groups == null) return null;

                return string.Format("远征需要的舰种\n{0}",
                    string.Join("\n", this.Groups.Select(x => x.ErrorMessage)));
            }
        }

        public ExpeditionShipTypesClaim(List<Model.ExpeditionShipTypes> list)
        {
            this.Groups = new List<GroupClaim>();
            list.ForEach(x => this.Groups.Add(new GroupClaim(x)));
        }
        public void Check(IEnumerable<Ship> ships)
        {
            if (ships == null || this.Groups == null) return;

            this.Groups.ForEach(x => x.Check(ships));
        }

        protected override bool CheckIsAccord()
        {
            return this.Groups.ClaimsIsAccord();
        }
    }
}
