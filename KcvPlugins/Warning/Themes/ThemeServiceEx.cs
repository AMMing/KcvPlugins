﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MetroRadiance.Core;
using MetroRadiance.Internal;
using AMing.Plugins.Core.Extensions;

namespace AMing.Warning
{
    public class ThemeServiceEx : Notificator
    {
        #region singleton members

        private static readonly ThemeServiceEx current = new ThemeServiceEx();

        public static ThemeServiceEx Current
        {
            get { return current; }
        }

        #endregion

        private ResourceDictionary appAccent;
        private bool initialized;



        private ThemeServiceEx() { }

        public bool Initialize()
        {
            if (this.initialized)
            {
                return true;
            }
            var accentUri = CreateAccentResourceUri();
            if (accentUri != null)
            {
                var accentResource = Application.Current.Resources.MergedDictionaries.FirstOrDefault(x => Uri.Compare(
                    x.Source,
                    accentUri,
                    UriComponents.AbsoluteUri,
                    UriFormat.Unescaped,
                    StringComparison.InvariantCultureIgnoreCase) == 0);

                if (accentResource == null)
                {
                    accentResource = new ResourceDictionary { Source = accentUri };
                    Application.Current.Resources.MergedDictionaries.Add(accentResource);
                }
                this.appAccent = accentResource;
            }
            this.initialized = this.appAccent != null;

            return true;
        }

        private bool _isWarning = false;

        public bool IsWarning
        {
            get { return _isWarning; }
            set
            {
                if (_isWarning != value)
                {
                    _isWarning = value;
                    if (_isWarning)
                    {
                        if (this.Initialize())
                        {
                            ChangeAccent();
                        }
                    }
                    else
                    {
                        if (this.initialized)
                        {
                            Application.Current.Resources.MergedDictionaries.Remove(this.appAccent);
                            this.appAccent = null;
                            this.initialized = false;
                        }
                    }
                }
            }
        }



        private void ChangeAccent()
        {
            var uri = CreateAccentResourceUri();
            if (uri != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var resource = new ResourceDictionary { Source = uri };
                    this.ChangeAccentCore(resource);
                });
            }
        }

        private void ChangeAccent(ResourceDictionary resource)
        {
            Application.Current.Dispatcher.Invoke(() => this.ChangeAccentCore(resource));
        }

        private void ChangeAccentCore(ResourceDictionary resource)
        {
            resource.Keys.OfType<string>()
                .Where(key => this.appAccent.Contains(key))
                .ForEach(key => this.appAccent[key] = resource[key]);
        }


        string AccentsUri = string.Format(@"pack://application:,,,/MetroRadiance;component/Themes/Accents/");

        private static Uri CreateAccentResourceUri()
        {
            var uri = string.Format(@"pack://application:,,,/AMing.Warning;component/Themes/Accents/Red.xaml");
            Uri result;
            return Uri.TryCreate(uri, UriKind.Absolute, out result) ? result : null;
        }

    }

}
