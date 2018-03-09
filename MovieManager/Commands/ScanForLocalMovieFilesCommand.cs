namespace MovieManager.Commands
{
    using System;
    using System.Windows.Input;
    using Contracts;
    using MovieManager.Contracts.Commands;
    using MovieManager.Contracts.Controllers;

    public class ScanForLocalMovieFilesCommand : IScanForLocalMovieFilesCommand
    {
        private readonly ICommonDataViewModel _commonData;
        private readonly IApiController _apiController;
        private readonly IFileController _fileController;
        private readonly IMovieController _movieController;
        private ICommand _command;
        
        public ScanForLocalMovieFilesCommand(
            ICommonDataViewModel commonData,
            IApiController apiController,
            IMovieController movieController, 
            IFileController fileController)
        {
            _commonData = commonData;
            _apiController = apiController;
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
            var updatedMovieList = _apiController.EnrichMoviesMatchedByTitle(movies);

            if (updatedMovieList.Count > 0)
            {
                _movieController.StoreMovieData(updatedMovieList);
            }

            _commonData.CommonDataMovies = updatedMovieList;
        }

        public event EventHandler CanExecuteChanged;
    }
}
