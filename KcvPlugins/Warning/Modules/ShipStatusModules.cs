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

        #endregion

        #region method

        #endregion

        public override void Initialize()
        {
            base.Initialize();
            this.StatusWindow = new Views.StatusWindow();
            this.StatusWindow.Show();

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

        #endregion


        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
