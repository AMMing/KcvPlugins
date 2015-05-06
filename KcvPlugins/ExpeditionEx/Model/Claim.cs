using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.ExpeditionEx.Model
{
    public class Claim
    {
        /// <summary>
        /// 当前
        /// </summary>
        public int Now { get; set; }

        /// <summary>
        /// 至少
        /// </summary>
        public int AtLeast { get; set; }

        /// <summary>
        /// 是否满足条件
        /// </summary>
        public bool IsAccord
        {
            get
            {
                return this.CheckFunc();
            }
        }


        private Func<bool> _check_func = null;

        public virtual Func<bool> CheckFunc
        {
            get
            {
                if (_check_func == null)
                {
                    return () => this.Now >= this.AtLeast;
                }
                return _check_func;
            }
            set { _check_func = value; }
        }

    }
}
