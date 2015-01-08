using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AMing.SettingsExtensions.ViewModels.Themes
{
    public class ThemeItemViewModels<T, T_Models> : ViewModel
        where T_Models : Models.ThemeItem<T>
    {
        public ThemeItemViewModels(T_Models models)
        {
            this.Type = models.Type;
            this.BackgroundColor = models.BackgroundColor;
            this.ForegroundColor = models.ForegroundColor;
        }

        public Color BackgroundColor { get; set; }
        public Color ForegroundColor { get; set; }

        public T Type { get; set; }


        #region IsSelected

        private bool _IsSelected;

        public bool IsSelected
        {
            get { return this._IsSelected; }
            set
            {
                if (this._IsSelected != value)
                {
                    this._IsSelected = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

    }
}
