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
using Grabacr07.KanColleWrapper.Models.Raw;

namespace AMing.Logger.ViewModels
{
    public class LoggerViewModel : ViewModel
    {
        public void Listener()
        {
            Listener_KanColleClientCurrent();
        }


        private void Listener_KanColleClientCurrent()
        {
            this.CompositeDisposable.Add(new PropertyChangedEventListener(KanColleClient.Current)
			{
				{ "IsStarted", (sender, args) => this.UpdateMode() }
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
                Listener_Homeport();
            }
        }

        bool isListener_Homeport = false;
        private void Listener_Homeport()
        {
            if (isListener_Homeport) return;
            isListener_Homeport = true;

            this.CompositeDisposable.Add(new PropertyChangedEventListener(KanColleClient.Current)
			{
				{ "IsInSortie", (sender, args) => this.UpdateIsInSortie() }
			});
            this.UpdateIsInSortie();

            this.CompositeDisposable.Add(new PropertyChangedEventListener(KanColleClient.Current.Homeport.Repairyard)
			{
				{ "Docks", (sender, args) => this.UpdateRepairingDocks() },
				{ "Docks", (sender, args) => this.UpdateIsInSortie() }
			});

            this.CompositeDisposable.Add(new PropertyChangedEventListener(KanColleClient.Current.Homeport.Dockyard)
			{
				{ "Docks", (sender, args) => this.UpdateBuildingDocks() },
				{ "CreatedSlotItem", (sender, args) => this.UpdateSlotItem() }
			});

            KanColleClient.Current.Proxy.api_req_sortie_battleresult.TryParse<kcsapi_battleresult>().Subscribe(x => AppendLog(x.Data));
        }

        private bool isFirstBattle = false;
        private bool oldIsBattle = false;


        private void AppendLog(kcsapi_battleresult br)
        {
            Helper.BattleLogsHelper.Current.Append(KanColleClient.Current, br, isFirstBattle);
            isFirstBattle = false;//重置

            AMing.Plugins.Core.GenericMessager.Current.SendToLogs(br.ToStringContent());

        }

        private void UpdateIsInSortie()
        {
            if (KanColleClient.Current.IsInSortie && !oldIsBattle)
            {
                isFirstBattle = true;
            }
            oldIsBattle = KanColleClient.Current.IsInSortie;

            Helper.AdmiralInfoHelper.Current.Append(KanColleClient.Current);
        }


        private void UpdateRepairingDocks()
        {
            KanColleClient.Current.Homeport.Repairyard.Docks.ForEach(item =>
            {
                AMing.Plugins.Core.GenericMessager.Current.SendToLogs(item.ToStringContent());
            });
        }
        private void UpdateBuildingDocks()
        {
            KanColleClient.Current.Homeport.Dockyard.Docks.ForEach(item =>
            {
                AMing.Plugins.Core.GenericMessager.Current.SendToLogs(item.ToStringContent());
            });
        }

        private void UpdateSlotItem()
        {
            var msg = KanColleClient.Current.Homeport.Dockyard.CreatedSlotItem.ToStringContent();
            AMing.Plugins.Core.GenericMessager.Current.SendToLogs(msg);

        }
    }


}
