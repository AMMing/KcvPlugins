using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlugins
{
    public class ModuleClass4 : AMing.Plugins.Base.Interface.IModules
    {
        private static ModuleClass4 _current = new ModuleClass4();

        public static ModuleClass4 Current
        {
            get { return _current; }
            set { _current = value; }
        }
        public string Key
        {
            get
            {
                return "ModuleClass4";
            }
            set { }
        }
        /// <summary>
        /// 初始化开始
        /// </summary>
        public void Initialize_Start()
        {
            AMing.Plugins.Base.Hub.MethodHub.Current.Register("test.key", TestMethod);
        }
        /// <summary>
        /// 初始化结束
        /// </summary>
        public void Initialize_End()
        {


        }
        public void Dispose()
        {

        }


        private dynamic TestMethod(dynamic val)
        {
            dynamic args = new System.Dynamic.ExpandoObject();
            //args.test = 123;
            //args.v = val.aaa;
            args.stauts = val.bbb;
            args.data = new TestModel
            {
                ID = 2443,
                Name = "test name.",
                Enable = true
            };

            //return args;

            return new TestModel
            {
                ID = 2443,
                Name = "test name.",
                Enable = true
            };
        }
    }
}
