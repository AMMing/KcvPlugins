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
				{ 
                    "IsStarted",  
                    KcListenerHelper.PropertyChangedEventListener_Try((sender, args) => this.UpdateMode()) 
                }
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

            #region 监听舰队是否出击


            this.CompositeDisposable.Add(new PropertyChangedEventListener(KanColleClient.Current)
			{
				{ 
                    "IsInSortie", 
                     KcListenerHelper.PropertyChangedEventListener_Try((sender, args) => this.UpdateIsInSortie())
                }
			});
            this.UpdateIsInSortie();

            #endregion

            #region 监听战斗结果

            //普通的战斗结果
            KanColleClient.Current.Proxy.api_req_sortie_battleresult.SessionTryParse<kcsapi_battleresult>().Subscribe(x => AppendBattleResult(x.Data));

            //联合舰队的战斗结果
            KanColleClient.Current.Proxy.api_req_combined_battle_battleresult.SessionTryParse<kcsapi_combined_battle_battleresult>().Subscribe(x => AppendCombinedBattleResult(x.Data));
            #endregion

            #region 监听舰队信息

            this.CompositeDisposable.Add(new PropertyChangedEventListener(KanColleClient.Current.Homeport.Organization)
			{
				{ 
                    "Fleets",  
                    KcListenerHelper.PropertyChangedEventListener_Try((sender, args) => this.UpdateFleets())
                }
			});
            this.UpdateFleets();

            #endregion

            this.CompositeDisposable.Add(new PropertyChangedEventListener(KanColleClient.Current.Homeport.Repairyard)
			{
				{ 
                    "Docks", 
                     KcListenerHelper.PropertyChangedEventListener_Try((sender, args) => this.UpdateRepairingDocks())
                }
			});

            this.CompositeDisposable.Add(new PropertyChangedEventListener(KanColleClient.Current.Homeport.Dockyard)
			{
				{ 
                    "Docks", 
                     KcListenerHelper.PropertyChangedEventListener_Try((sender, args) => this.UpdateBuildingDocks())
                },
				{ 
                    "CreatedSlotItem", 
                     KcListenerHelper.PropertyChangedEventListener_Try((sender, args) => this.UpdateSlotItem())
                }
			});

            //KanColleClient.Current.Proxy.api_req_sortie_battle.SessionTryParse<kcsapi_battle>().Subscribe(x => AMing.Plugins.Core.GenericMessager.Current.SendToLogs(x.Data.ToStringContentAndType()));

            //KanColleClient.Current.Proxy.api_req_combined_battle_battle.SessionTryParse<kcsapi_combined_battle>().Subscribe(x => AMing.Plugins.Core.GenericMessager.Current.SendToLogs(x.Data.ToStringContentAndType()));
            //KanColleClient.Current.Proxy.api_req_combined_battle_airbattle.SessionTryParse<kcsapi_combined_battle_airbattle>().Subscribe(x => AMing.Plugins.Core.GenericMessager.Current.SendToLogs(x.Data.ToStringContentAndType()));

        }


        #region 战斗结果


        private bool isFirstBattle = false;
        private bool oldIsBattle = false;

        private void AppendBattleResult(kcsapi_battleresult br)
        {
            //AMing.Plugins.Core.GenericMessager.Current.SendToLogs(br == null ? string.Empty : br.ToStringContentAndType());

            if (br == null) return;

            OnBattleEnd(KanColleClient.Current, br, isFirstBattle);
            isFirstBattle = false;//重置
        }


        private void AppendCombinedBattleResult(kcsapi_combined_battle_battleresult br)
        {
            //AMing.Plugins.Core.GenericMessager.Current.SendToLogs(br == null ? string.Empty : br.ToStringContentAndType());

            if (br == null) return;

            OnCombinedBattleEnd(KanColleClient.Current, br, isFirstBattle);
            isFirstBattle = false;//重置
        }

        #endregion

        #region 舰队出击改变

        private void UpdateIsInSortie()
        {
            if (KanColleClient.Current.IsInSortie && !oldIsBattle)
            {
                isFirstBattle = true;
            }
            oldIsBattle = KanColleClient.Current.IsInSortie;

            OnAdmiralInfoChange(KanColleClient.Current);
        }

        #endregion

        #region 舰队信息改变

        private void UpdateFleets()
        {
            foreach (var item in KanColleClient.Current.Homeport.Organization.Fleets)
            {
                this.CompositeDisposable.Add(new PropertyChangedEventListener(item.Value)
			    {
				     KcListenerHelper.PropertyChangedEventListener_Try((sender, args) =>  PropertyChangedFunc(sender, args.PropertyName))
                });
            };
            KanColleClient.Current.Homeport.Organization.Fleets.ForEach(item => OnShipsChange(item.Value));
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
        #endregion


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
        /// 战斗结束
        /// </summary>
        public event EventHandler<Modes.CombinedBattleEndEventArgs> CombinedBattleEnd;
        private void OnCombinedBattleEnd(KanColleClient kanColleClient, kcsapi_combined_battle_battleresult br, bool isFirstBattle)
        {
            if (CombinedBattleEnd != null)
                CombinedBattleEnd(this, new Modes.CombinedBattleEndEventArgs
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
        /// <summary>
        /// 舰队信息改变
        /// </summary>

        public event EventHandler<Fleet> ShipsChange;

        private void OnShipsChange(Fleet fleet)
        {
            if (ShipsChange != null)
                ShipsChange(this, fleet);
        }


        #endregion



    }



}
