using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Base.Model
{
    public class DynamicModel : DynamicObject
    {
        public DynamicModel()
        {
        }

        public DynamicModel(object obj)
        {
            this.Source = obj;
        }
        /// <summary>
        /// 源数据
        /// </summary>
        public object Source { get; private set; }


        public static dynamic Parse(object obj)
        {
            return (new DynamicModel(obj)).Source;
        }



    }
}
