using AMing.Logger.Modes;
using AMing.Plugins.Core.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Logger.Helper
{

    public partial class LogsHelperBase<T_List, T_Model>
        where T_List : ILogsList<T_Model>, new()
        where T_Model : IResult
    {
        /// <summary>
        /// 日志根目录位置
        /// </summary>
        protected readonly string loggerRootDir = Path.Combine(
              Environment.CurrentDirectory,
              "Plugins",
              "Logger");
        /// <summary>
        /// 日志文件格式
        /// </summary>
        protected readonly string logFormat = "json.txt";

        /// <summary>
        /// 每个文件保存的最大条数
        /// </summary>
        protected virtual int MaxSaveCount { get { return 10; } }

        /// <summary>
        /// 目录名
        /// </summary>
        protected virtual string FolderName { get { return "Battle"; } }

        /// <summary>
        /// 日志文件目录路径
        /// </summary>
        /// <returns></returns>
        protected virtual string FolderPath
        {
            get
            {
                return Path.Combine(this.loggerRootDir, this.FolderName);
            }
        }

        /// <summary>
        /// 备份文件目录路径
        /// </summary>
        protected virtual string BackupFolderPath
        {
            get
            {
                return Path.Combine(this.FolderPath, "Backup");
            }
        }

        /// <summary>
        /// 获取保存最近数据文件路径
        /// </summary>
        /// <returns></returns>
        protected virtual string LastFilePath
        {
            get
            {
                return Path.Combine(
                    this.FolderPath,
                    string.Format("logs_last.{0}", logFormat)
                    );
            }
        }

        /// <summary>
        /// 获取备份文件路径
        /// </summary>
        /// <returns></returns>
        protected virtual string BackupFilePath
        {
            get
            {
                var filename = string.Format("logs_{0:yyyy_MM_dd_HH_mm}_{1}.{2}",
                    DateTime.Now,
                    Guid.NewGuid().ToString().Replace("-", string.Empty),
                    logFormat
                    );
                return Path.Combine(
                    this.BackupFolderPath,
                    filename
                    );
            }
        }
        /// <summary>
        /// 创建新的列表实体
        /// </summary>
        /// <returns></returns>
        protected virtual T_List CreateNewList()
        {
            return new T_List
            {
                List = new T_Model[0],
                UpdateDate = DateTime.Now
            };
        }
        /// <summary>
        /// 清空列表
        /// </summary>
        /// <param name="list"></param>
        protected virtual void ClearList(T_List list)
        {
            list.List = new T_Model[0];
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        protected virtual T_List GetList(string filepath)
        {
            var file_content = AMing.Plugins.Core.Helper.TextFileHelper.TxtFileRead(filepath);
            var list = JsonHelper.Deserialize<T_List>(file_content);

            if (list == null)
            {
                list = CreateNewList();
            }

            return list;
        }
        /// <summary>
        /// 保存列表数据
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filepath"></param>
        protected virtual void SaveList(T_List list, string filepath)
        {
            string json = JsonHelper.Serialize(list);
            AMing.Plugins.Core.Helper.TextFileHelper.TxtFileWrite(filepath, json);
        }
        /// <summary>
        /// 追加数据
        /// </summary>
        /// <param name="addCallback"></param>
        protected virtual void Append(Func<T_List, bool> addCallback)
        {
            var filepath_last = this.LastFilePath;
            var list = GetList(filepath_last);

            if (addCallback != null)
            {
                if (!addCallback(list))
                {
                    return;
                }
            }
            list.UpdateDate = DateTime.Now;
            //save to file 
            if (list.List.Length >= MaxSaveCount)
            {
                var filepath_backup = BackupFilePath;
                SaveList(list, filepath_backup);
                ClearList(list);
            }

            SaveList(list, filepath_last);
        }
    }
}
