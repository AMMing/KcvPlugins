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

        private void UpdateFleets()
        {
            foreach (var item in KanColleClient.Current.Homeport.Organization.Fleets)
            {
                this.CompositeDisposable.Add(new PropertyChangedEventListener(item.Value)
			    {
                    "Ships",  
				    KcListenerHelper.PropertyChangedEventListener_Try((sender, args) =>  PropertyChangedFunc(sender, "Ships"))
                });
            };
            KanColleClient.Current.Homeport.Organization.Fleets.ForEach(item => PropertyChangedFunc(item.Value, "Ships"));
        }

        private void Listener_Ships(Fleet fleet)
        {
            if (fleet == null || fleet.Ships == null) return;

            foreach (var item in fleet.Ships)
            {
                this.CompositeDisposable.Add(new PropertyChangedEventListener(item)
			    {
                    "HP",  
				    KcListenerHelper.PropertyChangedEventListener_Try((sender, args) =>  PropertyChangedFunc(sender, "HP"))
                });
                this.CompositeDisposable.Add(new PropertyChangedEventListener(item)
			    {
                    "Situation",  
				    KcListenerHelper.PropertyChangedEventListener_Try((sender, args) =>  PropertyChangedFunc(sender, "Situation"))
                });
            };
            fleet.Ships.ForEach(item => PropertyChangedFunc(item, "situation"));
        }


        private void PropertyChangedFunc(object obj, string name)
        {
            Plugins.Core.GenericMessager.Current.SendToLogs(new
            {
                obj = obj.ToStringContentAndType(),
                name = name
            }.ToStringContentAndType());
            if (string.IsNullOrWhiteSpace(name)) return;
            switch (name.ToLower())
            {
                case "ships":
                    var fleet = obj as Fleet;
                    if (fleet != null)
                    {
                        Listener_Ships(fleet);
                    }
                    break;
                case "situation":
                case "hp":
                    var ship = obj as Ship;
                    if (ship != null)
                    {
                        OnHeavilyDamagedChange();
                    }
                    break;
                default:
                    break;
            }
        }

        public event EventHandler HeavilyDamagedChange;
        /// <summary>
        /// 大破状态改变的情况（HP变化，大破状态改变，不管有没有大破，只要有值变化都触发一次事件，但是处理那边会有个锁）
        /// </summary>
        private void OnHeavilyDamagedChange()
        {
            if (HeavilyDamagedChange != null)
                HeavilyDamagedChange(this, null);
        }
    }
}
