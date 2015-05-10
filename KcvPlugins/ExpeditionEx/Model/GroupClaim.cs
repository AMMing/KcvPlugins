using Grabacr07.KanColleWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMing.Plugins.Core.Extensions;


namespace AMing.ExpeditionEx.Model
{
    public class GroupClaim : Claim
    {
        public int Surplus { get; set; }

        public Model.ShipTypeGroup GroupData { get; set; }

        public override string ErrorMessage
        {
            get
            {
                if (this.GroupData == null) return null;

                return string.Format(
                    this.ErrorMessageFormat,
                    string.Join(",", this.GroupData.ShipTypes.Select(x => x.Name)),
                    this.AtLeast,
                    this.Now);
            }
        }

        public GroupClaim(int gid, int count = 1)
        {
            this.ErrorMessageFormat = "[{0}]需要（{1}艘）当前（{2}艘）";
            this.GroupData = Data.Group.Current.Get(gid);
            if (this.GroupData == null)
            {
                this.IsAccord = true;
            }
            this.AtLeast = count;
        }
        public GroupClaim(Model.ExpeditionShipTypes est) : this(est.GroupId, est.Count) { }

        public void Check(IEnumerable<Ship> ships)
        {
            if (ships == null || this.GroupData == null) return;

            var types = ships.Select(x => x.Info.ShipType.SortNumber);
            var typelist = this.GroupData.ShipTypes.Select(x => x.SortNumber);
            this.Now = types.Where(x => typelist.Contains(x)).Count();

            this.Surplus = this.Now - this.AtLeast;
            if (this.Surplus == 0)
            {
                this.Surplus = 0;
            }
        }
    }
}
