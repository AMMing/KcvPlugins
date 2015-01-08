using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AMing.SettingsExtensions.ViewModels.Themes
{
    public class ThemeListViewModels<T> : ViewModel
    {
        #region WindowThemeList

        private IList<ThemeItemViewModels<T, Models.ThemeItem<T>>> _list;

        public IList<ThemeItemViewModels<T, Models.ThemeItem<T>>> List
        {
            get
            {
                if (_list == null && GetListFunc != null)
                {
                    var result = GetListFunc();

                    List = result;
                }

                return _list;
            }
            set
            {
                if (_list != value)
                {
                    _list = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region SelectedItem

        private ThemeItemViewModels<T, Models.ThemeItem<T>> _selectedItem;

        public ThemeItemViewModels<T, Models.ThemeItem<T>> SelectedItem
        {
            get { return this._selectedItem; }
            set
            {
                if (this._selectedItem != value)
                {
                    if (this._selectedItem != null) this._selectedItem.IsSelected = false;
                    if (value != null) value.IsSelected = true;
                    this._selectedItem = value;
                    this.RaisePropertyChanged();
                    this.OnSelectedChange();
                }
            }
        }

        #endregion

        public Func<IList<ThemeItemViewModels<T, Models.ThemeItem<T>>>> GetListFunc { get; set; }

        public event EventHandler<ThemeItemViewModels<T, Models.ThemeItem<T>>> SelectedChange;

        protected virtual void OnSelectedChange()
        {
            if (SelectedChange != null)
            {
                SelectedChange(this, this.SelectedItem);
            }
        }
    }
}
