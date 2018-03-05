namespace MovieManager.cs.ViewModels
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

            //Movies = movieController.GetAllMovies();

            Movies = new List<Movie>
            {
                new Movie
                {
                    Title = "King Kong"
                }
            };
        }

        public ICollection<Movie> Movies { get; set; }
    }
}
