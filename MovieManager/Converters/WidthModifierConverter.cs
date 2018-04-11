namespace MovieManager.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class WidthModifierConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var width = (double)values[0];
            var modifier = (int)values[1];

            return width * modifier;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
