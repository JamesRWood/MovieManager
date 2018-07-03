namespace MovieManager.Contracts.Queries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IQueryForMoviesByTitle : IQueryBase
    {
        Task<List<Movie>> Execute(string title);
    }
}
