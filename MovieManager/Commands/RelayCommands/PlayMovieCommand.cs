namespace MovieManager.Commands.RelayCommands
{
    using System;
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
            if (!(parameter is Movie movie))
            {
                return;
            }
            
            var window = new MediaPlayer(movie.FileLocation);
            window.ShowDialog();
        }

        public bool CanExecute(object parameter)
        {
            if (!(parameter is Movie movie))
            {
                return false;
            }

            var fileInfo = new FileInfo(movie.FileLocation);
            return fileInfo.Exists && Core.MovieFileTypes.Any(x => string.Equals(fileInfo.Extension, x, StringComparison.InvariantCultureIgnoreCase));
        }

        public event EventHandler CanExecuteChanged;
    }
}
