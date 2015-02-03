using AMing.ViewRange.Data;
using Livet;
using Livet.Commands;
using MetroRadiance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AMing.Plugins.Core.Extensions;
using Grabacr07.KanColleWrapper;

namespace AMing.ViewRange.ViewModels
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

        private void RaisePropertyChanged_ViewRangeCalcLogic()
        {
            //Grabacr07.KanColleViewer.Models.Settings.Current.KanColleClientSettings.GetMethod<object>("RaisePropertyChanged", "ViewRangeCalcLogic");
            Grabacr07.KanColleViewer.Models.Settings.Current.KanColleClientSettings.ViewRangeCalcLogic =
            Grabacr07.KanColleViewer.Models.Settings.Current.KanColleClientSettings.ViewRangeCalcLogic ==
                ViewRangeCalcLogic.Type1 ? ViewRangeCalcLogic.Type2 : ViewRangeCalcLogic.Type1;
        }
        #region ViewRangeType1

        public bool ViewRangeType1
        {
            get { return Settings.Current.Type == Enums.ViewRangeType.Type1; }
            set
            {
                if (value) Settings.Current.Type = Enums.ViewRangeType.Type1;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged_ViewRangeCalcLogic();
            }
        }

        #endregion

        #region ViewRangeType2

        public bool ViewRangeType2
        {
            get { return Settings.Current.Type == Enums.ViewRangeType.Type2; }
            set
            {
                if (value) Settings.Current.Type = Enums.ViewRangeType.Type2;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged_ViewRangeCalcLogic();
            }
        }

        #endregion

        #region ViewRangeType3

        public bool ViewRangeType3
        {
            get { return Settings.Current.Type == Enums.ViewRangeType.Type3; }
            set
            {
                if (value) Settings.Current.Type = Enums.ViewRangeType.Type3;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged_ViewRangeCalcLogic();
            }
        }

        #endregion

        #region ViewRangeType4

        public bool ViewRangeType4
        {
            get { return Settings.Current.Type == Enums.ViewRangeType.Type4; }
            set
            {
                if (value) Settings.Current.Type = Enums.ViewRangeType.Type4;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged_ViewRangeCalcLogic();
            }
        }

        #endregion
    }
}
