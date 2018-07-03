namespace MovieManager.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Queries;
    using DM.MovieApi.MovieDb.Movies;
    using Movie = Models.Movie;

    public class QueryForMoviesByTitle : IQueryForMoviesByTitle
    {
        private readonly IQueryForMovieById _queryForMovieById;
        private readonly IApiMovieRequest _requestApi;

        public QueryForMoviesByTitle(
            IQueryForMovieById queryForMovieById, 
            IApiMovieRequest requestApi)
        {
            _queryForMovieById = queryForMovieById;
            _requestApi = requestApi;
        }

        public async Task<List<Movie>> Execute(string title)
        {
            var response = await _requestApi.SearchByTitleAsync(title);

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
