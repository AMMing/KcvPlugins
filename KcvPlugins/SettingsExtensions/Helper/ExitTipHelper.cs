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

namespace AMing.SettingsExtensions.Helper
{
    public class ExitTipHelper
    {
        #region Current

        private static ExitTipHelper _current = new ExitTipHelper();

        public static ExitTipHelper Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion


        public void Init()
        {
            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);
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

            UIHelper.GetControl<Grid>(exitDialog.Content, grid =>
            {
                if (grid.Children != null)
                {
                    bool get_tb = false,
                            get_sp = false;
                    foreach (var item in grid.Children)
                    {
                        if (!get_tb)
                        {
                            UIHelper.GetControl<TextBlock>(item, tb =>
                            {
                                get_tb = true;
                                tb.Text = TextResource.Exit_Msg_Content;
                                tb.Margin = new Thickness(10, 20, 0, 0);
                            });
                        }
                        if (!get_sp)
                        {
                            UIHelper.GetControl<StackPanel>(item, sp =>
                            {
                                get_sp = true;
                                if (sp.Children != null && sp.Children.Count == 2)
                                {
                                    UIHelper.GetControl<CallMethodButton>(sp.Children[0], btn_ok =>
                                    {
                                        btn_ok.Content = TextResource.Exit_Msg_Button_Yes;
                                        btn_ok.Click += (btn_ok_sender, btn_ok_e) =>
                                        {
                                            exitDialog.DialogResult = true;
                                            exitDialog.Close();
                                        };
                                    });
                                    UIHelper.GetControl<CallMethodButton>(sp.Children[1], btn_cancel =>
                                    {
                                        btn_cancel.Content = TextResource.Exit_Msg_Button_No;
                                        btn_cancel.Click += (btn_cancel_sender, btn_cancel_e) =>
                                        {
                                            exitDialog.DialogResult = false;
                                            exitDialog.Close();
                                        };
                                    });
                                }
                                else
                                {
                                    throw new Exception("ExitDialog is different versions.");
                                }
                            });
                        }
                    }
                }

            });
        }

    }
}
