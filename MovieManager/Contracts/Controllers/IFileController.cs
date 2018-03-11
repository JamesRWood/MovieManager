namespace MovieManager.Contracts.Controllers
{
    using System.Collections.Generic;
    using Models;

    public interface IFileController
    {
        IList<Movie> GetMovieDataFromLocalLibraryFile();

        List<Movie> FindLocalMovieFiles();

        void StoreMovieData(IList<Movie> movies);

        string RenameFile(string filePath, string newFileName);
    }
}
