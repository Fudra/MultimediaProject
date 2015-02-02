using System;
using System.Globalization;
using System.Windows.Data;

namespace EiMM.View.Converter
{
    [ValueConversion(typeof(bool?), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = (bool?)value;
            return !(b.HasValue && b.Value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = (bool?)value;
            return !(b.HasValue && b.Value);
        }
    }
}