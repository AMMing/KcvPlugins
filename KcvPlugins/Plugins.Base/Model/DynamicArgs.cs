using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMing.Plugins.Base.Extensions;

namespace AMing.Plugins.Base.Model
{
    public class DynamicArgs : DynamicArgsBase
    {

        protected string _validationKey;
        /// <summary>
        /// 验证key
        /// </summary>
        public override string ValidationKey
        {
            get { return _validationKey; }
        }
    }



    /// <summary>
    /// 动态参数传递（单个参数）
    /// </summary>
    public class DynamicArgs<T> : DynamicArgsBase
    //where T : struct
    {
        public DynamicArgs()
        {
            this._validationKey = GetValidationKey();
        }
        public DynamicArgs(T val)
            : this()
        {
            this.Value = val;
        }

        #region member

        private string _validationKey;
        /// <summary>
        /// 验证key
        /// </summary>
        public override string ValidationKey
        {
            get { return _validationKey; }
        }

        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public T val
        {
            get { return this.Value; }
            set { this.Value = value; }
        }

        #endregion

        #region method


        /// <summary>
        /// 获取验证key
        /// </summary>
        /// <returns></returns>
        public static string GetValidationKey()
        {
            return string.Format("DynamicArgs.{0}", typeof(T).Name);
        }

        /// <summary>
        /// 验证参数
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool Validation(dynamic args)
        {
            var key = GetValidationKey();

            return Validation(args, key);
        }

        #endregion
    }
    /// <summary>
    /// 动态参数传递（两个参数）
    /// </summary>
    public class DynamicArgs<T1, T2> : DynamicArgsBase
    //where T1 : struct
    //where T2 : struct
    {
        public DynamicArgs()
        {
            this._validationKey = GetValidationKey();
        }
        public DynamicArgs(T1 val1, T2 val2)
            : this()
        {
            this.Value1 = val1;
            this.Value2 = val2;
        }

        #region member

        private string _validationKey;
        /// <summary>
        /// 验证key
        /// </summary>
        public override string ValidationKey
        {
            get { return _validationKey; }
        }
        /// <summary>
        /// 值1
        /// </summary>
        public T1 Value1 { get; set; }
        /// <summary>
        /// 值1
        /// </summary>
        public T1 val1
        {
            get { return this.Value1; }
            set { this.Value1 = value; }
        }
        /// <summary>
        /// 值2
        /// </summary>
        public T2 Value2 { get; set; }
        /// <summary>
        /// 值2
        /// </summary>
        public T2 val2
        {
            get { return this.Value2; }
            set { this.Value2 = value; }
        }

        #endregion

        #region method


        /// <summary>
        /// 获取验证key
        /// </summary>
        /// <returns></returns>
        public static string GetValidationKey()
        {
            return string.Format("DynamicArgs.{0}.{1}",
                    typeof(T1).Name,
                    typeof(T2).Name
                );
        }

        /// <summary>
        /// 验证参数
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool Validation(dynamic args)
        {
            var key = GetValidationKey();

            return Validation(args, key);
        }

        #endregion

    }
    /// <summary>
    /// 动态参数传递（三个参数）
    /// </summary>
    public class DynamicArgs<T1, T2, T3> : DynamicArgsBase
    //where T1 : struct
    //where T2 : struct
    //where T3 : struct
    {
        public DynamicArgs()
        {
            this._validationKey =
                string.Format("DynamicArgs.{0}.{1}.{2}",
                    typeof(T1).Name,
                    typeof(T2).Name,
                    typeof(T3).Name
                );
        }
        public DynamicArgs(T1 val1, T2 val2, T3 val3)
            : this()
        {
            this.Value1 = val1;
            this.Value2 = val2;
            this.Value3 = val3;
        }

        private string _validationKey;
        /// <summary>
        /// 验证key
        /// </summary>
        public override string ValidationKey
        {
            get { return _validationKey; }
        }
        /// <summary>
        /// 值1
        /// </summary>
        public T1 Value1 { get; set; }
        public T1 val1 { get { return this.Value1; } set { this.Value1 = value; } }

        /// <summary>
        /// 值2
        /// </summary>
        public T2 Value2 { get; set; }
        public T2 val2 { get { return this.Value2; } set { this.Value2 = value; } }
        /// <summary>
        /// 值3
        /// </summary>
        public T3 Value3 { get; set; }
        public T3 val3 { get { return this.Value3; } set { this.Value3 = value; } }
    }
    /// <summary>
    /// 动态参数传递（四个参数）
    /// </summary>
    public class DynamicArgs<T1, T2, T3, T4> : DynamicArgsBase
    //where T1 : struct
    //where T2 : struct
    //where T3 : struct
    //where T4 : struct
    {
        public DynamicArgs()
        {
            this._validationKey =
                string.Format("DynamicArgs.{0}.{1}.{2}.{3}",
                    typeof(T1).Name,
                    typeof(T2).Name,
                    typeof(T3).Name,
                    typeof(T4).Name
                );
        }
        public DynamicArgs(T1 val1, T2 val2, T3 val3, T4 val4)
            : this()
        {
            this.Value1 = val1;
            this.Value2 = val2;
            this.Value3 = val3;
            this.Value4 = val4;
        }

        private string _validationKey;
        /// <summary>
        /// 验证key
        /// </summary>
        public override string ValidationKey
        {
            get { return _validationKey; }
        }
        /// <summary>
        /// 值1
        /// </summary>
        public T1 Value1 { get; set; }
        public T1 val1 { get { return this.Value1; } set { this.Value1 = value; } }

        /// <summary>
        /// 值2
        /// </summary>
        public T2 Value2 { get; set; }
        public T2 val2 { get { return this.Value2; } set { this.Value2 = value; } }
        /// <summary>
        /// 值3
        /// </summary>
        public T3 Value3 { get; set; }
        public T3 val3 { get { return this.Value3; } set { this.Value3 = value; } }
        /// <summary>
        /// 值4
        /// </summary>
        public T4 Value4 { get; set; }
        public T4 val4 { get { return this.Value4; } set { this.Value4 = value; } }
    }

