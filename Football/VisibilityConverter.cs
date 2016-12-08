using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Data;

namespace Football
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    class VisibilityConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool) value ? Visibility.Visible : Visibility.Hidden; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (Equals(value, Visibility.Visible))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}