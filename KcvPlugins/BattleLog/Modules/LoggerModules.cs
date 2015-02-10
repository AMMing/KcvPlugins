using AMing.Plugins.Core.Modules;
using Grabacr07.KanColleWrapper.Models;
using Grabacr07.KanColleWrapper;
using System.Collections.Generic;
using System.Linq;
using AMing.Plugins.Core.Extensions;
using Livet.Behaviors;
using System;
using AMing.Logger.Data;
using AMing.Plugins.Core;
using AMing.Plugins.Core.Enums;
using System.Windows;
using AMing.Logger.Extensions;
using Grabacr07.KanColleWrapper.Models.Raw;

namespace AMing.Logger.Modules
{
    public class LoggerModules : ModulesBase
    {
        #region Current

        private static LoggerModules _current = new LoggerModules();

        public static LoggerModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        #region member

        private readonly ViewModels.LoggerViewModel loggerViewModel = new ViewModels.LoggerViewModel();
        private readonly ViewModels.BattleLogViewModel battleLogViewModel = new ViewModels.BattleLogViewModel();

        public ViewModels.SettingsViewModel SettingsViewModel { get; set; }

        private IList<Modes.BattleResult> allBattleResult = new List<Modes.BattleResult>();
        private IList<Modes.SimpleAdmiral> allAdmiral = new List<Modes.SimpleAdmiral>();
        private int runBattleCount = 0;
        private int todayBattleCount = 0;

        private DateTime lastBattle = DateTime.MinValue;

        private IList<Modes.AdmiralInfo> allAdmiralInfo = new List<Modes.AdmiralInfo>();
        private DateTime lastAdmiralInfoChange = DateTime.MinValue;


        #endregion

        #region method

        private void ChangeBattleInfo()
        {
            this.SettingsViewModel.BattleAdmiralList = this.allAdmiral.Select(x => x.Nickname).ToString(" , ");
            this.SettingsViewModel.LastBattleUpdateDate = this.lastBattle;

            this.SettingsViewModel.KcvRunBattleCount = runBattleCount;
            this.SettingsViewModel.ToDayBattleCount = todayBattleCount;

            this.SettingsViewModel.AllBattleCount = this.allBattleResult.Count + runBattleCount;

            this.battleLogViewModel.UpdateReload();

        }
        private void ChangeAdmiralInfo()
        {
            this.SettingsViewModel.AdmiralResourceCount = this.allAdmiralInfo.Count;
            this.SettingsViewModel.LastAdmiralResourceUpdateDate = this.lastAdmiralInfoChange;
        }


        public void OpenBattleLogWindow()
        {
            Views.BattleLogWindow BattleLogWindow = new Views.BattleLogWindow { DataContext = this.battleLogViewModel };
            BattleLogWindow.Show();
        }

        private void NotificationBattle(Modes.BattleResult battleResult)
        {
            AMing.Plugins.Core.GenericMessager.Current.SendToNotification(new Plugins.Core.Models.MessageItem
            {
                Title = string.Format("战斗结束  {0}", battleResult.WinRank),
                Content = battleResult.GetShip == null ? "没有捞到船" : string.Format("捕获到野生的 {0}", battleResult.GetShip.Name)
            });
        }
        private void AddBattleAfter(Modes.BattleResult battleResult)
        {
            NotificationBattle(battleResult);
            if (this.lastBattle.Day != DateTime.Now.Day)//重置今天的次数
            {
                this.todayBattleCount = 0;
            }
            this.lastBattle = DateTime.Now;
            this.runBattleCount++;
            this.todayBattleCount++;

            this.ChangeBattleInfo();
        }
        #endregion

        #region event
        void loggerViewModel_BattleEnd(object sender, Modes.BattleEndEventArgs e)
        {
            var battleResult = Helper.BattleLogsHelper.Current.Append(e.KanColleClient, e.BattleResult, e.IsFirstBattle);
            AddBattleAfter(battleResult);
        }

        void loggerViewModel_CombinedBattleEnd(object sender, Modes.CombinedBattleEndEventArgs e)
        {
            var battleResult = Helper.BattleLogsHelper.Current.Append(e.KanColleClient, e.BattleResult, e.IsFirstBattle);
            AddBattleAfter(battleResult);
        }
        void loggerViewModel_AdmiralInfoChange(object sender, Modes.AdmiralInfoChangeEventArgs e)
        {
            Helper.AdmiralInfoHelper.Current.Append(e.KanColleClient, x =>
            {
                if (x)
                {
                    this.lastAdmiralInfoChange = DateTime.Now;
                }
            });

            this.ChangeAdmiralInfo();
        }

        private void InitInfo()
        {
            Helper.BattleLogsHelper.Current.GetInfo(out this.allBattleResult, out  this.allAdmiral, out  this.lastBattle);
            Helper.AdmiralInfoHelper.Current.GetInfo(out this.allAdmiralInfo, out  this.lastAdmiralInfoChange);

            var now = DateTime.Now;
            this.todayBattleCount = this.allBattleResult.Where(x =>
                x.CreateDate.Year == now.Year &&
                x.CreateDate.Month == now.Month &&
                x.CreateDate.Day == now.Day).Count();

            this.ChangeBattleInfo();
            this.ChangeAdmiralInfo();
        }


        void loggerViewModel_ShipsChange(object sender, Fleet e)
        {
            var item = Helper.BattleLogsHelper.Current.GetLastItem();
            if (item != null && !item.IsSetAfterHP())
            {
                if (item.SetFleetAfterHP(KanColleClient.Current))
                {
                    Helper.BattleLogsHelper.Current.UpdateItem(item);
                    this.battleLogViewModel.UpdateReload();
                }
            }
        }
        #endregion

        public override void Initialize()
        {
            base.Initialize();

            InitInfo();
            loggerViewModel.BattleEnd += loggerViewModel_BattleEnd;
            loggerViewModel.CombinedBattleEnd += loggerViewModel_CombinedBattleEnd;
            loggerViewModel.ShipsChange += loggerViewModel_ShipsChange;
            loggerViewModel.AdmiralInfoChange += loggerViewModel_AdmiralInfoChange;
            loggerViewModel.Listener();
        }





        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