    /// <summary>
    /// 动态参数传递（五个参数）
    /// </summary>
    public class DynamicArgs<T1, T2, T3, T4, T5> : DynamicArgsBase
    //where T1 : struct
    //where T2 : struct
    //where T3 : struct
    //where T4 : struct
    //where T5 : struct
    {
        public DynamicArgs()
        {
            this._validationKey =
                string.Format("DynamicArgs.{0}.{1}.{2}.{3}.{4}",
                    typeof(T1).Name,
                    typeof(T2).Name,
                    typeof(T3).Name,
                    typeof(T4).Name,
                    typeof(T5).Name
                );
        }
        public DynamicArgs(T1 val1, T2 val2, T3 val3, T4 val4, T5 val5)
            : this()
        {
            this.Value1 = val1;
            this.Value2 = val2;
            this.Value3 = val3;
            this.Value4 = val4;
            this.Value5 = val5;
        }

        private string _validationKey;
        /// <summary>
        /// 验证key
        /// </summary>
        public override string ValidationKey
        {
            get { return _validationKey; }
        }
        /// <summary>
        /// 值1
        /// </summary>
        public T1 Value1 { get; set; }
        public T1 val1 { get { return this.Value1; } set { this.Value1 = value; } }

        /// <summary>
        /// 值2
        /// </summary>
        public T2 Value2 { get; set; }
        public T2 val2 { get { return this.Value2; } set { this.Value2 = value; } }
        /// <summary>
        /// 值3
        /// </summary>
        public T3 Value3 { get; set; }
        public T3 val3 { get { return this.Value3; } set { this.Value3 = value; } }
        /// <summary>
        /// 值4
        /// </summary>
        public T4 Value4 { get; set; }
        public T4 val4 { get { return this.Value4; } set { this.Value4 = value; } }
        /// <summary>
        /// 值4
        /// </summary>
        public T5 Value5 { get; set; }
        public T5 val5 { get { return this.Value5; } set { this.Value5 = value; } }
    }
}
