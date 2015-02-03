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

        Dictionary<int, List<Ship>> FleetsDic = new Dictionary<int, List<Ship>>();

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
            OnShipsChange();
        }

        private void UpdateFleet(Fleet fleet)
        {
            if (fleet.Ships != null)
            {
                if (!FleetsDic.ContainsKey(fleet.Id))
                {
                    FleetsDic.Add(fleet.Id, new List<Ship>());
                }
                var ships = FleetsDic[fleet.Id];
                ships.Clear();
                var showlist = fleet.Ships.Where(s => s.HP.ShipStatus() == Plugins.Core.Enums.ShipStatus.SevereDamage);

                ships.AddRange(showlist);

                OnShipsChange();
            }

        }

        private List<Ship> GetShips()
        {
            List<Ship> shiplist = new List<Ship>();
            FleetsDic.Where(item =>
                //筛选舰队 start
                (item.Key == 1 && Settings.Current.EnableFleet1) ||
                (item.Key == 2 && Settings.Current.EnableFleet2) ||
                (item.Key == 3 && Settings.Current.EnableFleet3) ||
                (item.Key == 4 && Settings.Current.EnableFleet4)
                //筛选舰队 end
                ).ForEach(listitem => shiplist.AddRange(listitem.Value.Where(
                    //过滤入渠中 start
                    s => !(s.IsInRepairing && Settings.Current.FilterInRepairing)
                    //过滤入渠中 end
                )));

            return shiplist;
        }


        private void ShipsWarning(List<Ship> ships)
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

            GenericMessager.Current.SendToMessage(MessageType.Warning, new Plugins.Core.Models.MessageItem
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


        public event EventHandler<List<Ship>> ShipsChange;

        private void OnShipsChange()
        {
            var ships = GetShips();
            if (ShipsChange != null)
                ShipsChange(this, ships);
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

            this.shipStatusViewModel.Listener();//开始监听
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
