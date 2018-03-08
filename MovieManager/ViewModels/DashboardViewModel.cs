namespace MovieManager.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Autofac;
    using Contracts;
    using Contracts.Controllers;
    using Models;
    using MovieManager.Contracts.Commands;

    public class DashboardViewModel : IDashboardViewModel
    {
        private readonly ICommonDataViewModel _commonDataViewModel;
        private readonly IScanForLocalMovieFilesCommand _scanForLocalMovieFilesCommand;

        public DashboardViewModel()
        {
            _commonDataViewModel = AutofacInstaller.Container.Resolve<ICommonDataViewModel>();

            _scanForLocalMovieFilesCommand = AutofacInstaller.Container.Resolve<IScanForLocalMovieFilesCommand>();

            var movieController = AutofacInstaller.Container.Resolve<IMovieController>();

            Movies = movieController.GetMovieDataFromLocalLibraryFile().ToObservableCollection();
        }

        public ObservableCollection<Movie> Movies { get; set; }

        public ICommand ScanForLocalMovieFilesCommand => _scanForLocalMovieFilesCommand.Command;
    }
}
