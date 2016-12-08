using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Data;
using System.Reflection;
using System.ComponentModel;

namespace Football
{
    [ValueConversion(typeof(Enum), typeof(string))]
    class EnumConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value.ToString() == string.Empty)
            {
                return DependencyProperty.UnsetValue;
            }
            else
            {
                Enum temp = (Enum)value;
                Type type = value.GetType();
                MemberInfo[] memInfo = type.GetMember(temp.ToString());
                if (memInfo != null && memInfo.Length > 0)
                {
                    object[] atr = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (atr != null && atr.Length > 0)
                    {
                        return ((DescriptionAttribute)atr[0]).Description;
                    }
                }
                return temp.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}