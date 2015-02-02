using AMing.Plugins.Core.Modules;
using Grabacr07.KanColleWrapper.Models;
using Grabacr07.KanColleWrapper;
using System.Collections.Generic;
using System.Linq;
using AMing.Plugins.Core.Extensions;
using System;

namespace AMing.DebugExtensions.Modules
{
    public class LogsModules : ModulesBase
    {
        #region Current

        private static LogsModules _current = new LogsModules();

        public static LogsModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        #region member

        public ViewModels.SettingsViewModel SettingsViewModel { get; set; }

        #endregion

        #region method

        #endregion

        public override void Initialize()
        {
            base.Initialize();
            InitPublicModules();
        }

        #region PublicModules

        private void InitPublicModules()
        {
            AMing.Plugins.Core.GenericMessager.Current.Register(this, Plugins.Core.Enums.MessageType.Logs, obj => AppendLogs(obj));
        }

        void AppendLogs(object obj)
        {
            SettingsViewModel.LogsText = string.Format("{0}\nDateTime:{1:yyyy-MM-dd HH:mm:ss.fff}\n{2}\n",
                SettingsViewModel.LogsText,
                DateTime.Now,
                obj);
        }


        #endregion


        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
