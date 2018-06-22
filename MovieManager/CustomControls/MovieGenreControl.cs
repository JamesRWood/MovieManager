namespace MovieManager.CustomControls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class MovieGenreControl : Control
    {
        static MovieGenreControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MovieGenreControl),
                new FrameworkPropertyMetadata(typeof(MovieGenreControl)));
        }

        public static readonly DependencyProperty MovieGenreDisplayTextProperty = DependencyProperty.Register("MovieGenreDisplayText", typeof(string), typeof(MovieGenreControl), new UIPropertyMetadata(null));
        public static readonly DependencyProperty MovieGenreTextStyleProperty = DependencyProperty.Register("MovieGenreTextStyle", typeof(Style), typeof(MovieGenreControl),new UIPropertyMetadata(null));
        public static readonly DependencyProperty MovieGenreBackgroundBrushProperty = DependencyProperty.Register("MovieGenreBackgroundBrush", typeof(SolidColorBrush), typeof(MovieGenreControl),new UIPropertyMetadata(null));
        public static readonly DependencyProperty MovieGenreForegroundBrushProperty = DependencyProperty.Register("MovieGenreForegroundBrush", typeof(VisualBrush), typeof(MovieGenreControl),new UIPropertyMetadata(null));

        public string MovieGenreDisplayText
        {
            get => (string) GetValue(MovieGenreDisplayTextProperty);
            set => SetValue(MovieGenreDisplayTextProperty, value);
        }

        public Style MovieGenreTextStyle
        {
            get => (Style) GetValue(MovieGenreTextStyleProperty);
            set => SetValue(MovieGenreTextStyleProperty, value);
        }

        public SolidColorBrush MovieGenreBackgroundBrush
        {
            get => (SolidColorBrush) GetValue(MovieGenreBackgroundBrushProperty);
            set => SetValue(MovieGenreBackgroundBrushProperty, value);
        }

        public VisualBrush MovieGenreForegroundBrush
        {
            get => (VisualBrush) GetValue(MovieGenreForegroundBrushProperty);
            set => SetValue(MovieGenreForegroundBrushProperty, value);
        }
    }
}
