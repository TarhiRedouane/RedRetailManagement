using System;
using System.Globalization;
using System.Windows;

namespace RRMCustomControls.ValueConverters
{
    /// <summary>
    /// a converter that takes in a boolean and return a <see cref="Visibility"/>
    /// </summary>
    public class ReverseBooleanToVisibilityConverter : BaseValueConverter<ReverseBooleanToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}