using AMing.SettingsExtensions.ViewModels.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.SettingsExtensions.ViewModels.Collections
{
    public class ThemeListViewModels<T> : ListViewModels<ThemeItemViewModel<T, Models.ThemeItem<T>>>
    {
    }
}
