﻿using Grabacr07.KanColleViewer.Composition;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace AMing.SettingsExtensions
{
    [Export(typeof(IToolPlugin))]
    [ExportMetadata("Title", "SettingsExtensions")]
    [ExportMetadata("Description", "KCV Settings Extensions")]
    [ExportMetadata("Version", "1.0")]
    [ExportMetadata("Author", "@AMing")]
    public class Entrance : IToolPlugin
    {
        public string ToolName
        {
            get { return TextResource.Plugin_ToolName; }
        }

        private SettingsControl ToolControl = new SettingsControl();

        public object GetToolView()
        {
            return ToolControl;
        }

        public object GetSettingsView()
        {
            return null;
        }

        public Entrance()
        {
            Init();
        }

        ~Entrance()
        {
            Exit();
        }

        private void Init()
        {
            Data.Settings.Load();
        }

        private void Exit()
        {
            Data.Settings.Current.Save();
        }
    }
}