namespace MovieManager.Commands
{
    using Contracts.Commands;
    using Contracts.Controllers;
    using Contracts.ViewModels;
    using Models;

    public class OpenWindowCommand : IOpenWindowCommand
    {
        private readonly ICommonDataViewModel _commonData;
        private readonly IFindMovieDetailsViewModel _viewModel;
        private readonly IFileController _fileController;

        public OpenWindowCommand(
            ICommonDataViewModel commonData,
            IFindMovieDetailsViewModel viewModel,
            IFileController fileController)
        {
            _commonData = commonData;
            _viewModel = viewModel;
            _fileController = fileController;
        }

        public Movie OpenWindow()
        {
            var window = new FindMovieDetails
            {
                DataContext = _viewModel
            };

            window.ShowDialog();

            if (_viewModel.SelectedMatchedMovie == null)
            {
                return _viewModel.SelectedMovie;
            }

            var movies = _commonData.CommonDataMovies;
            movies.Remove(_viewModel.SelectedMovie);
            movies.Add(_viewModel.SelectedMatchedMovie);

            _commonData.CommonDataMovies = movies;
            _fileController.StoreMovieData(movies);

            return _viewModel.SelectedMatchedMovie;
        }
    }
}
