using AMing.Plugins.Base.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TestPlugins
{
    [Export(typeof(IPlugin))]
    public class Class1 : AMing.Plugins.Base.Generic.PluginBase
    {
        public override string PluginKey
        {
            get { return "TestPlugins_PluginKey"; }
        }

        public override string PluginName
        {
            get { return "TestPlugins_PluginName"; }
        }
        public override Version PluginVersion
        {
            get { return Version.Parse("1.0.1.1"); }
        }

        public override ImageSource ItemButton
        {
            get { return null; }
        }

        public override object SettingsView()
        {
            return null;
        }

        public override void InitSettings()
        {
            base.InitSettings();
            this._settings.Add(Class2.Current);
        }

    }
}
