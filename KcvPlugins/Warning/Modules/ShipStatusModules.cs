using AMing.Plugins.Core.Modules;
using Grabacr07.KanColleWrapper.Models;
using Grabacr07.KanColleWrapper;
using System.Collections.Generic;
using System.Linq;
using AMing.Plugins.Core.Extensions;
using Livet.Behaviors;

namespace AMing.Warning.Modules
{
    public class ShipStatusModules : ModulesBase
    {
        #region Current

        private static ShipStatusModules _current = new ShipStatusModules();

        public static ShipStatusModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        #region member

        private readonly ViewModels.ShipStatusViewModel shipStatusViewModel = new ViewModels.ShipStatusViewModel();

        Dictionary<int, List<Ship>> FleetsDic = new Dictionary<int, List<Ship>>();

        public Views.StatusWindow StatusWindow { get; set; }

        //void ShipStatusViewModel_ShipsChange(object sender, Fleet e)
        //{
        //    ChackHP(e);
        //}


        //private void ChackHP(Fleet fleet)
        //{
        //    if (fleet.Ships != null)
        //    {
        //        if (!FleetsDic.ContainsKey(fleet.Id))
        //        {
        //            FleetsDic.Add(fleet.Id, new List<Ship>());
        //        }
        //        var ships = FleetsDic[fleet.Id];
        //        ships.Clear();
        //        foreach (var item in fleet.Ships)
        //        {
        //            var status = item.HP.ShipStatus();
        //            if (status != Plugins.Core.Enums.ShipStatus.Normal)
        //            {
        //                ships.Add(item);
        //            }
        //        }
        //    }

        //    var txt = FleetsDic.Select(x => string.Format("id:{0}\n{1}", x.Key, x.Value.Select(s => string.Format("name:{0}\thp:{1}/{2}\tstatus:{3}",
        //        s.Info.Name,
        //        s.HP.Current,
        //        s.HP.Maximum,
        //        s.HP.ShipStatus())).ToString("\n"))).ToString("\n");
        //}


        #endregion

        #region method

        #endregion

        public override void Initialize()
        {
            base.Initialize();
            this.StatusWindow = new Views.StatusWindow();
            this.StatusWindow.Show();

            //shipStatusViewModel.ShipsChange += ShipStatusViewModel_ShipsChange;
            shipStatusViewModel.ShipsChange += (sender, e) => this.StatusWindow.UpdateFleet(e);

            InitPublicModules();

            shipStatusViewModel.Listener();//开始监听
        }

        #region PublicModules

        private void InitPublicModules()
        {
            //隐藏全部窗体
            //MessagerModules.Current.Register<string>(this, "TestModules", Test);
        }
        private readonly MethodBinder binder = new MethodBinder();

        //void Test(string msg)
        //{
        //    AMing.Plugins.Core.Helper.MessageBoxDialog.Show("Warning:" + msg);
        //}


        #endregion


        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
