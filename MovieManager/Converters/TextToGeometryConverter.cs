namespace MovieManager.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;

    public class TextToGeometryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string textValue))
            {
                return null;
            }
            
            var formattedText = new FormattedText(
                textValue,
                CultureInfo.CurrentCulture, 
                FlowDirection.LeftToRight,
                new Typeface(
                    new FontFamily("Calibri"),
                    FontStyles.Normal,
                    FontWeights.ExtraBold, 
                    FontStretches.Normal),
                12,
                Brushes.Black, 
                20);
            
            return formattedText.BuildGeometry(new Point(0, 0));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
