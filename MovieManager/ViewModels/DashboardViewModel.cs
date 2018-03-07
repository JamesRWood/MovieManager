namespace MovieManager.ViewModels
{
    using Autofac;
    using Contracts;
    using Contracts.Controllers;
    using Models;
    using MovieManager.Contracts.Commands;
    using System.Collections.Generic;
    using System.Windows.Input;

    public class DashboardViewModel : IDashboardViewModel
    {
        private readonly ICommonDataViewModel _commonDataViewModel;
        private readonly IScanForLocalMovieFilesCommand _scanForLocalMovieFilesCommand;

        public DashboardViewModel()
        {
            _commonDataViewModel = AutofacInstaller.Container.Resolve<ICommonDataViewModel>();

            _scanForLocalMovieFilesCommand = AutofacInstaller.Container.Resolve<IScanForLocalMovieFilesCommand>();

            var movieController = AutofacInstaller.Container.Resolve<IMovieController>();

            Movies = movieController.GetMovieDataFromLocalLibraryFile();
        }

        public IList<Movie> Movies { get; set; }

        public ICommand ScanForLocalMovieFilesCommand => _scanForLocalMovieFilesCommand.Command;
    }
}
