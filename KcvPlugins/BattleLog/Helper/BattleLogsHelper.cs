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
    public class BattleLogsHelper
    {
        #region Current

        private static BattleLogsHelper _current = new BattleLogsHelper();

        public static BattleLogsHelper Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        protected const int MaxSaveCount = 10;//Debug 10 
        /// <summary>
        /// 日志根目录位置
        /// </summary>
        protected readonly string loggerRootDir = Path.Combine(
              Environment.CurrentDirectory,
              "Plugins",
              "Logger",
              "Battle");

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

        private Modes.BattleResultList GetList(string filepath)
        {
            var file_content = AMing.Plugins.Core.Helper.TextFileHelper.TxtFileRead(filepath);
            var list = JsonHelper.Deserialize<Modes.BattleResultList>(file_content);
            if (list == null)
            {
                list = new Modes.BattleResultList
                {
                    List = new List<Modes.BattleResult>(),
                    AdmiralList = new List<Modes.SimpleAdmiral>()
                };
            }

            return list;
        }
        private void SaveList(Modes.BattleResultList brList, string filepath)
        {
            string json = JsonHelper.Serialize(brList);
            AMing.Plugins.Core.Helper.TextFileHelper.TxtFileWrite(filepath, json);
        }

        public void Append(KanColleClient kanColleClient, kcsapi_battleresult br, bool isFirstBattle)
        {
            var filepath_last = GetLastPath();

            var resultList = GetList(filepath_last);

            resultList.List.Add(new Modes.BattleResult(kanColleClient, br) { IsFirstBattle = isFirstBattle });

            var admiral = new Modes.SimpleAdmiral(kanColleClient);
            if (!resultList.AdmiralList.Any(x => x.Id == admiral.Id))
            {
                resultList.AdmiralList.Add(admiral);
            }

            resultList.UpdateDate = DateTime.Now;

            //save to file 
            if (resultList.List.Count > MaxSaveCount)
            {
                var filepath_backup = GetBackupPath();
                SaveList(resultList, filepath_backup);
                resultList.List.Clear();
                resultList.AdmiralList.Clear();
            }

            SaveList(resultList, filepath_last);
        }
    }
}
