using Livet;
using Livet.Commands;
using MetroRadiance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AMing.Plugins.Core.ViewModels
{
    public class ViewModelEx : ViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propValue"></param>
        /// <param name="newValue"></param>
        /// <param name="propertyName"></param>
        /// <returns>value is change</returns>
        protected virtual bool RaisePropertyChanged<T>(ref T propValue, T newValue, [CallerMemberName]string propertyName = null)
        {
            if (object.Equals(propValue, newValue)) return false;

            propValue = newValue;
            RaisePropertyChanged(propertyName);

            return true;
        }
    }
}
