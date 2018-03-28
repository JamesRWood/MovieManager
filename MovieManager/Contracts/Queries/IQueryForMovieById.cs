namespace MovieManager.Contracts.Queries
{
    using Models;

    public interface IQueryForMovieById : IQueryBase
    {
        Movie Execute(int movieId);
    }
}
