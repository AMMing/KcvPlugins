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
using Fiddler;

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
				{ "Docks", (sender, args) => this.UpdateRepairingDocks() }
			});

            this.CompositeDisposable.Add(new PropertyChangedEventListener(KanColleClient.Current.Homeport.Dockyard)
			{
				{ "Docks", (sender, args) => this.UpdateBuildingDocks() },
				{ "CreatedSlotItem", (sender, args) => this.UpdateSlotItem() }
			});

            KanColleClient.Current.Proxy.api_req_sortie_battleresult.TryParse<kcsapi_battleresult>().Subscribe(x => AppendBattleResult(x.Data));


            KanColleClient.Current.Proxy.api_req_sortie_battle.TryParse<kcsapi_battle>().Subscribe(x => AMing.Plugins.Core.GenericMessager.Current.SendToLogs(x.Data.ToStringContentAndType()));

            KanColleClient.Current.Proxy.api_req_combined_battle_battle.TryParse<kcsapi_combined_battle>().Subscribe(x => AMing.Plugins.Core.GenericMessager.Current.SendToLogs(x.Data.ToStringContentAndType()));
            KanColleClient.Current.Proxy.api_req_combined_battle_airbattle.TryParse<kcsapi_combined_battle_airbattle>().Subscribe(x => AMing.Plugins.Core.GenericMessager.Current.SendToLogs(x.Data.ToStringContentAndType()));
            KanColleClient.Current.Proxy.api_req_combined_battle_battleresult.TryParse<kcsapi_combined_battle_battleresult>().Subscribe(x => AMing.Plugins.Core.GenericMessager.Current.SendToLogs(x.Data.ToStringContentAndType()));
        }

        private bool isFirstBattle = false;
        private bool oldIsBattle = false;


        private void AppendBattleResult(kcsapi_battleresult br)
        {
            AMing.Plugins.Core.GenericMessager.Current.SendToLogs(br == null ? string.Empty : br.ToStringContentAndType());

            if (br == null) return;

            OnBattleEnd(KanColleClient.Current, br, isFirstBattle);
            isFirstBattle = false;//重置
        }

        private void UpdateIsInSortie()
        {
            if (KanColleClient.Current.IsInSortie && !oldIsBattle)
            {
                isFirstBattle = true;
            }
            oldIsBattle = KanColleClient.Current.IsInSortie;

            OnAdmiralInfoChange(KanColleClient.Current);
        }


        private void UpdateRepairingDocks()
        {
            //KanColleClient.Current.Homeport.Repairyard.Docks.ForEach(item =>
            //{
            //    AMing.Plugins.Core.GenericMessager.Current.SendToLogs(item.ToStringContent());
            //});
        }
        private void UpdateBuildingDocks()
        {
            //KanColleClient.Current.Homeport.Dockyard.Docks.ForEach(item =>
            //{
            //    AMing.Plugins.Core.GenericMessager.Current.SendToLogs(item.ToStringContent());
            //});
        }

        private void UpdateSlotItem()
        {
            //var msg = KanColleClient.Current.Homeport.Dockyard.CreatedSlotItem.ToStringContent();
            //AMing.Plugins.Core.GenericMessager.Current.SendToLogs(msg);

        }

        #region event
        /// <summary>
        /// 战斗结束
        /// </summary>
        public event EventHandler<Modes.BattleEndEventArgs> BattleEnd;
        private void OnBattleEnd(KanColleClient kanColleClient, kcsapi_battleresult br, bool isFirstBattle)
        {
            if (BattleEnd != null)
                BattleEnd(this, new Modes.BattleEndEventArgs
                {
                    KanColleClient = kanColleClient,
                    BattleResult = br,
                    IsFirstBattle = isFirstBattle
                });
        }
        /// <summary>
        /// 提督资源信息改变
        /// </summary>
        public event EventHandler<Modes.AdmiralInfoChangeEventArgs> AdmiralInfoChange;
        private void OnAdmiralInfoChange(KanColleClient kanColleClient)
        {
            if (AdmiralInfoChange != null)
                AdmiralInfoChange(this, new Modes.AdmiralInfoChangeEventArgs
                {
                    KanColleClient = kanColleClient
                });
        }

        #endregion



    }



}
