namespace MovieManager.Commands
{
    using MovieManager.Contracts;
    using MovieManager.Contracts.Commands;
    using MovieManager.Contracts.Controllers;
    using MovieManager.Models;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;

    public class ScanForLocalMovieFilesCommand : IScanForLocalMovieFilesCommand
    {
        private readonly ICommonDataViewModel _commonData;
        private readonly IMovieController _movieController;
        private ICommand _command;
        
        public ScanForLocalMovieFilesCommand(
            ICommonDataViewModel commonData, 
            IMovieController movieController)
        {
            _commonData = commonData;
            _movieController = movieController;
        }

        public ICommand Command => _command ?? (_command = new RelayCommand<object>(Execute, CanExecute));

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var movies = new List<Movie>();
            var mainDirectory = ConfigurationManager.AppSettings["Directory"];

            if (mainDirectory == "C:\\")
            {
                return;
            }

            var rootFiles = Directory.GetFiles(mainDirectory);
            foreach (var file in rootFiles)
            {
                var fileInfo = new FileInfo(file);
                if (_commonData.MovieFileTypes.Any(x => fileInfo.Extension == x))
                {
                    movies.Add(new Movie
                    {
                        Title = fileInfo.Name.Replace(fileInfo.Extension, ""),
                        FileLocation = fileInfo.FullName
                    });
                }
            }

            foreach (var directory in Directory.GetDirectories(mainDirectory))
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    var fileInfo = new FileInfo(file);
                    if (_commonData.MovieFileTypes.Any(x => fileInfo.Extension == x))
                    {
                        movies.Add(new Movie
                        {
                            Title = fileInfo.Name.Replace(fileInfo.Extension, ""),
                            FileLocation = fileInfo.FullName
                        });
                    }
                }
            }

            //_commonData.LocalMovieFiles = movies;

            if (movies.Count > 0)
            {
                _movieController.StoreMovieData(movies);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
