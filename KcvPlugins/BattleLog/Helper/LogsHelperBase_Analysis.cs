using AMing.Logger.Modes;
using AMing.Plugins.Core.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMing.Plugins.Core.Extensions;
using System.Collections;

namespace AMing.Logger.Helper
{

    public partial class LogsHelperBase<T_List, T_Model>
    {
        /// <summary>
        /// 获取全部日志文件路径
        /// </summary>
        /// <returns></returns>
        protected virtual IList<string> GetAllFilePath()
        {
            var list = new List<string>
            { 
                this.LastFilePath
            };
            var dirInfo = new DirectoryInfo(this.BackupFolderPath);
            if (dirInfo.Exists)
            {
                var backuplist = dirInfo.GetFiles(string.Format("*.{0}", this.logFormat)).Select(x => x.FullName);
                list.AddRange(backuplist);
            }
            return list;
        }
        /// <summary>
        /// 获取全部列表
        /// </summary>
        /// <returns></returns>
        protected virtual IList<T_List> GetAllList()
        {
            return GetAllFilePath().Select(x => GetList(x)).ToList();
        }
        /// <summary>
        /// 获取所有的项（通过id移除重复项）
        /// </summary>
        /// <returns></returns>
        protected virtual IList<T_Model> GetAllItem(IList<T_List> allList)
        {
            var result = allList.SelectMany(x => x.List).Distinct(x => x.Id);

            return result.ToList();
        }

        public virtual IList<T_List> GetInfo(out IList<T_Model> list, out DateTime lastUpdateDate)
        {
            var allList = GetAllList();

            list = GetAllItem(allList);
            lastUpdateDate = allList.Select(x => x.UpdateDate).OrderByDescending(x => x).FirstOrDefault();

            return allList;
        }
    }
}
