namespace MovieManager.Helpers
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Net;
    using Models;
    using Color = System.Windows.Media.Color;
    using PixelFormat = System.Drawing.Imaging.PixelFormat;

    public static class GetImageMajorityColorHelper
    {
        public static unsafe Color GetImageMajorityColor<T>(this T movie) where T : Movie
        {
            var filePath = movie.ImagePath.StartsWith("\\") ? movie.ImagePath.Substring(1) : movie.ImagePath;
            var req = WebRequest.Create(new Uri("https://image.tmdb.org/t/p/w780" + filePath));
            var responseStream = req.GetResponse().GetResponseStream();

            if (responseStream == null)
            {
                return Core.TransparentColor;
            }

            using (var image = (Bitmap)Image.FromStream(responseStream))
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
                    byte* row = (byte*)data.Scan0 + (y * data.Stride);
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

                System.Drawing.Color colour;
                if (r >= g && r >= b)
                {
                    colour = System.Drawing.Color.FromArgb((int)r, 0, 0);
                }
                else if (g >= r && g >= b)
                {
                    colour = System.Drawing.Color.FromArgb(0, (int)g, 0);
                }
                else
                {
                    colour = System.Drawing.Color.FromArgb(0, 0, (int)b);
                }

                return Color.FromArgb(colour.A, colour.R, colour.G, colour.B);
            }
        }
    }
}
