using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AMing.Plugins.Base.Helper
{
    public class XmlFileWriterHelper
    {
        /// <summary>
        /// 指定した型のデータを XML ファイルに書き込みます。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="saveData">書き込むデータ。</param>
        /// <param name="savePath">保存先のパス。</param>
        public static void WriteXml(Type type, object saveData, string savePath)
        {
            var dir = Path.GetDirectoryName(Path.GetFullPath(savePath)) ?? "";
            Directory.CreateDirectory(dir);

            FileStream stream = null;								// 書き込み用ファイルストリーム
            var serializer = new XmlSerializer(type);	// シリアル化用のオブジェクトを生成

            try
            {
                stream = new FileStream(savePath, FileMode.Create);	// ファイルストリーム生成
                serializer.Serialize(stream, saveData);				// シリアル化して書き込み
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();		// ストリームを閉じる
                    stream.Dispose();	// ストリームを解放
                }
            }
        }
    }
}
