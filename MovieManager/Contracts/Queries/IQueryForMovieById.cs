namespace MovieManager.Contracts.Queries
{
    using Models;

    public interface IQueryForMovieById
    {
        Movie Execute(int movieId);
    }
}
