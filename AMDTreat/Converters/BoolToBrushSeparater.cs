using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace AMDTreat.Converters
{
    public class BoolToBrushSeparater : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isBadValue = (bool)value;

            if (isBadValue)
            {
                return Brushes.Red;
            }
            else
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffa0a0a0"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
