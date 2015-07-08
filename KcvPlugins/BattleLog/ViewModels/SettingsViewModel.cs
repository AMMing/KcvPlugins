using AMing.Logger.Data;
using Livet;
using Livet.Commands;
using MetroRadiance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AMing.Logger.ViewModels
{
    public class SettingsViewModel : AMing.Plugins.Core.ViewModels.ViewModelEx
    {
        public SettingsViewModel()
        {
            #region init

            var assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            PluginInfo = string.Format("{0} Version {1}",
                assembly.Name,
                assembly.Version.ToString());

            #endregion
        }

        #region PluginInfo

        private string _pluginInfo;

        public string PluginInfo
        {
            get { return _pluginInfo; }
            set { base.RaisePropertyChanged(ref _pluginInfo, value); }
        }

        #endregion

        #region AllBattleCount
        private int _allBattleCount;

        public int AllBattleCount
        {
            get { return _allBattleCount; }
            set { base.RaisePropertyChanged(ref _allBattleCount, value); }
        }

        #endregion


        #region ToDayBattleCount

        private int _toDayBattleCount;

        public int ToDayBattleCount
        {
            get { return _toDayBattleCount; }
            set { base.RaisePropertyChanged(ref _toDayBattleCount, value); }
        }

        #endregion

        #region KcvRunBattleCount

        private int _kcvRunBattleCount;

        public int KcvRunBattleCount
        {
            get { return _kcvRunBattleCount; }
            set { base.RaisePropertyChanged(ref _kcvRunBattleCount, value); }
        }

        #endregion

        #region BattleAdmiralList

        private string _battleAdmiralList;

        public string BattleAdmiralList
        {
            get { return _battleAdmiralList; }
            set { base.RaisePropertyChanged(ref _battleAdmiralList, value); }
        }

        #endregion

        #region LastBattleUpdateDate

        private string _lastBattleUpdateDate;

        public string LastBattleUpdateDate
        {
            get { return _lastBattleUpdateDate; }
            set { base.RaisePropertyChanged(ref _lastBattleUpdateDate, value); }
        }

        #endregion

        #region AdmiralResourceCount

        private int _admiralResourceCount;

        public int AdmiralResourceCount
        {
            get { return _admiralResourceCount; }
            set { base.RaisePropertyChanged(ref _admiralResourceCount, value); }
        }

        #endregion

        #region LastAdmiralResourceUpdateDate

        private string _lastAdmiralResourceUpdateDate;

        public string LastAdmiralResourceUpdateDate
        {
            get { return _lastAdmiralResourceUpdateDate; }
            set { base.RaisePropertyChanged(ref _lastAdmiralResourceUpdateDate, value); }
        }

        #endregion

        public string DateTimeToString(DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                return "未记录";
            }

            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }


        public void OpenBattleLogWindow()
        {
            Modules.LoggerModules.Current.OpenBattleLogWindow();
        }


        public void OpenAdmiralLogWindow()
        {
            Modules.LoggerModules.Current.OpenAdmiralLogWindow();
        }

    }
}
