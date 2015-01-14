using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.SettingsExtensions.ViewModels.Items
{
    public class KeyModulesItemViewModel : SelectedItemViewModel
    {
        public KeyModulesItemViewModel(Models.KeyModulesItem keyModulesItem)
        {
            this.KeyModulesItem = keyModulesItem;
        }
        public Models.KeyModulesItem KeyModulesItem { get; set; }

        public Action<KeyModulesItemViewModel> SetKeyAction { get; set; }

        public void SetKey(KeyModulesItemViewModel viewModel)
        {
            if (SetKeyAction != null)
                SetKeyAction(viewModel);
        }
    }
}
