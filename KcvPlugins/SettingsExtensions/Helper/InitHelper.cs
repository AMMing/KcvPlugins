using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMing.SettingsExtensions.Helper
{
    public class InitHelper
    {
        #region Current

        private static InitHelper _current = new InitHelper();

        public static InitHelper Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        bool isappinit = false;
        private void AppInit()
        {
            Helper.ExitTipHelper.Current.Init();
            Helper.NotifyIconHelper.Current.Init();
        }


        public void Init()
        {
            if (isappinit)
            {
                return;
            }
            isappinit = true;
            AppInit();
        }
    }
}
