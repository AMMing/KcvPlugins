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
				{ 
                    () => 
                        KanColleClient.Current.IsStarted, 
                        KcListenerHelper.PropertyChangedEventListener_Try((sender, args) => this.UpdateMode()) 
                },
				{ 
                    () => 
                        KanColleClient.Current.IsInSortie,
                        KcListenerHelper.PropertyChangedEventListener_Try((sender, args) => this.UpdateMode())
                },
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
				{ 
                    "Fleets",  
                    KcListenerHelper.PropertyChangedEventListener_Try((sender, args) => this.UpdateFleets()) 
                }
			});
            this.UpdateFleets();
        }

        const string prop_key_fleets = "Fleets";
        private void UpdateFleets()
        {
            foreach (var item in KanColleClient.Current.Homeport.Organization.Fleets)
            {
                this.CompositeDisposable.Add(new PropertyChangedEventListener(item.Value)
			    {
                    "Fleets",  
				    KcListenerHelper.PropertyChangedEventListener_Try((sender, args) =>  PropertyChangedFunc(sender, prop_key_fleets))
                });
            };
            KanColleClient.Current.Homeport.Organization.Fleets.ForEach(item => PropertyChangedFunc(item.Value, prop_key_fleets));
        }


        const string prop_key_ships = "Ships";
        private void Listener_Ships(Fleet fleet)
        {
            if (fleet == null || fleet.Ships == null) return;

            foreach (var item in fleet.Ships)
            {
                this.CompositeDisposable.Add(new PropertyChangedEventListener(item)
			    {
                    { 
                        "HP",  
				        KcListenerHelper.PropertyChangedEventListener_Try((sender, args) =>  PropertyChangedFunc(sender, prop_key_ships))
                    },
                    { 
                        "Situation",  
				        KcListenerHelper.PropertyChangedEventListener_Try((sender, args) =>  PropertyChangedFunc(sender, prop_key_ships))
                    }
                });
            };
            fleet.Ships.ForEach(item => PropertyChangedFunc(item, prop_key_ships));
        }


        private void PropertyChangedFunc(object obj, string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return;
            switch (name)
            {
                case prop_key_fleets:
                    var fleet = obj as Fleet;
                    if (fleet != null)
                    {
                        OnShipsChange(fleet);
                    }
                    break;
                case prop_key_ships:
                    var ship = obj as Ship;
                    if (ship != null)
                    {
                        OnShipsStateChange(ship);
                    }
                    break;
            }
        }

        /// <summary>
        /// 舰队改变
        /// </summary>
        public event EventHandler<Fleet> ShipsChange;

        private void OnShipsChange(Fleet fleet)
        {
            Listener_Ships(fleet);
            if (ShipsChange != null)
                ShipsChange(this, fleet);
        }

        /// <summary>
        /// 船的状态改变
        /// </summary>
        public event EventHandler<Ship> ShipsStateChange;

        private void OnShipsStateChange(Ship ship)
        {
            if (ShipsStateChange != null)
                ShipsStateChange(this, ship);
        }
    }
}
