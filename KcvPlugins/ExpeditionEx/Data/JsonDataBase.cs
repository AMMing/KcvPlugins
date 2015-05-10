using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.ExpeditionEx.Data
{
    public class JsonDataBase<T>
        where T : class
    {
        /// <summary>
        /// 根目录位置
        /// </summary>
        protected readonly string jsonRootDir = Path.Combine(
              Environment.CurrentDirectory,
              "Plugins",
              "Json");

        protected virtual string FileName { get; private set; }

        protected virtual string FilePath
        {
            get
            {
                return Path.Combine(
                    jsonRootDir,
                    this.FileName);
            }
        }

        protected T _data;

        public T Data
        {
            get
            {
                if (_data == null)
                {
                    _data = this.Read();
                }

                return _data;
            }
            set
            {
                if (_data != value)
                {
                    _data = value;
                    this.Save();
                }
            }
        }
        protected virtual string ReadContent()
        {
            return Plugins.Core.Helper.TextFileHelper.TxtFileRead(this.FilePath);
        }
        protected virtual T Deserialize(string content)
        {
            return Plugins.Core.Helper.JsonHelper.Deserialize<T>(content);
        }
        protected virtual T Read()
        {
            var content = this.ReadContent();
            var result = this.Deserialize(content);

            return result;
        }
        protected virtual string Serialize(object obj)
        {
            return Plugins.Core.Helper.JsonHelper.Serialize(obj);
        }
        protected virtual void SaveContent(string content)
        {
            Plugins.Core.Helper.TextFileHelper.TxtFileWrite(this.FileName, content);
        }
        protected virtual void Save()
        {
            var content = this.Serialize(this.Data);
            this.SaveContent(content);
        }
    }
}
