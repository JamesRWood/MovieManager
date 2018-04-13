namespace MovieManager.Commands.RelayCommands
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Contracts.Commands.RelayCommands;
    using Contracts.Controllers;
    using Contracts.ViewModels;

    public class SearchForMovieWithTextCommand : ISearchForMovieWithTextCommand
    {
        private readonly ICommonDataViewModel _commonData;
        private readonly IApiController _apiController;
        private ICommand _command;

        public SearchForMovieWithTextCommand(
            ICommonDataViewModel commonData,
            IApiController apiController)
        {
            _commonData = commonData;
            _apiController = apiController;
        }

        public ICommand Command => _command ?? (_command = new RelayCommand<object>(Execute, CanExecute));

        private void Execute(object parameter)
        {
            if (!(parameter is string searchTerm))
            {
                return;
            }

            _commonData.CommonDataPossibleMatches = Task.Run(() => _apiController.GetPossibleMatchesFromApi(searchTerm)).Result;
        }

        private bool CanExecute(object parameter)
        {
            return parameter is string;
        }
    }
}
