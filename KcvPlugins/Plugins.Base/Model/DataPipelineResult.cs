using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Base.Model
{
    public class DataPipelineResult
    {
        /// <summary>
        /// 动作类型
        /// </summary>
        public Enums.ActionType ActionType { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public dynamic Result { get; set; }
        /// <summary>
        /// 是否获取成功
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}
