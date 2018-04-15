namespace MovieManager.Commands.RelayCommands
{
    using MovieManager.Contracts.Commands.RelayCommands;
    using MovieManager.Contracts.Controllers;
    using MovieManager.Contracts.ViewModels;
    using MovieManager.Helpers;
    using MovieManager.Models;
    using System;
    using System.Windows.Input;

    public class SelectMatchedMovieCommand : ISelectMatchedMovieCommand
    {
        private readonly ICommonDataViewModel _commonData;
        private readonly IFileController _fileController;
        private ICommand _command;

        public SelectMatchedMovieCommand(
            ICommonDataViewModel commonData, 
            IFileController fileController)
        {
            _commonData = commonData;
            _fileController = fileController;
        }

        public ICommand Command => _command ?? (_command = new RelayCommand<object>(Execute, CanExecute));

        public void Execute(object parameter)
        {
            var moviesArray = (object[])parameter;
            if (!(moviesArray[0] is Movie selectedPossibleMatch))
            {
                return;
            }

            if (!(moviesArray[1] is Movie currentSelectedMovie))
            {
                return;
            }

            selectedPossibleMatch.FileLocation = currentSelectedMovie.FileLocation;
            _commonData.CommonDataMovies.Remove(currentSelectedMovie);

            selectedPossibleMatch.BackdropColor = selectedPossibleMatch.GetImageMajorityColor();

            _commonData.CommonDataSelectedMovie = null;
            _commonData.CommonDataSelectedMovie = selectedPossibleMatch;

            var movies = _commonData.CommonDataMovies;
            _commonData.CommonDataMovies = null;
            movies.Add(selectedPossibleMatch);
            _commonData.CommonDataMovies = movies;

            _fileController.StoreMovieData(movies);
        }

        public bool CanExecute(object parameter)
        {
            var moviesArray = (object[])parameter;
            return moviesArray?[0] is Movie;
        }

        public event EventHandler CanExecuteChanged;

    }
}
