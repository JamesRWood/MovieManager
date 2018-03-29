namespace MovieManager.Converters
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.Net;
    using System.Windows.Data;
    using Models;

    public class ImageToMajorityColourRgbConverter : IValueConverter
    {
        private static System.Windows.Media.Color Black => System.Windows.Media.Color.FromArgb(Color.DarkSlateGray.A, Color.DarkSlateGray.R, Color.DarkSlateGray.G, Color.DarkSlateGray.B);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Movie movie))
            {
                return Black;
            }

            return string.IsNullOrEmpty(movie.ImagePath) ? Black : GetColor(movie.ImagePath);
        }

        private static unsafe System.Windows.Media.Color GetColor(string filePath)
        {
            filePath = filePath.StartsWith("\\") ? filePath.Substring(1) : filePath;
            var req = WebRequest.Create(new Uri("https://image.tmdb.org/t/p/w780" + filePath));
            var responseStream = req.GetResponse().GetResponseStream();

            if (responseStream == null)
            {
                return Black;
            }

            using (var image = (Bitmap) Image.FromStream(responseStream))
            {
                if (image.PixelFormat != PixelFormat.Format24bppRgb)
                {
                    throw new NotSupportedException($"Unsupported pixel format: {image.PixelFormat}");
                }

                var bounds = new Rectangle(0, 0, image.Width, image.Height);
                var data = image.LockBits(bounds, ImageLockMode.ReadOnly, image.PixelFormat);

                long r = 0;
                long g = 0;
                long b = 0;

                const int pixelSize = 3;
                for (int y = 0; y < data.Height; ++y)
                {
                    byte* row = (byte*) data.Scan0 + (y * data.Stride);
                    for (int x = 0; x < data.Width; ++x)
                    {
                        var pos = x * pixelSize;
                        b += row[pos];
                        g += row[pos + 1];
                        r += row[pos + 2];
                    }
                }

                r = r / (data.Width * data.Height);
                g = g / (data.Width * data.Height);
                b = b / (data.Width * data.Height);
                image.UnlockBits(data);

                Color colour;
                if (r >= g && r >= b)
                {
                    colour = Color.FromArgb((int)r, 0, 0);
                }
                else if (g >= r && g >= b)
                {
                    colour = Color.FromArgb(0, (int)g, 0);
                }
                else
                {
                    colour = Color.FromArgb(0, 0, (int)b);
                }
                
                //var colour = Color.FromArgb((int)r, (int)g, (int)b);
                return System.Windows.Media.Color.FromArgb(colour.A, colour.R, colour.G, colour.B);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
