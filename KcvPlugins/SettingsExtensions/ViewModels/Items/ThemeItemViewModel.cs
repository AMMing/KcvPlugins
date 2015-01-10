using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AMing.SettingsExtensions.ViewModels.Items
{
    public class ThemeItemViewModel<T, T_Models> : SelectedItemViewModel
        where T_Models : Models.ThemeItem<T>
    {
        public ThemeItemViewModel(T_Models models)
        {
            this.Type = models.Type;
            this.BackgroundColor = models.BackgroundColor;
            this.ForegroundColor = models.ForegroundColor;
        }

        public Color BackgroundColor { get; set; }
        public Color ForegroundColor { get; set; }

        public T Type { get; set; }
    }
}
