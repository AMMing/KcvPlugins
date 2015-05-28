using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlugins
{
    public class Class2 : AMing.Plugins.Base.Generic.SettingBase<Class3>
    {
        private static Class2 _current = new Class2();

        public static Class2 Current
        {
            get { return _current; }
            set { _current = value; }
        }

        public override string SettingKey
        {
            get { return "TestPlugins"; }
        }

        public override string SettingDirName
        {
            get { return "TestPlugins"; }
        }

        public override string SettingFileName
        {
            get { return "Setting.xml"; }
        }


        public override void SetDefault()
        {
            this.Settings = new Class3
            {
                AAAAA = true ,
                MyProperty = 6976,
                MyProperty2 = "czxczxc"
            };
        }


    }
}
