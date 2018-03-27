namespace MovieManager.Contracts.Commands
{
    using Controllers;
    using Models;
    using ViewModels;

    public interface IOpenWindowCommand
    {
        Movie OpenWindow(ICommonDataViewModel commonData, IFindMovieDetailsViewModel viewModel, IFileController fileController);
    }
}
