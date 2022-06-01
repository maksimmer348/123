using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TelerikWpfApp2;

public class BoolToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return Brushes.Transparent;
        return System.Convert.ToBoolean(value) ? Brushes.Red : Brushes.Green;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}