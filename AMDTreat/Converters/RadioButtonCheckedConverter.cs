using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AMDTreat.Converters
{
    /// <summary>
    /// This converter is used to bind a value to a group of radio buttons, and can be used with an enum, bool, int, etc.
    /// It depends on the converter parameter, which maps a radio button to a specific value.
    /// Note: this converter is not designed to work with flag enums (e.g. multiple checkboxes scenario).
    /// 
    /// Here is an example of the binding for an enum value:
    /// <RadioButton Content="Option 1" IsChecked="{Binding EnumValue, Converter={StaticResource EnumToRadioBoxCheckedConverter}, 
    ///     ConverterParameter={x:Static local:TestEnum.Option1}}" GroupName="EnumGroup"/>
    /// <RadioButton Content="Option 2" IsChecked="{Binding EnumValue, Converter={StaticResource EnumToRadioBoxCheckedConverter}, 
    ///     ConverterParameter={x:Static local:TestEnum.Option2}}" GroupName="EnumGroup"/>
    ///     
    public class RadioButtonCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string parameterString = parameter as string;
            object parameterValue = Enum.Parse(value.GetType(), parameterString);

            return value.Equals(parameterValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
