using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Core.Helper
{
    public class TextFileHelper
    {
        /// <summary>
        /// 读取txt文本内容
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string TxtFileRead(string filepath)
        {
            FileInfo fileInfo = new FileInfo(filepath);
            if (fileInfo.Exists)
            {
                using (var stream = fileInfo.Open(FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 保存Txt文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="txt"></param>
        public static void TxtFileWrite(string filepath, string txt)
        {
            string folder = Path.GetDirectoryName(filepath);

            DirectoryInfo dirInfo = new DirectoryInfo(folder);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            FileInfo fileInfo = new FileInfo(filepath);
            if (fileInfo.Exists)
            {
                System.IO.File.SetAttributes(filepath, FileAttributes.Normal);
            }
            using (var stream = fileInfo.Open(FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    sw.Write(txt);
                }
            }
        }


        /// <summary>
        /// 保存Txt文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="txt"></param>
        public static void AppendAllText(string filepath, string txt)
        {
            string folder = Path.GetDirectoryName(filepath);

            DirectoryInfo dirInfo = new DirectoryInfo(folder);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            System.IO.FileInfo fileInfo = new FileInfo(filepath);
            if (fileInfo.Exists)
            {
                System.IO.File.SetAttributes(filepath, FileAttributes.Normal);
            }

            File.AppendAllText(filepath, txt);
        }




        /// <summary>
        /// 读取txt文本内容
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static List<string> TxtFileReadLines(string filepath)
        {
            System.IO.FileInfo fileInfo = new FileInfo(filepath);
            if (fileInfo.Exists)
            {
                using (var stream = fileInfo.Open(FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        string sLine = "";
                        List<string> LineList = new List<string>();
                        while (sLine != null)
                        {
                            sLine = sr.ReadLine();
                            if (sLine != null && !sLine.Equals(""))
                            {
                                LineList.Add(sLine);
                            }
                        }
                        sr.Close();
                        return LineList;

                    }
                }
            }
            else
            {
                return null;
            }
        }
    }
}
