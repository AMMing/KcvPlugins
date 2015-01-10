using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AMing.SettingsExtensions.ViewModels.Collections
{
    public class ListViewModels<T> : ViewModel
        where T : Items.SelectedItemViewModel
    {
        #region List

        protected IList<T> _list;

        public virtual IList<T> List
        {
            get
            {
                if (this._list == null && GetListFunc != null)
                {
                    var result = GetListFunc();

                    this.List = result;
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

        protected T _selectedItem;

        public virtual T SelectedItem
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


        public Func<IList<T>> GetListFunc { get; set; }

        public event EventHandler<T> SelectedChange;

        protected virtual void OnSelectedChange()
        {
            if (SelectedChange != null)
            {
                SelectedChange(this, this.SelectedItem);
            }
        }
    }
}
