namespace MovieManager.Commands.RelayCommands
{
    using Contracts.Commands.RelayCommands;
    using Contracts.Controllers;
    using Contracts.ViewModels;
    using Helpers;
    using Models;
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class ShowEditMovieSettingsViewCommand : IShowEditMovieSettingsViewCommand
    {
        private readonly IApiController _apiController;
        private readonly IFindMovieDetailsViewModel _findMovieDetailsViewModel;
        private ICommand _command;

        public ShowEditMovieSettingsViewCommand(
            IApiController apiController,
            IFindMovieDetailsViewModel findMovieDetailsViewModel)
        {
            _apiController = apiController;
            _findMovieDetailsViewModel = findMovieDetailsViewModel;
        }

        public ICommand Command => _command ?? (_command = new RelayCommand<object>(Execute, CanExecute));

        public void Execute(object parameter)
        {
            if (!(parameter is Movie selectedMovie))
            {
                return;
            }
            
            Application.Current.MainWindow.ToggleBlurEffect();

            _findMovieDetailsViewModel.SelectedMovie = null;
            _findMovieDetailsViewModel.SelectedMatchedMovie = null;

            _findMovieDetailsViewModel.SelectedMovie = selectedMovie;

            var formattedMovieTitle = Regex.Replace(selectedMovie.Title, Core.DateTagRegex, string.Empty);
            _findMovieDetailsViewModel.PossibleMatches = Task.Run(() => _apiController.GetPossibleMatchesFromApi(formattedMovieTitle)).Result.ToObservableCollection();

            var window = new FindMovieDetails
            {
                DataContext = _findMovieDetailsViewModel,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowStyle = WindowStyle.None,
                Topmost = true
            };
            
            window.Show();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
