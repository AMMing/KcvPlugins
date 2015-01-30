using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMing.Plugins.Core.Helper
{
    public class MessageBoxDialog
    {
        public static void Show(string messageBoxText)
        {
            try
            {
                Views.MessageBoxDialog messageBoxDialog = new Views.MessageBoxDialog(messageBoxText);
                messageBoxDialog.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show(messageBoxText);
            }
        }

        public static bool Show(string messageBoxText, string caption)
        {
            try
            {
                Views.MessageBoxDialog messageBoxDialog = new Views.MessageBoxDialog(messageBoxText, caption);
                return messageBoxDialog.ShowDialog() ?? false;
            }
            catch (Exception)
            {
                return MessageBox.Show(messageBoxText, caption, MessageBoxButton.OKCancel) == MessageBoxResult.OK;
            }
        }
    }
}
