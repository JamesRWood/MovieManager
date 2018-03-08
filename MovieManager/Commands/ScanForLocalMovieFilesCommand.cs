namespace MovieManager.Commands
{
    using System;
    using System.Windows.Input;
    using MovieManager.Contracts.Commands;
    using MovieManager.Contracts.Controllers;

    public class ScanForLocalMovieFilesCommand : IScanForLocalMovieFilesCommand
    {
        private readonly IFileController _fileController;
        private readonly IMovieController _movieController;
        private ICommand _command;
        
        public ScanForLocalMovieFilesCommand(
            IMovieController movieController, 
            IFileController fileController)
        {
            _movieController = movieController;
            _fileController = fileController;
        }

        public ICommand Command => _command ?? (_command = new RelayCommand<object>(Execute, CanExecute));

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var movies = _fileController.FindLocalMovieFiles();

            if (movies.Count > 0)
            {
                _movieController.StoreMovieData(movies);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
