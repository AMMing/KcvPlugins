using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Logger.Modes
{
    public interface IResult
    {
        string Id { get; set; }
        DateTime CreateDate { get; set; }
    }
}
