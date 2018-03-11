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

    public class DashboardViewModel : IDashboardViewModel
    {
        private readonly ICommonDataViewModel _commonData;
        private readonly IScanForLocalMovieFilesCommand _scanForLocalMovieFilesCommand;
        private ObservableCollection<Movie> _movies;
        private Movie _selectedMovie;

        public DashboardViewModel()
        {
            _commonData = AutofacInstaller.Container.Resolve<ICommonDataViewModel>();
            _scanForLocalMovieFilesCommand = AutofacInstaller.Container.Resolve<IScanForLocalMovieFilesCommand>();

            var fileController = AutofacInstaller.Container.Resolve<IFileController>();

            _movies = fileController.GetMovieDataFromLocalLibraryFile().ToObservableCollection();

            _commonData.PropertyChanged += CommonDataPropertyChanged;
            PropertyChanged += DashboardViewModelPropertyChanged;
        }

        public ObservableCollection<Movie> Movies
        {
            get => _movies;
            set { PropertyChanged.ChangeAndNotify(ref _movies, value, () => Movies); }
        }

        public Movie SelectedMovie
        {
            get => _selectedMovie;
            set { PropertyChanged.ChangeAndNotify(ref _selectedMovie, value, () => SelectedMovie); }
        }

        public ICommand ScanForLocalMovieFilesCommand => _scanForLocalMovieFilesCommand.Command;

        public event PropertyChangedEventHandler PropertyChanged;

        private void CommonDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CommonDataMovies":
                    Movies = _commonData.CommonDataMovies.ToObservableCollection();
                    break;
                default:
                    break;
            }
        }

        private void DashboardViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SelectedMovie":
                    break;
                default:
                    break;
            }
        }
    }
}
