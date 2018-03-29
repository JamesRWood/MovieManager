namespace MovieManager.Converters
{
    using MovieManager.Models;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class MovieTitleHasValueToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (Movie)value;
            if (val == null)
            {
                return Visibility.Hidden;
            }

            return !string.IsNullOrEmpty(val.Title) ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
