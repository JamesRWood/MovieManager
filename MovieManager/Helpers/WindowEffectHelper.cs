namespace MovieManager.Helpers
{
    using System.Windows;
    using System.Windows.Media.Effects;

    public static class WindowEffectHelper
    {
        public static void ToggleBlurEffect<T>(this T window) where T : Window
        {
            BlurEffect blur;
            var hasBlurEffect = window.Effect is BlurEffect;
            if (!hasBlurEffect)
            {
                blur = new BlurEffect
                {
                    Radius = 50,
                    RenderingBias = RenderingBias.Quality
                };
            }
            else
            {
                blur = null;
            }

            window.Effect = blur;
        }
    }
}
