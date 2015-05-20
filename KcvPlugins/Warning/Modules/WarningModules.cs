using AMing.Plugins.Core.Modules;
using Grabacr07.KanColleWrapper.Models;
using Grabacr07.KanColleWrapper;
using System.Collections.Generic;
using System.Linq;
using AMing.Plugins.Core.Extensions;
using Livet.Behaviors;
using System;
using AMing.Warning.Data;
using AMing.Plugins.Core;
using AMing.Plugins.Core.Enums;
using System.Windows;

namespace AMing.Warning.Modules
{
    public class WarningModules : ModulesBase
    {
        #region Current

        private static WarningModules _current = new WarningModules();

        public static WarningModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        #region member

        private readonly ViewModels.ShipStatusViewModel shipStatusViewModel = new ViewModels.ShipStatusViewModel();

        Dictionary<int, List<Model.WarningShip>> FleetsDic = new Dictionary<int, List<Model.WarningShip>>();
        Dictionary<int, Model.WarningShip> WarningShips = new Dictionary<int, Model.WarningShip>();

        public Views.StatusWindow StatusWindow { get; set; }



        #endregion

        #region method

        public void EnableWarning()
        {
            this.EnableThemeWarning();
            this.EnableWindows();
            this.Filter();
        }
        public void EnableThemeWarning()
        {
            WarningTheme();
        }
        public void EnableWindows(bool? value = null)
        {
            var enable = Settings.Current.EnableWindows && Settings.Current.EnableWarning;
            if (this.StatusWindow != null && this.StatusWindow.IsInitialized)
            {
                this.StatusWindow.Visibility = enable ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public void Filter()
        {
            foreach (var fleetitem in FleetsDic)
            {
                fleetitem.Value.ForEach(s => UpdateWarningShip(s));
            }
            OnShipsChange();
        }

        private void UpdateFleet(Fleet fleet)
        {
            if (fleet.Ships != null)
            {
                if (!FleetsDic.ContainsKey(fleet.Id))
                {
                    FleetsDic.Add(fleet.Id, new List<Model.WarningShip>());
                }
                var ships = FleetsDic[fleet.Id];
                ships.Clear();

                //移除大破列表中已经不存在舰队的船
                var nohasList_Warning = WarningShips.Where(x => x.Value.FleetIndex == fleet.Id).Select(x => x.Value.Id);
                nohasList_Warning = nohasList_Warning.Except(fleet.Ships.Select(x => x.Id));
                nohasList_Warning.ForEach(id => WarningShips.Remove(id));

                fleet.Ships.Select(s => new Model.WarningShip(s, fleet.Id)).ForEach(s => ships.Add(s));
                ships.ForEach(x => UpdateWarningShip(x));

                OnShipsChange();
            }

        }

        private void ShipsStateChange(Ship ship)
        {
            if (UpdateShip(ship))
            {
                OnShipsChange();
            }
        }

        private void RepairyardChange()
        {
            this.Filter();
        }


        private bool UpdateShip(Ship ship)
        {
            var wShip = GetWarningShip(ship);
            if (wShip == null) return false;

            wShip.Name = ship.Info.Name;
            wShip.HP = ship.HP;
            wShip.Situation = ship.Situation;

            return UpdateWarningShip(wShip);
        }
        private bool UpdateWarningShip(Model.WarningShip ship)
        {
            if (IsHeavilyDamaged(ship))
            {
                if (!WarningShips.ContainsKey(ship.Id))
                {
                    WarningShips.Add(ship.Id, ship);

                    return true;
                }
            }
            else
            {
                if (WarningShips.ContainsKey(ship.Id))
                {
                    WarningShips.Remove(ship.Id);

                    return true;
                }

            }

            return false;
        }

        private Model.WarningShip GetWarningShip(Ship ship)
        {
            foreach (var fleetitem in FleetsDic)
            {
                return fleetitem.Value.FirstOrDefault(s => s.Id == ship.Id);
            }

            return null;
        }
        /// <summary>
        /// 是否大破
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        private bool IsHeavilyDamaged(Model.WarningShip ship)
        {
            if (ship == null ||
                ship.HP.ShipStatus() != Plugins.Core.Enums.ShipStatus.SevereDamage)
                return false;


            if ((ship.FleetIndex == 1 && Settings.Current.EnableFleet1) ||
                (ship.FleetIndex == 2 && Settings.Current.EnableFleet2) ||
                (ship.FleetIndex == 3 && Settings.Current.EnableFleet3) ||
                (ship.FleetIndex == 4 && Settings.Current.EnableFleet4))
            {
                //if (!(ship.Situation.HasFlag(ShipSituation.Repair) && Settings.Current.FilterInRepairing))//没有入渠或者关闭入渠选择
                //{
                //    return true;
                //}
                if (!(IsRepair(ship.Id) && Settings.Current.FilterInRepairing))//没有入渠或者关闭入渠选择
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// 是否入渠
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool IsRepair(int id)
        {
            if (KanColleClient.Current == null ||
                KanColleClient.Current.Homeport == null ||
                KanColleClient.Current.Homeport.Repairyard == null)
                return false;

            return KanColleClient.Current.Homeport.Repairyard.CheckRepairing(id);
        }

        private void ShipsWarning(List<Model.WarningShip> ships)
        {
            if (!Settings.Current.EnableWarning) return;

            this.StatusWindow.UpdateFleet(ships);
            SetWarning(ships.Count > 0);
        }



        bool waiting = false;
        bool isWarning = false;
        private void SetWarning(bool val)
        {
            if (waiting || isWarning == val) return;

            waiting = true;
            isWarning = val;

            WarningTheme();
            WarningEx();

            new Plugins.Core.Helper.ThreadHelper().DeferredExecution(400, () =>
            {
                waiting = false;
            });
        }

        private void WarningEx()
        {
            var enable = Settings.Current.EnableWarningEx && Settings.Current.EnableWarning;
            if (!isWarning || !enable) return;

            GenericMessager.Current.SendToWarning(new Plugins.Core.Models.MessageItem
            {
                Title = TextResource.Warning_Title,
                Content = TextResource.Warning_Content
            });
        }
        private void WarningTheme()
        {
            var enable = Settings.Current.EnableThemeWarning && Settings.Current.EnableWarning;
            ThemeServiceEx.Current.IsWarning = isWarning && enable;
        }

        #endregion

        #region event


        public event EventHandler<List<Model.WarningShip>> ShipsChange;

        private void OnShipsChange()
        {
            if (ShipsChange != null)
                ShipsChange(this, WarningShips.Select(x => x.Value).ToList());
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();
            this.StatusWindow = new Views.StatusWindow();
            this.StatusWindow.Loaded += (sender, e) => EnableWindows();
            this.StatusWindow.Show();
            this.ShipsChange += (sender, e) => ShipsWarning(e);
            this.shipStatusViewModel.ShipsChange += (sender, e) => this.UpdateFleet(e);
            this.shipStatusViewModel.ShipsStateChange += (sender, e) => this.ShipsStateChange(e);
            this.shipStatusViewModel.RepairyardChange += (sender, e) => this.RepairyardChange();

            this.shipStatusViewModel.Listener();//开始监听
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
