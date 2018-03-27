namespace MovieManager.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Autofac;
    using Contracts.Commands;
    using Contracts.Commands.RelayCommands;
    using Contracts.Controllers;
    using Contracts.Queries;
    using Contracts.ViewModels;
    using MovieManager.Helpers;
    using MovieManager.Models;

    public class DashboardViewModel : IDashboardViewModel
    {
        private readonly ICommonDataViewModel _commonData;
        private readonly IFindMovieDetailsViewModel _findMovieDetailsViewModel;
        private readonly IQueryForMoviesByTitle _queryForMoviesByTitle;
        private readonly IScanForLocalMovieFilesCommand _scanForLocalMovieFilesCommand;
        private readonly IOpenWindowCommand _openFindMovieDetailsWindow;
        private ObservableCollection<Movie> _movies;
        private Movie _selectedMovie;

        public DashboardViewModel()
        {
            _commonData = AutofacInstaller.Container.Resolve<ICommonDataViewModel>();
            _findMovieDetailsViewModel = AutofacInstaller.Container.Resolve<IFindMovieDetailsViewModel>();
            _queryForMoviesByTitle = AutofacInstaller.Container.Resolve<IQueryForMoviesByTitle>();
            _scanForLocalMovieFilesCommand = AutofacInstaller.Container.Resolve<IScanForLocalMovieFilesCommand>();
            _openFindMovieDetailsWindow = AutofacInstaller.Container.Resolve<IOpenWindowCommand>();

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
                    if (SelectedMovie.MovieId == null && SelectedMovie != _findMovieDetailsViewModel.SelectedMovie)
                    {
                        _findMovieDetailsViewModel.SelectedMovie = SelectedMovie;
                        _findMovieDetailsViewModel.PossibleMatches = Task.Run(() => _queryForMoviesByTitle.Execute(SelectedMovie.Title)).Result.ToObservableCollection();
                        SelectedMovie = _openFindMovieDetailsWindow.OpenWindow();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
