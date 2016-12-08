using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Data;

namespace Football
{
    [ValueConversion(typeof(string), typeof(string))]
    class StringUpperConverter:IValueConverter
    {
        private string text;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            text = value as string;
            if (text != null)
            {
                return text.ToUpper();
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return text;
        }
    }
}