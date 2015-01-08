using AMing.SettingsExtensions.Helper;
using Grabacr07.Desktop.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using kcv = Grabacr07.KanColleViewer;

namespace AMing.SettingsExtensions.Modules
{
    public class ExitTipModules : ModulesBase
    {
        #region Current

        private static ExitTipModules _current = new ExitTipModules();

        public static ExitTipModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        public override void Initialize()
        {
            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);
        }
        public override void Dispose()
        {
        }

        void MainWindow_Closing(object o, CancelEventArgs e)
        {
            if (!Data.Settings.Current.EnableExitTip)
            {
                return;
            }

            try
            {
                var exit = new kcv.Views.ExitDialog();
                SetExitDialog(exit);
                if (!exit.ShowDialog() ?? false)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception)
            {
                if (MessageBox.Show(
                    TextResource.Exit_Msg_Content,
                    TextResource.Exit_Msg_Title,
                    MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }


        /// <summary>
        /// 完善kcv半成品的ExitDialog
        /// </summary>
        /// <param name="exitDialog"></param>
        void SetExitDialog(kcv.Views.ExitDialog exitDialog)
        {
            exitDialog.Title = TextResource.Exit_Msg_Title;
            exitDialog.Width = 400;
            exitDialog.Height = 120;
            exitDialog.ResizeMode = ResizeMode.NoResize;
            exitDialog.Icon = Application.Current.MainWindow.Icon;

            UIHelper.GetControl<Grid>(exitDialog.Content, grid =>
            {
                if (grid.Children != null)
                {
                    #region get controls

                    var tb_content = grid.Children.OfType<TextBlock>().First();
                    var sp_btns = grid.Children.OfType<StackPanel>().First();
                    var btns = sp_btns.Children.OfType<CallMethodButton>().ToList();
                    var btn_ok = btns[0];
                    var btn_cancel = btns[1];

                    #endregion
                    #region content

                    tb_content.Text = TextResource.Exit_Msg_Content;
                    tb_content.Margin = new Thickness(10, 20, 0, 0);

                    #endregion
                    #region btn_ok

                    btn_ok.Content = TextResource.Exit_Msg_Button_Yes;
                    btn_ok.Click += (btn_ok_sender, btn_ok_e) =>
                    {
                        exitDialog.DialogResult = true;
                        exitDialog.Close();
                    };

                    #endregion
                    #region btn_cancel

                    btn_cancel.Content = TextResource.Exit_Msg_Button_No;
                    btn_cancel.Click += (btn_cancel_sender, btn_cancel_e) =>
                    {
                        exitDialog.DialogResult = false;
                        exitDialog.Close();
                    };
                    btn_cancel.Focus();

                    #endregion
                }
            });
        }

    }
}
