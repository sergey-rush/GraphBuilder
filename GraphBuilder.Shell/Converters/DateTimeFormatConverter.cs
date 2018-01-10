using System;
using System.Globalization;
using System.Windows.Data;

namespace GraphBuilder.Shell.Converters
{
    public class DateTimeFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((DateTime)value == DateTime.MinValue)
                return string.Empty;
            else
                return ((DateTime)value).ToString((string)parameter);
            //string carddate = ((DateTime)value).ToString("F");
            //return carddate;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}