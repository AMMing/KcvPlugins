using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlugins
{
    public class ModuleClass4 : AMing.Plugins.Base.Generic.ModulesBase
    {
        private static ModuleClass4 _current = new ModuleClass4();

        public static ModuleClass4 Current
        {
            get { return _current; }
            set { _current = value; }
        }

        public override string Key
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
        public override void Initialize_Start()
        {
            AMing.Plugins.Base.Hub.MethodHub.Current.Register("test.key", TestMethod);
        }


        private dynamic TestMethod(dynamic val)
        {
            var r_val = new AMing.Plugins.Base.Model.DynamicArgs<int, string, string>()
            {
                val2 = "tsestatatt"
            };
            if (AMing.Plugins.Base.Model.DynamicArgs<int>.Validation(val))
            {
                r_val.val2 += "\nint";
            }
            if (AMing.Plugins.Base.Model.DynamicArgsBase.Validation(val, "DynamicArgs.Int32.String"))
            {
                r_val.val1 = val.val1;
                r_val.val3 = val.val2;
                r_val.val2 += "\nintString";
            }

            //dynamic args = new System.Dynamic.ExpandoObject();
            ////args.test = 123;
            ////args.v = val.aaa;
            //args.stauts = val.bbb;
            //args.data = new TestModel
            //{
            //    ID = 2443,
            //    Name = "test name.",
            //    Enable = true
            //};

            ////return args;

            //return new TestModel
            //{
            //    ID = 2443,
            //    Name = "test name.",
            //    Enable = true
            //};

            return r_val;
        }
    }
}
