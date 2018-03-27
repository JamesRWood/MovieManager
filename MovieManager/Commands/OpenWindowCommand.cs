namespace MovieManager.Commands
{
    using Contracts.Commands;
    using Contracts.Controllers;
    using Contracts.ViewModels;
    using Models;

    public class OpenWindowCommand : IOpenWindowCommand
    {
        public Movie OpenWindow(
            ICommonDataViewModel commonData, 
            IFindMovieDetailsViewModel viewModel,
            IFileController fileController)
        {
            var window = new FindMovieDetails
            {
                DataContext = viewModel
            };

            window.ShowDialog();

            if (viewModel.SelectedMatchedMovie == null)
            {
                return viewModel.SelectedMovie;
            }

            var movies = commonData.CommonDataMovies;
            movies.Remove(viewModel.SelectedMovie);
            movies.Add(viewModel.SelectedMatchedMovie);

            commonData.CommonDataMovies = movies;
            fileController.StoreMovieData(movies);

            return viewModel.SelectedMatchedMovie;
        }
    }
}
