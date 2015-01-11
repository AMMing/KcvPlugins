using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grabacr07.KanColleViewer.Models;
using Grabacr07.KanColleWrapper;
using Livet;
using Livet.EventListeners;
using Grabacr07.KanColleViewer.ViewModels;
using Grabacr07.KanColleViewer.ViewModels.Contents.Fleets;
using System.Windows;

namespace AMing.SettingsExtensions.ViewModels
{
    public class SimpleFleetViewModel : TabItemViewModel
    {
        public override string Name
        {
            get { return string.Empty; }
            protected set { throw new NotImplementedException(); }
        }
        #region Fleets 変更通知プロパティ

        private FleetViewModel[] _Fleets;

        public FleetViewModel[] Fleets
        {
            get { return this._Fleets; }
            set
            {
                if (this._Fleets != value)
                {
                    this._Fleets = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region FirstFleet

        private FleetViewModel _FirstFleet;

        public FleetViewModel FirstFleet
        {
            get { return this._FirstFleet; }
            set
            {
                if (this._FirstFleet != value)
                {
                    this._FirstFleet = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region FirstFleetInit

        private bool _FirstFleetInit;

        public bool FirstFleetInit
        {
            get { return this._FirstFleetInit; }
            set
            {
                if (this._FirstFleetInit != value)
                {
                    this._FirstFleetInit = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion


        public SimpleFleetViewModel()
        {
            this.CompositeDisposable.Add(new PropertyChangedEventListener(KanColleClient.Current)
			{
				{ () => KanColleClient.Current.IsStarted, (sender, args) => this.UpdateMode() },
				{ () => KanColleClient.Current.IsInSortie, (sender, args) => this.UpdateMode() },
			});

            this.UpdateMode();
        }

        private void UpdateMode()
        {
            if (KanColleClient.Current.IsStarted)
            {
                Initialize();
            }
        }

        public void Initialize()
        {
            this.CompositeDisposable.Add(new PropertyChangedEventListener(KanColleClient.Current.Homeport.Organization)
			{
				{ "Fleets", (sender, args) => this.UpdateFleets() },
			});
            this.UpdateFleets();
        }

        private void UpdateFleets()
        {
            this.Fleets = KanColleClient.Current.Homeport.Organization.Fleets.Select(kvp => new FleetViewModel(kvp.Value)).ToArray();
            this.FirstFleet = this.Fleets.FirstOrDefault();

            this.FirstFleetInit = this.FirstFleet != null && this.FirstFleet.Ships != null && this.FirstFleet.Ships.Count() > 0;
            if (this.FirstFleetInit)
            {
                Helper.MessagerHelper.Current.Send(Entrance.MessagerKey + "FirstFleetInit");
            }
        }

    }
}
