namespace MovieManager.Commands.RelayCommands
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Contracts.Commands.RelayCommands;
    using Contracts.Controllers;
    using Contracts.ViewModels;
    using Helpers;
    using Models;

    public class EditMovieSettingsCommand : IEditMovieSettingsCommand
    {
        private readonly ICommonDataViewModel _commonData;
        private readonly IApiController _apiController;
        private readonly IFindMovieDetailsViewModel _findMovieDetailsViewModel;
        private readonly IFileController _fileController;
        private ICommand _command;

        public EditMovieSettingsCommand(
            ICommonDataViewModel commonData,
            IApiController apiController,
            IFindMovieDetailsViewModel findMovieDetailsViewModel,
            IFileController fileController)
        {
            _commonData = commonData;
            _apiController = apiController;
            _findMovieDetailsViewModel = findMovieDetailsViewModel;
            _fileController = fileController;
        }

        public ICommand Command => _command ?? (_command = new RelayCommand<object>(Execute, CanExecute));

        public void Execute(object parameter)
        {
            if (!(parameter is Movie selectedMovie))
            {
                return;
            }

            _findMovieDetailsViewModel.SelectedMovie = selectedMovie;
            var formattedMovieTitle = Regex.Replace(selectedMovie.Title, Core.DateTagRegex, string.Empty);
            _findMovieDetailsViewModel.PossibleMatches = Task.Run(() => _apiController.GetPossibleMatchesFromApi(formattedMovieTitle)).Result.ToObservableCollection();

            var window = new FindMovieDetails
            {
                DataContext = _findMovieDetailsViewModel
            };

            window.ShowDialog();

            if (_findMovieDetailsViewModel.SelectedMatchedMovie == null)
            {
                return;
            }

            _findMovieDetailsViewModel.SelectedMatchedMovie.FileLocation = _fileController.RenameFile(_findMovieDetailsViewModel.SelectedMovie.FileLocation, _findMovieDetailsViewModel.SelectedMatchedMovie.Title);
                
            var movies = _commonData.CommonDataMovies;
            movies.Remove(_findMovieDetailsViewModel.SelectedMovie);
            movies.Add(_findMovieDetailsViewModel.SelectedMatchedMovie);

            _fileController.StoreMovieData(movies);

            _commonData.CommonDataMovies = movies;
            _commonData.CommonDataSelectedMovie = _findMovieDetailsViewModel.SelectedMatchedMovie;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
