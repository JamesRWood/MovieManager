namespace MovieManager.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Queries;
    using DM.MovieApi;
    using DM.MovieApi.MovieDb.Movies;
    using Movie = Models.Movie;

    public class QueryForMoviesesByTitle : IQueryForMoviesByTitle
    {
        private readonly IQueryForMovieById _queryForMovieById;

        public QueryForMoviesesByTitle(IQueryForMovieById queryForMovieById)
        {
            _queryForMovieById = queryForMovieById;
        }

        public List<Movie> Execute(string title)
        {
            var api = MovieDbFactory.Create<IApiMovieRequest>().Value;
            var response = Task.Run(() => api.SearchByTitleAsync(title));

            var movies = new List<Movie>();
            if (response.Result.TotalResults <= 0)
            {
                return movies;
            }

            movies.AddRange(response.Result.Results.Select(x => _queryForMovieById.Execute(x.Id)));

            return movies;
        }
    }
}
