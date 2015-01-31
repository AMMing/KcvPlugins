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
using AMing.Plugins.Core.Helper;
using AMing.Plugins.Core.Extensions;
using Grabacr07.KanColleWrapper.Models;

namespace AMing.Warning.ViewModels
{
    public class ShipStatusViewModel : TabItemViewModel
    {

        public override string Name
        {
            get
            {
                return string.Empty;
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }

        public void Listener()
        {
            Listener_KanColleClientCurrent();
        }


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
            foreach (var item in KanColleClient.Current.Homeport.Organization.Fleets)
            {
                this.CompositeDisposable.Add(new PropertyChangedEventListener(item.Value)
			    {
				    (sender, args) =>  PropertyChangedFunc(sender, args.PropertyName)
                });
            };
        }

        private void PropertyChangedFunc(object obj, string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.ToLower() != "ships") return;
            var fleet = obj as Fleet;
            if (fleet != null)
            {
                OnShipsChange(fleet);
            }
        }

        public event EventHandler<Fleet> ShipsChange;

        private void OnShipsChange(Fleet fleet)
        {
            if (ShipsChange != null)
                ShipsChange(this, fleet);
        }
    }
}
