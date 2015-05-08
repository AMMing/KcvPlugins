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

        public GroupClaim(int gid, int count = 1)
        {
            this.GroupData = Data.Group.Current.Get(gid);
            if (this.GroupData == null)
            {
                CheckFunc = () => true;
            }
            this.AtLeast = count;
        }
        public GroupClaim(Model.ExpeditionShipTypes est) : this(est.GroupId, est.Count) { }

        public void Check(IEnumerable<Ship> ships)
        {
            if (ships == null) return;
            AMing.Plugins.Core.GenericMessager.Current.SendToLogs(ships.ToStringContentAndType());

            var types = ships.Select(x => x.Info.ShipType.SortNumber);
            this.Now = this.GroupData.ShipTypes.Select(x => x.SortNumber).Intersect(types).Count();
            this.Surplus = this.Now - this.AtLeast;
            if (this.Surplus == 0)
            {
                this.Surplus = 0;
            }
        }

    }
}
