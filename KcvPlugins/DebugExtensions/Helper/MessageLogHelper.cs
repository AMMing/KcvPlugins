using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.DebugExtensions.Helper
{
    public class MessageLogHelper : LogHelper<string>
    {
        #region Current

        private static MessageLogHelper _current = new MessageLogHelper();

        public static MessageLogHelper Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        protected override string GetLogDirPath()
        {
            return Path.Combine(
                base.logsRootDir,
                "Message",
                DateTime.Now.ToString("yyyy_MM_dd")
                );
        }
        /// <summary>
        /// message有时候消息量比较大，所以一个小时记录一个文件
        /// </summary>
        /// <returns></returns>
        protected override string GetLogFileName()
        {
            return string.Format("msg_{0:yyyy_MM_dd_HH}.txt", DateTime.Now);
        }

        protected override string ObjectToString(string  val)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Time:{0:yyyy-MM-dd HH:mm:ss.fff}\nMessage:\n{1}\n",
                DateTime.Now,
                val);
            sb.AppendLine("---------------------------------------");

            return sb.ToString();
        }
    }
}
