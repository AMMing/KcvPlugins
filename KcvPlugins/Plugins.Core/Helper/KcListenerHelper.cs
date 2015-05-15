using Fiddler;
using Grabacr07.KanColleWrapper.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper;

namespace AMing.Plugins.Core.Helper
{
    public static class KcListenerHelper
    {
        public static PropertyChangedEventHandler PropertyChangedEventListener_Try(PropertyChangedEventHandler func)
        {
            return (sender, e) =>
            {
                try
                {
                    func(sender, e);
                }
                catch (Exception ex)
                {
                    GenericMessager.Current.SendToException(ex);
                }
            };
        }
        public static IObservable<SvData<TResult>> SessionTryParse<TResult>(this IObservable<Session> source)
        {
            try
            {
                return source.TryParse<TResult>();
            }
            catch (Exception ex)
            {
                GenericMessager.Current.SendToException(ex);
                return null;
            }
        }
    }
}
