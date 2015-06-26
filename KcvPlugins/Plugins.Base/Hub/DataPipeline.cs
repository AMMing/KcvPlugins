using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Base.Hub
{
    /// <summary>
    /// 数据管道（主动获取某个数据，或者被动接受）
    /// </summary>
    public class DataPipeline
    {
        public DataPipeline(string key)
        {
            this.MethodKey = key;
            this.RegisterMethodListener();
        }

        /// <summary>
        /// 方法的key
        /// </summary>
        public string MethodKey { get; private set; }

        /// <summary>
        /// 数据回调时
        /// </summary>
        public event EventHandler<Model.DataPipelineResult> DataResult;

        private void OnDataResult(Model.DataPipelineResult dpResult)
        {
            if (DataResult != null)
                DataResult(this, dpResult);
        }

        /// <summary>
        /// 索取数据
        /// </summary>
        /// <param name="args"></param>
        public void Ask(dynamic args)
        {
            var dpResult = new Model.DataPipelineResult
            {
                ActionType = Enums.ActionType.Initiative,
                Result = null
            };
            dynamic result = null;
            dpResult.IsSuccess = MethodHub.Current.TryExecuteMethod(this.MethodKey, args, ref result);
            dpResult.Result = result;

            this.OnDataResult(dpResult);
        }

        /// <summary>
        /// 获取DataPipeline的ListenerKey
        /// </summary>
        /// <param name="methodKey"></param>
        /// <returns></returns>
        public static string GetListenerKey(string methodKey)
        {
            return string.Format("DataPipeline.{0}", methodKey);
        }

        /// <summary>
        /// 注册被动接受数据
        /// </summary>
        private void RegisterMethodListener()
        {
            RadioHub.Current.Register(new ListenerMember
            {
                ListenerKey = GetListenerKey(this.MethodKey),
                ListenerObject = this,
                Receive = x =>
                {
                    this.OnDataResult(new Model.DataPipelineResult
                    {
                        ActionType = Enums.ActionType.Passive,
                        Result = x,
                        IsSuccess = true
                    });
                }
            });
        }
    }
}
