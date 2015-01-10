using MetroRadiance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using AMing.SettingsExtensions.Extensions;

namespace AMing.SettingsExtensions.Helper
{
    public class ThemeHelper
    {
        public ThemeHelper()
        {
            Init();
        }
        #region member

        public Dictionary<Theme, Models.ThemeItem<Theme>> ThemeList { get; set; }

        public Dictionary<Accent, Models.ThemeItem<Accent>> AccentList { get; set; }


        const string ThemeBackgroundColorKey = "ThemeColorKey";
        const string ThemeForegroundColorKey = "ForegroundColorKey";
        const string AccentBackgroundColorKey = "AccentHighlightColorKey";
        const string AccentForegroundColorKey = "AccentForegroundColorKey";

        #endregion

        #region method

        #region list

        void Init()
        {
            ThemeList = new Dictionary<Theme, Models.ThemeItem<Theme>>();
            AccentList = new Dictionary<Accent, Models.ThemeItem<Accent>>();

            var themelist = GetEnumList<Theme>();
            var accentlist = GetEnumList<Accent>();

            GetThemeList(ThemeList, themelist, ThemeBackgroundColorKey, ThemeForegroundColorKey, CreateThemeResourceUri);
            GetThemeList(AccentList, accentlist, AccentBackgroundColorKey, AccentForegroundColorKey, CreateAccentResourceUri);
        }

        private List<T> GetEnumList<T>()
        {
            List<T> list = new List<T>();
            EnumEx.ForEach<T>(item => list.Add(item));

            return list;
        }
        private Models.ThemeItem<T> GetThemeItem<T>(T item, string bg_colorKey, string fg_colorKey, Func<T, Uri> getResourceUri)
        {
            Uri uri = getResourceUri(item);
            if (uri == null)
            {
                return null;
            }
            var resource = GetResource(uri);
            if (resource == null)
            {
                return null;
            }
            return new Models.ThemeItem<T>
            {
                Type = item,
                BackgroundColor = (Color)resource[bg_colorKey],
                ForegroundColor = (Color)resource[fg_colorKey],
            };
        }


        private void GetThemeList<T>(Dictionary<T, Models.ThemeItem<T>> dic, List<T> list, string bg_colorKey, string fg_colorKey, Func<T, Uri> getResourceUri)
        {
            list.ForEach(item =>
            {
                var result = GetThemeItem<T>(item, bg_colorKey, fg_colorKey, getResourceUri);
                if (result != null)
                {
                    dic.Add(item, result);
                }
            });
        }

        #endregion

        #region ResourceDictionary


        private static Uri CreateThemeResourceUri(Theme theme)
        {
            var uri = string.Format(@"pack://application:,,,/MetroRadiance;component/Themes/{0}.xaml", theme);
            Uri result;
            return Uri.TryCreate(uri, UriKind.Absolute, out result) ? result : null;
        }

        private static Uri CreateAccentResourceUri(Accent accent)
        {
            var uri = string.Format(@"pack://application:,,,/MetroRadiance;component/Themes/Accents/{0}.xaml", accent);
            Uri result;
            return Uri.TryCreate(uri, UriKind.Absolute, out result) ? result : null;
        }


        private ResourceDictionary GetResource(Uri uri)
        {
            try
            {
                return new ResourceDictionary { Source = uri };
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #endregion
    }
}
