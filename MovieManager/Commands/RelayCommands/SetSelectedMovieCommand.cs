namespace MovieManager.Commands.RelayCommands
{
    using Contracts.ViewModels;
    using Models;
    using MovieManager.Contracts.Commands.RelayCommands;
    using MovieManager.Contracts.Controllers;
    using MovieManager.Helpers;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class SetSelectedMovieCommand : ISetSelectedMovieCommand
    {
        private readonly ICommonDataViewModel _commonData;
        private readonly IFileController _fileController;
        private ICommand _command;

        public SetSelectedMovieCommand(
            ICommonDataViewModel commonData, 
            IFileController fileController)
        {
            _commonData = commonData;
            _fileController = fileController;
        }

        public ICommand Command => _command ?? (_command = new RelayCommand<object>(Execute, CanExecute));

        public void Execute(object parameter)
        {
            var val = (object[]) parameter;
            if (val.Length == 3 &&
                val[1] is Movie updatedSelectedMovie &&
                val[2] is Movie selectedMovie)
            {
                UpdateMovies(updatedSelectedMovie, selectedMovie);
            }

            if (!(val[0] is Window window))
            {
                return;
            }

            window.Close();
            Application.Current.MainWindow.ToggleBlurEffect();
        }

        public bool CanExecute(object parameter)
        {
            var val = (object[])parameter;
            return val?[1] is Movie;
        }

        private void UpdateMovies(Movie updatedSelectedMovie, Movie selectedMovie)
        {
            if (updatedSelectedMovie == null)
            {
                return;
            }

            updatedSelectedMovie.FileLocation = _fileController.RenameFile(selectedMovie.FileLocation, updatedSelectedMovie.Title);

            var movies = _commonData.CommonDataMovies;
            movies.Remove(selectedMovie);
            movies.Add(updatedSelectedMovie);

            _fileController.StoreMovieData(movies);

            _commonData.CommonDataMovies = null;
            _commonData.CommonDataMovies = movies;

            _commonData.CommonDataSelectedMovie = null;
            _commonData.CommonDataSelectedMovie = updatedSelectedMovie;
        }

        public event EventHandler CanExecuteChanged;
    }
}
