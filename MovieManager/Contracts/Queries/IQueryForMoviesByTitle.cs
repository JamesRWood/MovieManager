namespace MovieManager.Contracts.Queries
{
    using System.Collections.Generic;
    using Models;

    public interface IQueryForMoviesByTitle
    {
        List<Movie> Execute(string title);
    }
}
