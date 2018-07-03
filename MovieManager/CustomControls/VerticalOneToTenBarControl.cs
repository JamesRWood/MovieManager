namespace MovieManager.CustomControls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class VerticalOneToTenBarControl : Control
    {
        static VerticalOneToTenBarControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VerticalOneToTenBarControl), new FrameworkPropertyMetadata(typeof(VerticalOneToTenBarControl)));
        }

        public static readonly DependencyProperty DisplayValueProperty = DependencyProperty.Register("DisplayValue", typeof(double?), typeof(VerticalOneToTenBarControl), new UIPropertyMetadata(null));
        public static readonly DependencyProperty DisplayTextProperty = DependencyProperty.Register("DisplayText", typeof(string), typeof(VerticalOneToTenBarControl), new UIPropertyMetadata(null));
        public static readonly DependencyProperty VisibilityIndicatorProperty = DependencyProperty.Register("VisibilityIndicator", typeof(Visibility), typeof(VerticalOneToTenBarControl), new UIPropertyMetadata(null));
        public static readonly DependencyProperty HeaderStyleProperty = DependencyProperty.Register("HeaderStyle", typeof(Style), typeof(VerticalOneToTenBarControl), new UIPropertyMetadata(null));
        public static readonly DependencyProperty TextStyleProperty = DependencyProperty.Register("TextStyle", typeof(Style), typeof(VerticalOneToTenBarControl), new UIPropertyMetadata(null));
        public static readonly DependencyProperty BackgroundBrushProperty = DependencyProperty.Register("BackgroundBrush", typeof(VisualBrush), typeof(VerticalOneToTenBarControl), new UIPropertyMetadata(null));
        public static readonly DependencyProperty ForegroundBrushProperty = DependencyProperty.Register("ForegroundBrush", typeof(SolidColorBrush), typeof(VerticalOneToTenBarControl), new UIPropertyMetadata(null));

        public double? DisplayValue
        {
            get => (double?) GetValue(DisplayValueProperty);
            set => SetValue(DisplayValueProperty, value);
        }

        public string DisplayText
        {
            get => (string)GetValue(DisplayTextProperty);
            set => SetValue(DisplayTextProperty, value);
        }

        public Visibility VisibilityIndicator
        {
            get => (Visibility) GetValue(VisibilityIndicatorProperty);
            set => SetValue(VisibilityIndicatorProperty, value);
        }

        public Style HeaderStyle
        {
            get => (Style) GetValue(HeaderStyleProperty);
            set => SetValue(HeaderStyleProperty, value);
        }

        public Style TextStyle
        {
            get => (Style) GetValue(TextStyleProperty);
            set => SetValue(TextStyleProperty, value);
        }

        public VisualBrush BackgroundBrush
        {
            get => (VisualBrush) GetValue(BackgroundBrushProperty);
            set => SetValue(BackgroundBrushProperty, value);
        }

        public SolidColorBrush ForegroundBrush
        {
            get => (SolidColorBrush) GetValue(ForegroundBrushProperty);
            set => SetValue(ForegroundBrushProperty, value);
        }
    }
}
