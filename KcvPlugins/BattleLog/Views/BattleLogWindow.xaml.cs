using Grabacr07.KanColleViewer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AMing.Logger.Views
{
    partial class BattleLogWindow
    {
        public BattleLogWindow()
        {
            this.InitializeComponent();

            Application.Current.MainWindow.Closed += (sender, args) => this.Close();
        }
    }
}
