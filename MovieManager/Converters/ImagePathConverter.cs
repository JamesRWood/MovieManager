namespace MovieManager.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            path = path.StartsWith("\\") ? path.Substring(1) : path;
            var uri = new Uri("https://image.tmdb.org/t/p/w780" + path);
            return uri;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
