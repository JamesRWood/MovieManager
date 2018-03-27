namespace MovieManager.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Queries;
    using DM.MovieApi;
    using DM.MovieApi.MovieDb.Movies;
    using Movie = Models.Movie;

    public class QueryForMoviesByTitle : IQueryForMoviesByTitle
    {
        private readonly IQueryForMovieById _queryForMovieById;

        public QueryForMoviesByTitle(IQueryForMovieById queryForMovieById)
        {
            _queryForMovieById = queryForMovieById;
        }

        public async Task<List<Movie>> Execute(string title)
        {
            var api = MovieDbFactory.Create<IApiMovieRequest>().Value;
            var response = await api.SearchByTitleAsync(title);

            var movies = new List<Movie>();
            if (response.TotalResults <= 0)
            {
                return movies;
            }

            movies.AddRange(response.Results.Select(x => _queryForMovieById.Execute(x.Id)));

            return movies;
        }
    }
}
