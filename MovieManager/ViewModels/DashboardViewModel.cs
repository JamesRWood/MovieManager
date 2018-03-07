namespace MovieManager.ViewModels
{
    using System.Collections.Generic;
    using Autofac;
    using Contracts;
    using Contracts.Controllers;
    using Models;

    public class DashboardViewModel : IDashboardViewModel
    {
        public DashboardViewModel()
        {
            var movieController = AutofacInstaller.Container.Resolve<IMovieController>();

            Movies = movieController.GetMovieDataFromLocalLibraryFile();
        }

        public IList<Movie> Movies { get; set; }
    }
}
