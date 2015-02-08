using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Logger.Modes
{
    public interface ILogsList<T>
    {
        DateTime UpdateDate { get; set; }

        T[] List { get; set; }
    }
}
