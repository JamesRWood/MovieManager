namespace MovieManager.cs
{
    using System.Configuration;
    using System.Windows;
    using DM.MovieApi;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            MovieDbFactory.RegisterSettings(ConfigurationManager.AppSettings["ApiKey"]);

            AutofacInstaller.RegisterComponents();

            InitializeComponent();
        }
    }
}
