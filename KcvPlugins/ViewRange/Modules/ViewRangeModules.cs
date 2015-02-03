using AMing.Plugins.Core.Modules;
using System.Collections.Generic;
using System.Linq;
using AMing.Plugins.Core.Extensions;
using System.Windows.Media;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Collections;
using Grabacr07.KanColleViewer.ViewModels.Contents.Fleets;
using AMing.Plugins.Core.Helper;
using AMing.Plugins.Core;

namespace AMing.ViewRange.Modules
{
    public class ViewRangeModules : ModulesBase
    {
        #region Current

        private static ViewRangeModules _current = new ViewRangeModules();

        public static ViewRangeModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();

            ReplaceDataTemplate();
        }

        #region method

        public void ReplaceDataTemplate()
        {
            var dataTemplate = GetDataTemplate();
            if (dataTemplate == null) return;

            var statusBar = KcvMainWindowControlHelper.Current.StatusBar;
            var grid = statusBar.Content as Grid;
            var content = grid.Children.OfType<ContentControl>().FirstOrDefault();

            var result = content.Resources.Cast<DictionaryEntry>().Where((item, i) =>
                (item.Key is DataTemplateKey) &&
                (item.Key as DataTemplateKey).DataType.Equals(typeof(FleetsViewModel))
            ).FirstOrDefault();

            content.Resources.Remove(result.Key);
            content.Resources.Add(result.Key, dataTemplate);
        }


        private DataTemplate GetDataTemplate()
        {
            var uri = CreateResourceDictionaryUri();
            if (uri != null)
            {
                var resource = new ResourceDictionary { Source = uri };
                return resource["NewTotalViewRange"] as DataTemplate;
            }

            return null;
        }

        private static Uri CreateResourceDictionaryUri()
        {
            var uri = string.Format(@"pack://application:,,,/AMing.ViewRange;component/Views/ResourceDictionary.xaml");
            Uri result;
            return Uri.TryCreate(uri, UriKind.Absolute, out result) ? result : null;
        }

        #endregion


        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
