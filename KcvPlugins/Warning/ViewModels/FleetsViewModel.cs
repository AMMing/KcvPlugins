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
using AMing.Plugins.Core.Extensions;
using AMing.Plugins.Core.Helper;

namespace AMing.Warning.ViewModels
{
    public class FleetsViewModel : TabItemViewModel
    {

        public override string Name
        {
            get { return Grabacr07.KanColleViewer.Properties.Resources.Fleets; }
            protected set { throw new NotImplementedException(); }
        }

        public FleetsViewModel()
        {
            Listener_KanColleClientCurrent();
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

        private void Listener_KanColleClientCurrent()
        {
            this.CompositeDisposable.Add(new PropertyChangedEventListener(KanColleClient.Current)
			{
				{ () => KanColleClient.Current.IsStarted, (sender, args) => this.UpdateMode() },
				{ () => KanColleClient.Current.IsInSortie, (sender, args) => this.UpdateMode() },
			});
        }

        private void UpdateMode()
        {
            var mode = KanColleClient.Current.IsStarted
                 ? KanColleClient.Current.IsInSortie
                     ? Mode.InSortie
                     : Mode.Started
                 : Mode.NotStarted;
            if (mode == Mode.Started)
            {
                Listener_Fleets();
            }
        }

        bool isListener_Fleets = false;
        private void Listener_Fleets()
        {
            if (isListener_Fleets) return;
            isListener_Fleets = true;

            this.CompositeDisposable.Add(new PropertyChangedEventListener(KanColleClient.Current.Homeport.Organization)
			{
				{ "Fleets", (sender, args) => this.UpdateFleets() },
			});
            this.UpdateFleets();
        }

        private void UpdateFleets()
        {
            this.Fleets = KanColleClient.Current.Homeport.Organization.Fleets.Select(kvp => new FleetViewModel(kvp.Value)).ToArray();

            if (this.Fleets != null)
            {
                foreach (var fleetItem in this.Fleets)
                {
                    fleetItem.PropertyChanged += (sender, e) => this.CheckHP();
                    if (fleetItem.Ships != null)
                    {
                        foreach (var shipItem in fleetItem.Ships)
                        {
                            shipItem.PropertyChanged += (sender, e) => this.CheckHP();
                        }
                    }
                }
            }

            this.CheckHP();
        }

        private void CheckHP()
        {
            if (this.Fleets != null)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (var fleetItem in this.Fleets)
                {
                    if (fleetItem.Ships != null)
                    {
                        foreach (var shipItem in fleetItem.Ships)
                        {
                            sb.AppendFormat("{0}\t{1}\n", shipItem.Ship.Info.Name, shipItem.Ship.HP.ShipStatus());
                        }
                    }
                }
                var msg = sb.ToString();
                if (!string.IsNullOrWhiteSpace(msg))
                    Msg(msg);
            }
        }

        private void Msg(string msg)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                MsgText = msg;
            }));
        }

        private string _MsgText;

        public string MsgText
        {
            get { return this._MsgText; }
            set
            {
                if (this._MsgText != value)
                {
                    this._MsgText = value;
                    this.RaisePropertyChanged();
                }
            }
        }

    }
}
