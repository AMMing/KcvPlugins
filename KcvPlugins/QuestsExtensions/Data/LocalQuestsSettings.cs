using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Grabacr07.KanColleViewer.Models.Data.Xml;
using Grabacr07.KanColleWrapper;
using Livet;
using Grabacr07.KanColleViewer.Models;
using System.Windows.Input;

namespace AMing.QuestsExtensions.Data
{
    [Serializable]
    public class LocalQuestsSettings
    {
        #region static members

        private static readonly string filePath = Path.Combine(
            Environment.CurrentDirectory,
            "Plugins",
            "LocalQuests",
            "Settings.xml");

        public static LocalQuestsSettings Current { get; set; }

        public static void Load()
        {
            try
            {
                Current = filePath.ReadXml<LocalQuestsSettings>();
                Current.IsLoad = true;
            }
            catch (Exception ex)
            {
#if DEBUG
                Current = GetInitialSettings();
                Current.IsLoad = false;
                System.Diagnostics.Debug.WriteLine(ex);

#endif
            }
        }

#if DEBUG
        public static LocalQuestsSettings GetInitialSettings()
        {
            var list = new List<string>();
            list.Add("zh-cn");
            var defaultkey = list.First();

            return new LocalQuestsSettings
            {
                QuestsResourceList = list,
                Default = defaultkey
            };
        }
        public void Save()
        {
            try
            {
                this.WriteXml(filePath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
#endif

        #endregion

        #region member


        public string Default { get; set; }

        public List<string> QuestsResourceList { get; set; }

        [XmlIgnore]
        public bool IsLoad { get; set; }

        #endregion

    }
}
