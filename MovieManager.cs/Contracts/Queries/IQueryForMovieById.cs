namespace MovieManager.cs.Contracts.Queries
{
    using Models;

    public interface IQueryForMovieById
    {
        Movie Execute(int movieId);
    }
}
