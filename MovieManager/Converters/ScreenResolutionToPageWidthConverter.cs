namespace MovieManager.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ScreenResolutionToPageWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var width = 1280.00;
            if (parameter is double)
            {
                var screenWidth = (double)parameter;
                if (screenWidth > width)
                {
                    return width;
                }

                width = screenWidth * 0.8;
            }

            return (int)width;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
