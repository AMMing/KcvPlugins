using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.SettingsExtensions.ViewModels.Items
{
    public class WindowViewTypeViewModel : SelectedItemViewModel
    {
        public WindowViewTypeViewModel(Enums.WindowViewType type)
        {
            this.Type = type;
        }
        public Enums.WindowViewType Type { get; set; }
    }
}
