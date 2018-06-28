namespace MovieManager
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public partial class MediaPlayer : Window
    {
        public MediaPlayer(string filePath)
        {
            InitializeComponent();
            
            VlcPlayer.LoadMedia(filePath);
            
            VlcPlayer.Stretch = Stretch.UniformToFill;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            VlcPlayer.Dispose();
        }

        private void Button_Click_Stop(object sender, RoutedEventArgs e)
        {
            VlcPlayer.Stop();
        }

        private void Button_Click_Pause(object sender, RoutedEventArgs e)
        {
            VlcPlayer.PauseOrResume();
        }

        private void Button_Click_Play(object sender, RoutedEventArgs e)
        {
            VlcPlayer.Play();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            VlcPlayer.Stop();
            VlcPlayer.Dispose();
            Close();
        }

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            MediaPlayerControlsPanel.Visibility = Visibility.Visible;
        }

        private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
        {
            MediaPlayerControlsPanel.Visibility = Visibility.Collapsed;
        }
    }
}
