using System;
using System.Globalization;
using System.Windows.Data;

namespace CsharpWpf.chapter1
{
    internal class MyValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double c = (double)value;
            if(c == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
