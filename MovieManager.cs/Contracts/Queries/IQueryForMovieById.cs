namespace MovieManager.cs.Contracts.Queries
{
    using System.Threading.Tasks;
    using Models;

    public interface IQueryForMovieById
    {
        Task<Movie> Execute(int movieId);
    }
}
