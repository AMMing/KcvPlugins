using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.DebugExtensions.Helper
{
    public class ErrorLogHelper : LogHelper<Exception>
    {

        #region Current

        private static ErrorLogHelper _current = new ErrorLogHelper();

        public static ErrorLogHelper Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        protected override string GetLogDirPath()
        {
            return Path.Combine(
                base.logsRootDir,
                "Error",
                DateTime.Now.ToString("yyyy_MM")
                );
        }

        protected override string GetLogFileName()
        {
            return string.Format("logs_{0:yyyy_MM_dd}.txt", DateTime.Now);
        }

        protected override string ObjectToString(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Time:{0:yyyy-MM-dd HH:mm:ss.fff}\nMessage:{1}\nSource:{2}\nTrace:\n{3}\n",
                DateTime.Now,
                ex.Message,
                ex.Source,
                ex.StackTrace);
            sb.AppendLine("---------------------------------------");

            return sb.ToString();
        }
    }
}
