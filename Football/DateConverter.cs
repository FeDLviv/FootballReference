using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Data;

namespace Football
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    class DateConverter:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime temp = (DateTime) value;
            return temp.ToString("dd.MM.yyyy   HH:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DateTime.ParseExact((string) value, "dd.MM.yyyy HH:mm:ss", null);
        }
    }
}