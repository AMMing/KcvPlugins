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
    public class LocalQuests
    {
        #region static members

        private static readonly string filePathFormat = Path.Combine(
            Environment.CurrentDirectory,
            "Plugins",
            "LocalQuests",
            "QuestsResource.{0}.xml");


        #endregion

        #region member

        public string Name { get; set; }
        public string Key { get; set; }
        public string Version { get; set; }

        public List<Models.Quest> QuestsResourceList { get; set; }

        #endregion

        #region XmlIgnore member

        [XmlIgnore]
        public string XmlPath { get; private set; }

        [XmlIgnore]
        public bool XmlExist
        {
            get { return File.Exists(this.XmlPath); }
        }

        [XmlIgnore]
        public bool IsLoad { get; private set; }

        [XmlIgnore]
        public Dictionary<int, Models.Quest> QuestsResourceDictionary { get; private set; }

        #endregion

        #region method


        public bool Load()
        {
            LocalQuests current;
            try
            {
                current = this.XmlPath.ReadXml<LocalQuests>();
                this.IsLoad = true;
            }
            catch (Exception ex)
            {
                this.IsLoad = false;

#if DEBUG
                current = GetInitialSettings();
                System.Diagnostics.Debug.WriteLine(ex);
#endif
            }
#if DEBUG
            this.Name = current.Name;
            this.Key = current.Key;
            this.Version = current.Version;
            this.QuestsResourceList = current.QuestsResourceList;
#endif
            this.QuestsResourceDictionary = new Dictionary<int, Models.Quest>();
            this.QuestsResourceList.ForEach(quest => this.QuestsResourceDictionary.Add(quest.Id, quest));

            return this.IsLoad;
        }

#if DEBUG
        private LocalQuests GetInitialSettings()
        {
            var list = new List<Models.Quest>();
            list.Add(new Models.Quest
            {
                Id = 1,
                Title = "title",
                Detail = "detail"
            });
            return new LocalQuests
            {
                Name = "简体中文",
                Key = "zh-cn",
                Version = "1.0",
                QuestsResourceList = list
            };
        }

        public void Save()
        {
            try
            {
                this.WriteXml(XmlPath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
#endif
        #endregion



        public LocalQuests() { }
        public LocalQuests(string key)
        {
            this.XmlPath = string.Format(filePathFormat, key);
        }
    }
}
