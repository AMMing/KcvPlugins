using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models.Raw;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Logger.Helper
{
    public class AdmiralInfoHelper : LogsHelperBase<Modes.AdmiralInfoList, Modes.AdmiralInfo>
    {
        #region Current

        private static AdmiralInfoHelper _current = new AdmiralInfoHelper();

        public static AdmiralInfoHelper Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion
        protected override int MaxSaveCount { get { return 1000; } }
        protected override string FolderName { get { return "AdmiralInfo"; } }

        protected readonly static TimeSpan Interval = TimeSpan.FromHours(1);

        public void Append(KanColleClient kanColleClient, Action<bool> isAppend)
        {
            base.Append(list =>
            {
                var admiralInfo = new Modes.AdmiralInfo(KanColleClient.Current.Homeport.Admiral, KanColleClient.Current.Homeport.Materials);

                if (list.UpdateDate.Add(Interval) > DateTime.Now || (
                    admiralInfo.Fuel == 0 &&
                    admiralInfo.Ammunition == 0 &&
                    admiralInfo.Steel == 0 &&
                    admiralInfo.Bauxite == 0
                    ))
                {
                    isAppend(false);
                    return false;
                }
                var newlist = list.List.ToList();
                newlist.Add(admiralInfo);
                list.List = newlist.ToArray();

                isAppend(true);
                return true;
            });
        }

    }
}
