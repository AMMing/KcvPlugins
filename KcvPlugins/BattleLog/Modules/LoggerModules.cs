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

        }
        private void ChangeAdmiralInfo()
        {
            this.SettingsViewModel.AdmiralResourceCount = this.allAdmiralInfo.Count;
            this.SettingsViewModel.LastAdmiralResourceUpdateDate = this.lastAdmiralInfoChange;
        }

        #endregion

        #region event

        void loggerViewModel_BattleEnd(object sender, Modes.BattleEndEventArgs e)
        {
            Helper.BattleLogsHelper.Current.Append(e.KanColleClient, e.BattleResult, e.IsFirstBattle);
            if (this.lastBattle.Day != DateTime.Now.Day)//重置今天的次数
            {
                this.todayBattleCount = 0;
            }
            this.lastBattle = DateTime.Now;
            this.runBattleCount++;
            this.todayBattleCount++;

            this.ChangeBattleInfo();
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


        #endregion

        public override void Initialize()
        {
            base.Initialize();

            InitInfo();
            loggerViewModel.BattleEnd += loggerViewModel_BattleEnd;
            loggerViewModel.AdmiralInfoChange += loggerViewModel_AdmiralInfoChange;
            loggerViewModel.Listener();
        }



        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
