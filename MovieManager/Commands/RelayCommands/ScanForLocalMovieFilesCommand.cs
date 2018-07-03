namespace MovieManager.Commands.RelayCommands
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Contracts.Commands.RelayCommands;
    using Contracts.Controllers;
    using Contracts.ViewModels;
    using Helpers;
    using Models;

    public class ScanForLocalMovieFilesCommand : IScanForLocalMovieFilesCommand
    {
        private readonly ICommonDataViewModel _commonData;
        private readonly IApiController _apiController;
        private readonly IFileController _fileController;
        //// There's an issue when increasing the parallelism. Concurrent calls to the API don't seem to work very well
        private readonly ParallelOptions _parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 1 };
        private ICommand _command;
        
        public ScanForLocalMovieFilesCommand(
            ICommonDataViewModel commonData,
            IApiController apiController,
            IFileController fileController)
        {
            _commonData = commonData;
            _apiController = apiController;
            _fileController = fileController;
        }

        public ICommand Command => _command ?? (_command = new RelayCommand<object>(Execute, CanExecute));

        public void Execute(object parameter)
        {
            var exisitingMovieLibrary = _fileController.GetMovieDataFromLocalLibraryFile();
            var movies = new ConcurrentBag<Movie>(_fileController.FindLocalMovieFiles());

            var concurrentMovieList = new ConcurrentBag<Movie>();
            Parallel.ForEach(movies, _parallelOptions, m =>
            {
                var existingMatch = exisitingMovieLibrary.FirstOrDefault(x => x.FileLocation == m.FileLocation && !string.IsNullOrEmpty(x.TagLine) && !string.IsNullOrEmpty(x.Title));
                if (existingMatch != null)
                {
                    concurrentMovieList.Add(existingMatch);
                }
                else
                {
                    var mov = Task.Run(() => _apiController.EnrichMovieMatchedByTitle(m)).Result;
                    concurrentMovieList.Add(mov);
                    
                    //// Yeh......ought to look into issues around API latency to get around this
                    Thread.Sleep(500);
                }
            });
            
            _commonData.UpdateValue(cd => cd.CommonDataMovies, concurrentMovieList.OrderBy(x => x.Title).ToList());
            _fileController.StoreMovieData(concurrentMovieList.ToList());
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
