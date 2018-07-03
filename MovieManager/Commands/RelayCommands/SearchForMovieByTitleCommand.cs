namespace MovieManager.Commands.RelayCommands
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Contracts.Commands.RelayCommands;
    using Contracts.Controllers;
    using Contracts.ViewModels;
    using Helpers;

    public class SearchForMovieByTitleCommand : ISearchForMovieByTitleCommand
    {
        private readonly ICommonDataViewModel _commonData;
        private readonly IApiController _apiController;
        private ICommand _command;

        public SearchForMovieByTitleCommand(
            ICommonDataViewModel commonData,
            IApiController apiController)
        {
            _commonData = commonData;
            _apiController = apiController;
        }

        public ICommand Command => _command ?? (_command = new RelayCommand<object>(Execute, CanExecute));

        public void Execute(object parameter)
        {
            if (!(parameter is string searchTerm) || searchTerm.Length < 1)
            {
                return;
            }
            
            _commonData.UpdateValue(cd => cd.CommonDataPossibleMatches, Task.Run(() => _apiController.GetPossibleMatchesFromApi(searchTerm)).Result);
        }

        public bool CanExecute(object parameter)
        {
            if (!(parameter is string searchTerm))
            {
                return false;
            }

            return searchTerm.Length > 0;
        }

        public event EventHandler CanExecuteChanged;
    }
}
