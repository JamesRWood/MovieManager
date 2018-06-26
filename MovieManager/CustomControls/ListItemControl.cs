namespace MovieManager.CustomControls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class ListItemControl : Control
    {
        static ListItemControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListItemControl), new FrameworkPropertyMetadata(typeof(ListItemControl)));
        }

        public static readonly DependencyProperty ListItemWidthProperty = DependencyProperty.Register("ListItemWidth", typeof(double), typeof(ListItemControl), new UIPropertyMetadata(null));
        public static readonly DependencyProperty ListItemDisplayTextProperty = DependencyProperty.Register("ListItemDisplayText", typeof(string), typeof(ListItemControl), new UIPropertyMetadata(null));
        public static readonly DependencyProperty ListItemTextStyleProperty = DependencyProperty.Register("ListItemTextStyle", typeof(Style), typeof(ListItemControl), new UIPropertyMetadata(null));
        public static readonly DependencyProperty ListItemBackgroundBrushProperty = DependencyProperty.Register("ListItemBackgroundBrush", typeof(SolidColorBrush), typeof(ListItemControl), new UIPropertyMetadata(null));
        public static readonly DependencyProperty ListItemForegroundBrushProperty = DependencyProperty.Register("ListItemForegroundBrush", typeof(SolidColorBrush), typeof(ListItemControl), new UIPropertyMetadata(null));

        public double ListItemWidth
        {
            get => (double) GetValue(ListItemWidthProperty);
            set => SetValue(ListItemWidthProperty, value);
        }

        public string ListItemDisplayText
        {
            get => (string)GetValue(ListItemDisplayTextProperty);
            set => SetValue(ListItemDisplayTextProperty, value);
        }

        public Style ListItemTextStyle
        {
            get => (Style)GetValue(ListItemTextStyleProperty);
            set => SetValue(ListItemTextStyleProperty, value);
        }

        public SolidColorBrush ListItemBackgroundBrush
        {
            get => (SolidColorBrush)GetValue(ListItemBackgroundBrushProperty);
            set => SetValue(ListItemBackgroundBrushProperty, value);
        }

        public SolidColorBrush ListItemForegroundBrush
        {
            get => (SolidColorBrush)GetValue(ListItemForegroundBrushProperty);
            set => SetValue(ListItemForegroundBrushProperty, value);
        }
    }
}
