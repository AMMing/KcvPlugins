using AMing.Plugins.Core.Modules;
using Grabacr07.KanColleWrapper.Models;
using Grabacr07.KanColleWrapper;
using System.Collections.Generic;
using System.Linq;
using AMing.Plugins.Core.Extensions;
using Livet.Behaviors;
using System;
using AMing.Logger.Data;
using AMing.Plugins.Core;
using AMing.Plugins.Core.Enums;
using System.Windows;
using AMing.Logger.Extensions;
using Grabacr07.KanColleWrapper.Models.Raw;

namespace AMing.Logger.Modules
{
    public class LoggerModules : ModulesBase
    {
        #region Current

        private static LoggerModules _current = new LoggerModules();

        public static LoggerModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        #region member

        private readonly ViewModels.LoggerViewModel loggerViewModel = new ViewModels.LoggerViewModel();

        #endregion

        #region method

        #endregion

        #region event


        #endregion

        public override void Initialize()
        {
            base.Initialize();

            loggerViewModel.Listener();
        }



        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
