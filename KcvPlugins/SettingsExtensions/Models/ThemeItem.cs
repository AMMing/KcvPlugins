using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AMing.SettingsExtensions.Models
{
    public class ThemeItem<T>
    {

        public Color BackgroundColor { get; set; }
        public Color ForegroundColor { get; set; }

        public T Type { get; set; }
    }
}
