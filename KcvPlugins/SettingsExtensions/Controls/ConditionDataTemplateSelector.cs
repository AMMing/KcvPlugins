using Grabacr07.KanColleWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AMing.SettingsExtensions.Controls
{
    public class ConditionDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Brilliant { get; set; }
        public DataTemplate Normal { get; set; }
        public DataTemplate Tired { get; set; }
        public DataTemplate OrangeTired { get; set; }
        public DataTemplate RedTired { get; set; }

        public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var type = (ConditionType)item;
            switch (type)
            {
                case ConditionType.Brilliant:
                    return this.Brilliant;
                case ConditionType.Normal:
                    return this.Normal;
                case ConditionType.Tired:
                    return this.Tired;
                case ConditionType.OrangeTired:
                    return this.OrangeTired;
                case ConditionType.RedTired:
                    return this.RedTired;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
