namespace MovieManager
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media.Animation;

    public partial class MediaPlayer : Window
    {
        public MediaPlayer(string filePath)
        {
            InitializeComponent();
            
            VlcPlayer.LoadMedia(filePath);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            VlcPlayer.Dispose();
        }

        private void Button_Click_Stop(object sender, RoutedEventArgs e)
        {
            VlcPlayer.Stop();
            VlcPlayer.Dispose();
            Close();
        }

        private void Button_Click_Pause(object sender, RoutedEventArgs e)
        {
            VlcPlayer.PauseOrResume();
        }

        private void Button_Click_Play(object sender, RoutedEventArgs e)
        {
            VlcPlayer.Play();
        }

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            var animation = new DoubleAnimation
            {
                From = 0.0,
                To = 0.4,
                Duration = TimeSpan.FromSeconds(0.2)
            };

            MediaPlayerControlsPanel.BeginAnimation(OpacityProperty, animation);
        }

        private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var animation = new DoubleAnimation
            {
                From = 0.4,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(0.2)
            };

            MediaPlayerControlsPanel.BeginAnimation(OpacityProperty, animation);
        }
    }
}
