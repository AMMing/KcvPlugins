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

namespace AMing.QuestsExtensions.Modules
{
    public class QuestsModules : ModulesBase
    {
        #region Current

        private static QuestsModules _current = new QuestsModules();

        public static QuestsModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        Helper.QuestHelper QuestHelper = new Helper.QuestHelper();


        #region method


        public ViewModels.QuestViewModelEx Replace(ViewModels.QuestViewModelEx quest_vm)
        {
            return QuestHelper.Replace(quest_vm);
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();

            QuestHelper.Init();
        }


        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
