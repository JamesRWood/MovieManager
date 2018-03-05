namespace MovieManager.cs
{
    using System.Configuration;
    using System.Windows;
    using Autofac;
    using Contracts;
    using DM.MovieApi;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            AutofacInstaller.RegisterComponents();

            InitializeComponent();
            
            MovieDbFactory.RegisterSettings(ConfigurationManager.AppSettings["ApiKey"]);

            //DasbDashboardViewModel = container.Resolve<IDashboardViewModel>();
        }

        //public IDashboardViewModel DasbDashboardViewModel { get; set; }
    }
}
