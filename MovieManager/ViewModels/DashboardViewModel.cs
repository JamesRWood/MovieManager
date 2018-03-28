namespace MovieManager.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;
    using Autofac;
    using Contracts.Commands.RelayCommands;
    using Contracts.Controllers;
    using Contracts.ViewModels;
    using MovieManager.Helpers;
    using MovieManager.Models;

    public class DashboardViewModel : IDashboardViewModel
    {
        private readonly ICommonDataViewModel _commonData;
        private readonly IScanForLocalMovieFilesCommand _scanForLocalMovieFilesCommand;
        private readonly IEditMovieSettingsCommand _editMovieSettingsCommand;
        private readonly IPlayMovieCommand _playMovieCommand;
        private ObservableCollection<Movie> _movies;
        private Movie _selectedMovie;

        public DashboardViewModel()
        {
            _commonData = AutofacInstaller.Container.Resolve<ICommonDataViewModel>();
            _scanForLocalMovieFilesCommand = AutofacInstaller.Container.Resolve<IScanForLocalMovieFilesCommand>();
            _editMovieSettingsCommand = AutofacInstaller.Container.Resolve<IEditMovieSettingsCommand>();
            _playMovieCommand = AutofacInstaller.Container.Resolve<IPlayMovieCommand>();

            var fileController = AutofacInstaller.Container.Resolve<IFileController>();

            _movies = fileController.GetMovieDataFromLocalLibraryFile().ToObservableCollection();

            _commonData.PropertyChanged += CommonDataPropertyChanged;
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

        public ICommand EditMovieSettingsCommand => _editMovieSettingsCommand.Command;

        public ICommand PlayMovieCommand => _playMovieCommand.Command;

        public event PropertyChangedEventHandler PropertyChanged;

        private void CommonDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CommonDataMovies":
                    Movies = _commonData.CommonDataMovies.OrderBy(x => x.Title).ToObservableCollection();
                    break;
                case "CommonDataSelectedMovie":
                    SelectedMovie = _commonData.CommonDataSelectedMovie;
                    break;
                default:
                    break;
            }
        }
    }
}
