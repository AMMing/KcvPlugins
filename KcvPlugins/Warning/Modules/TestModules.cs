using AMing.Plugins.Core.Enums;
using AMing.Plugins.Core.Helper;
using AMing.Plugins.Core.Models;
using AMing.Plugins.Core.Modules;
using Grabacr07.Desktop.Metro.Controls;
using Grabacr07.KanColleViewer.ViewModels;
using Grabacr07.KanColleViewer.ViewModels.Contents;
using Livet.Behaviors;
using MetroRadiance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using kcv = Grabacr07.KanColleViewer;

namespace AMing.Warning.Modules
{
    public class TestModules : ModulesBase
    {
        #region Current

        private static TestModules _current = new TestModules();

        public static TestModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

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
            //隐藏全部窗体
            //MessagerModules.Current.Register<string>(this, "TestModules", Test);
        }
        private readonly MethodBinder binder = new MethodBinder();

        //void Test(string msg)
        //{
        //    AMing.Plugins.Core.Helper.MessageBoxDialog.Show("Warning:" + msg);
        //}


        #endregion


        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
