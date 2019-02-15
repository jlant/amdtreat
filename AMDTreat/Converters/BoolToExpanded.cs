using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace AMDTreat.Converters
{
    public class BoolToExpanded : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {


            return values;
            //var isExpanded = (bool)values[0];
            //var isError = (bool)values[1];

            //if (isError)
            //{
            //    return true;
            //}
            //else
            //{
            //    return isExpanded;
            //}

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new[] { value, DependencyProperty.UnsetValue, }; ;

        }


        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //{

        //    if ((bool)value)
        //    {
        //        return true;
        //    }

        //    return (bool)value;

        //}

        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    return (bool)value;
        //}
    }
}
