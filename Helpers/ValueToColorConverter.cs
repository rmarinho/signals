using System;
using System.Globalization;

namespace signals.Helpers;

public class ValueToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return null;
        }
              if (value is double numericValue)
        {
            if (numericValue > 0)
            {
                return Color.FromArgb("#21b559"); // Green for values greater than 0
            }
            else if (numericValue < 0)
            {
                return Color.FromArgb("#f87171"); // Red for values less than 0
            }
            else
            {
                return Colors.Gray; // Gray for 0
            }
        }

        throw new ArgumentException("Value must be a numeric type.");
   
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value; // Not implemented
    }
}
