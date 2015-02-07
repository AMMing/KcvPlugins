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
    public class AdmiralInfoHelper
    {
        #region Current

        private static AdmiralInfoHelper _current = new AdmiralInfoHelper();

        public static AdmiralInfoHelper Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        protected const int MaxSaveCount = 10;//Debug 10 

        protected readonly static TimeSpan Interval = TimeSpan.FromHours(1);

        /// <summary>
        /// 日志根目录位置
        /// </summary>
        protected readonly string loggerRootDir = Path.Combine(
              Environment.CurrentDirectory,
              "Plugins",
              "Logger",
              "AdmiralInfo");

        private string GetBackupPath()
        {
            var filename = string.Format("logs_{0:yyyy_MM_dd_HH_mm}_{1}.json.txt",
                DateTime.Now,
                Guid.NewGuid().ToString().Replace("-", "")
                );

            return Path.Combine(
                this.loggerRootDir,
                "Backup",
                filename
                );
        }

        private string GetLastPath()
        {
            return Path.Combine(
                this.loggerRootDir,
                "logs_last.json.txt"
                );
        }

        private Modes.AdmiralInfoList GetList(string filepath)
        {
            var file_content = AMing.Plugins.Core.Helper.TextFileHelper.TxtFileRead(filepath);
            var list = JsonHelper.Deserialize<Modes.AdmiralInfoList>(file_content);
            if (list == null)
            {
                list = new Modes.AdmiralInfoList
                {
                    List = new List<Modes.AdmiralInfo>(),
                    UpdateDate = DateTime.MinValue
                };
            }

            return list;
        }
        private void SaveList(Modes.AdmiralInfoList brList, string filepath)
        {
            string json = JsonHelper.Serialize(brList);
            AMing.Plugins.Core.Helper.TextFileHelper.TxtFileWrite(filepath, json);
        }

        public void Append(KanColleClient kanColleClient)
        {
            var filepath_last = GetLastPath();

            var resultList = GetList(filepath_last);
            var admiralInfo = new Modes.AdmiralInfo(KanColleClient.Current.Homeport.Admiral, KanColleClient.Current.Homeport.Materials);

            if (resultList.UpdateDate.Add(Interval) > DateTime.Now || (
                admiralInfo.Fuel == 0 &&
                admiralInfo.Ammunition == 0 &&
                admiralInfo.Steel == 0 &&
                admiralInfo.Bauxite == 0
                ))
            {
                return;
            }
            resultList.List.Add(admiralInfo);
            resultList.UpdateDate = DateTime.Now;

            //save to file 
            if (resultList.List.Count > MaxSaveCount)
            {
                var filepath_backup = GetBackupPath();
                SaveList(resultList, filepath_backup);
                resultList.List.Clear();
            }

            SaveList(resultList, filepath_last);
        }
    }
}
