using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Core.Models
{
    public class MessageItem
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return string.Format("Title:{0}\nContent:{1}\n", this.Title, this.Content);
        }
    }
}
