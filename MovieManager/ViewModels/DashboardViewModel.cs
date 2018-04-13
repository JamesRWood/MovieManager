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
    using Color = System.Windows.Media.Color;

    public class DashboardViewModel : IDashboardViewModel
    {
        private readonly ICommonDataViewModel _commonData;
        private readonly IScanForLocalMovieFilesCommand _scanForLocalMovieFilesCommand;
        private readonly IPlayMovieCommand _playMovieCommand;
        private readonly ISlideGridCommand _slideGridCommand;
        private readonly ISearchForMovieWithTextCommand _searchForMovieWithTextCommand;
        private ObservableCollection<Movie> _movies;
        private Movie _selectedMovie;
        private ObservableCollection<Movie> _possibleMatches;
        private Movie _selectedPossibleMatch;
        private string _searchTerm;

        public DashboardViewModel()
        {
            _commonData = AutofacInstaller.Container.Resolve<ICommonDataViewModel>();
            _scanForLocalMovieFilesCommand = AutofacInstaller.Container.Resolve<IScanForLocalMovieFilesCommand>();
            _playMovieCommand = AutofacInstaller.Container.Resolve<IPlayMovieCommand>();
            _slideGridCommand = AutofacInstaller.Container.Resolve<ISlideGridCommand>();
            _searchForMovieWithTextCommand = AutofacInstaller.Container.Resolve<ISearchForMovieWithTextCommand>();

            _commonData.PropertyChanged += CommonDataPropertyChanged;

            var fileController = AutofacInstaller.Container.Resolve<IFileController>();
            _commonData.CommonDataMovies = fileController.GetMovieDataFromLocalLibraryFile();
        }

        #region MainDashboardGrid Properties

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

        #endregion

        #region MovieSettingsGrid Properties

        public ObservableCollection<Movie> PossibleMatches
        {
            get => _possibleMatches;
            set { PropertyChanged.ChangeAndNotify(ref _possibleMatches, value, () => PossibleMatches); }
        }

        public Movie SelectedPossibleMatch
        {
            get => _selectedPossibleMatch;
            set { PropertyChanged.ChangeAndNotify(ref _selectedPossibleMatch, value, () => SelectedPossibleMatch); }
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set { PropertyChanged.ChangeAndNotify(ref _searchTerm, value, () => SearchTerm); }
        }

        #endregion

        public Color BaseColor => Core.TransparentColor;

        public ICommand ScanForLocalMovieFilesCommand => _scanForLocalMovieFilesCommand.Command;

        public ICommand PlayMovieCommand => _playMovieCommand.Command;

        public ICommand SlideGridCommand => _slideGridCommand.Command;

        public ICommand SearchForMovieWithTextCommand => _searchForMovieWithTextCommand.Command;

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
                case "CommonDataPossibleMatches":
                    PossibleMatches = _commonData.CommonDataPossibleMatches.ToObservableCollection();
                    break;
                default:
                    break;
            }
        }
    }
}