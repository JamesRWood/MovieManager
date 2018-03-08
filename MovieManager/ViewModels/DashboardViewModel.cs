namespace MovieManager.ViewModels
{
    using Autofac;
    using Contracts;
    using Contracts.Controllers;
    using MovieManager.Contracts.Commands;
    using MovieManager.Helpers;
    using MovieManager.Models;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;

    public class DashboardViewModel : IDashboardViewModel, INotifyPropertyChanged
    {
        private readonly IScanForLocalMovieFilesCommand _scanForLocalMovieFilesCommand;
        private ObservableCollection<Movie> _movies;

        public DashboardViewModel()
        {
            _scanForLocalMovieFilesCommand = AutofacInstaller.Container.Resolve<IScanForLocalMovieFilesCommand>();

            var movieController = AutofacInstaller.Container.Resolve<IMovieController>();

            _movies = movieController.GetMovieDataFromLocalLibraryFile().ToObservableCollection();
        }

        public ObservableCollection<Movie> Movies
        {
            get => _movies;
            set { PropertyChanged.ChangeAndNotify(ref _movies, value, () => Movies); }
        }

        public ICommand ScanForLocalMovieFilesCommand => _scanForLocalMovieFilesCommand.Command;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
