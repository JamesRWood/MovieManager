namespace MovieManager.Commands.RelayCommands
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using Contracts.ViewModels;
    using Models;
    using MovieManager.Contracts.Commands.RelayCommands;

    public class SetSelectedMovieCommand : ISetSelectedMovieCommand
    {
        private readonly ICommonDataViewModel _commonData;
        private ICommand _command;

        public SetSelectedMovieCommand(ICommonDataViewModel commonData)
        {
            _commonData = commonData;
        }

        public ICommand Command => _command ?? (_command = new RelayCommand<object>(Execute, CanExecute));

        public void Execute(object parameter)
        {
            var val = (object[]) parameter;
            if (val[0] is Movie selectedMovie)
            {
                _commonData.CommonDataSelectedMovie = selectedMovie;
            }

            if (val[1] is Window window)
            {
                window.Close();
            }
        }

        public bool CanExecute(object parameter)
        {
            var val = (object[])parameter;
            return val?[0] is Movie;
        }

        public event EventHandler CanExecuteChanged;
    }
}
