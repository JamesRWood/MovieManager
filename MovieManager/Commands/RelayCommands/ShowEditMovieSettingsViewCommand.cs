namespace MovieManager.Commands.RelayCommands
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media.Effects;
    using Contracts.Commands.RelayCommands;
    using Contracts.Controllers;
    using Contracts.ViewModels;
    using Helpers;
    using Models;

    public class ShowEditMovieSettingsViewCommand : IShowEditMovieSettingsViewCommand
    {
        //private readonly ICommonDataViewModel _commonData;
        private readonly IApiController _apiController;
        private readonly IFindMovieDetailsViewModel _findMovieDetailsViewModel;
        //private readonly IFileController _fileController;
        private ICommand _command;
        private BlurEffect _blur;

        public ShowEditMovieSettingsViewCommand(
            //ICommonDataViewModel commonData,
            IApiController apiController,
            IFindMovieDetailsViewModel findMovieDetailsViewModel)
            //IFileController fileController)
        {
            //_commonData = commonData;
            _apiController = apiController;
            _findMovieDetailsViewModel = findMovieDetailsViewModel;
            //_fileController = fileController;
        }

        public ICommand Command => _command ?? (_command = new RelayCommand<object>(Execute, CanExecute));

        public void Execute(object parameter)
        {
            if (!(parameter is Movie selectedMovie))
            {
                return;
            }

            //ToggleMainWindowBlur();
            _findMovieDetailsViewModel.SelectedMovie = selectedMovie;
            var formattedMovieTitle = Regex.Replace(selectedMovie.Title, Core.DateTagRegex, string.Empty);
            _findMovieDetailsViewModel.PossibleMatches = Task.Run(() => _apiController.GetPossibleMatchesFromApi(formattedMovieTitle)).Result.ToObservableCollection();

            var window = new FindMovieDetails
            {
                DataContext = _findMovieDetailsViewModel,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Topmost = true
            };
            
            window.Show();
            //ToggleMainWindowBlur();

            //if (_findMovieDetailsViewModel.SelectedMatchedMovie == null)
            //{
            //    return;
            //}

            //_findMovieDetailsViewModel.SelectedMatchedMovie.FileLocation = _fileController.RenameFile(_findMovieDetailsViewModel.SelectedMovie.FileLocation, _findMovieDetailsViewModel.SelectedMatchedMovie.Title);
            
            //var movies = _commonData.CommonDataMovies;
            //movies.Remove(_findMovieDetailsViewModel.SelectedMovie);
            //movies.Add(_findMovieDetailsViewModel.SelectedMatchedMovie);

            //_fileController.StoreMovieData(movies);
            
            //_commonData.CommonDataMovies = movies;
            //_commonData.CommonDataSelectedMovie = _findMovieDetailsViewModel.SelectedMatchedMovie;
        }

        private void ToggleMainWindowBlur()
        {
            if (_blur == null)
            {
                _blur = new BlurEffect
                {
                    Radius = 50,
                    RenderingBias = RenderingBias.Quality
                };
            }
            else
            {
                _blur = null;
            }
            
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Effect = _blur;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
