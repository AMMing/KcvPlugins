using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.SettingsExtensions.ViewModels.Items
{
    public class FeetStyleTypeViewModel : SelectedItemViewModel
    {
        public FeetStyleTypeViewModel(Enums.FeetStyleType type)
        {
            this.Type = type;
        }
        public Enums.FeetStyleType Type { get; set; }
    }
}
