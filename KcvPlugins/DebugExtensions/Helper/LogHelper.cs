using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.DebugExtensions.Helper
{
    public class LogHelper<T>
    {
        /// <summary>
        /// 日志根目录位置
        /// </summary>
        protected readonly string logsRootDir = Path.Combine(
              Environment.CurrentDirectory,
              "Plugins",
              "Logs");
        /// <summary>
        /// 获取日志保存目录
        /// </summary>
        /// <returns></returns>
        protected virtual string GetLogDirPath()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取日志文件名
        /// </summary>
        /// <returns></returns>
        protected virtual string GetLogFileName()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 将对象转换成字符串
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        protected virtual string ObjectToString(T val)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 追加内容
        /// </summary>
        /// <param name="val"></param>
        public virtual void Append(T val)
        {
            try
            {
                string logDirPath = GetLogDirPath();
                string logFileName = GetLogFileName();
                string logFileNamePath = Path.Combine(logDirPath, logFileName);
                if (!Directory.Exists(logDirPath))
                {
                    Directory.CreateDirectory(logDirPath);
                }
                string content = ObjectToString(val);
                File.AppendAllText(logFileNamePath, content);
            }
            catch { }
        }
    }
}
