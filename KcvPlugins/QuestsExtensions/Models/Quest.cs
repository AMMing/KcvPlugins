using Grabacr07.KanColleWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.QuestsExtensions.Models
{
    [Serializable]
    public class Quest
    {
        public int Id { get; set; }
        public int Type { get; set; }

        public int Category { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

    }
}
