using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.WindowsNotifierEx
{
    public struct ToastMessage
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Enums.ToastType Type { get; set; }
    }
}
