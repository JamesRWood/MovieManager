namespace MovieManager.Commands.RelayCommands
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;
    using Contracts.Commands.RelayCommands;
    using Models;

    public class PlayMovieCommand : IPlayMovieCommand
    {
        private ICommand _command;

        public ICommand Command => _command ?? (_command = new RelayCommand<object>(Execute, CanExecute));

        public void Execute(object parameter)
        {
            if (parameter is Movie movie)
            {
                Process.Start(movie.FileLocation);
            }
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is Movie movie)
            {
                var fileInfo = new FileInfo(movie.FileLocation);
                return fileInfo.Exists && Core.MovieFileTypes.Any(x => x == fileInfo.Extension);
            }

            return false;
        }

        public event EventHandler CanExecuteChanged;
    }
}
