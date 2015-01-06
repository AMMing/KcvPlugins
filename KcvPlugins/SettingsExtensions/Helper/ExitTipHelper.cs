using Grabacr07.Desktop.Metro.Controls;
using System;
using System.Collections.Generic;
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



        static bool isInit = false;

        public void Init()
        {
            if (isInit)
            {
                return;
            }
            isInit = true;

            kcv.App.Current.MainWindow.Closing += new System.ComponentModel.CancelEventHandler(KanColleViewer_Closing);
        }
        void KanColleViewer_Closing(object o, System.ComponentModel.CancelEventArgs e)
        {
            if (!Data.Settings.Current.Enable_ExitTip)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (MessageBox.Show(
                    TextResource.Exit_Msg_Content,
                    TextResource.Exit_Msg_Title,
                    MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }



        void SetExitDialog(kcv.Views.ExitDialog exitDialog)
        {
            exitDialog.Title = TextResource.Exit_Msg_Title;

            GetControl<Grid>(exitDialog.Content, grid =>
            {
                if (grid.Children != null)
                {
                    bool get_tb = false,
                         get_sp = false;
                    foreach (var item in grid.Children)
                    {
                        if (!get_tb)
                        {
                            GetControl<TextBlock>(item, tb =>
                            {
                                get_tb = true;
                                tb.Text = TextResource.Exit_Msg_Content;
                                tb.Margin = new Thickness(10, 20, 0, 0);
                            });
                        }
                        if (!get_sp)
                        {
                            GetControl<StackPanel>(item, sp =>
                            {
                                get_sp = true;
                                if (sp.Children != null && sp.Children.Count == 2)
                                {
                                    GetControl<CallMethodButton>(sp.Children[0], btn_ok =>
                                    {
                                        btn_ok.Click += (btn_ok_sender, btn_ok_e) =>
                                        {
                                            exitDialog.DialogResult = true;
                                            exitDialog.Close();
                                        };
                                    });
                                    GetControl<CallMethodButton>(sp.Children[1], btn_cancel =>
                                    {
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

        public void GetControl<T>(object obj, Action<T> callback)
            where T : UIElement
        {
            if (obj is T)
            {
                callback(obj as T);
            }

        }
    }
}
