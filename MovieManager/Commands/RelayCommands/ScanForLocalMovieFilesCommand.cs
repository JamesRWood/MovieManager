namespace MovieManager.Commands.RelayCommands
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
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
        private readonly ParallelOptions _parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 8 };
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
            var movies = _fileController.FindLocalMovieFiles();

            var concurrentMovieList = new ConcurrentBag<Movie>();
            Parallel.ForEach(movies, _parallelOptions, m =>
            {
                    var mov = Task.Run(() => _apiController.EnrichMovieMatchedByTitle(m)).Result;
                    concurrentMovieList.Add(mov);
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
