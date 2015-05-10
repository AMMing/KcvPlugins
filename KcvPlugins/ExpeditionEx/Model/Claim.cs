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


        private bool _isAccord = false;
        /// <summary>
        /// 是否满足条件
        /// </summary>
        public virtual bool IsAccord
        {
            get
            {
                if (_isAccord) return true;

                return this.CheckIsAccord();
            }
            protected set { _isAccord = value; }
        }

        protected virtual bool CheckIsAccord()
        {
            return this.Now >= this.AtLeast;
        }

        public virtual string ErrorMessage
        {
            get
            {
                return string.Format(ErrorMessageFormat, this.AtLeast, this.Now);
            }
        }

        private string _errorMessageFormat = "{1} < {0}";

        public string ErrorMessageFormat
        {
            get { return _errorMessageFormat; }
            set { _errorMessageFormat = value; }
        }

    }
}
