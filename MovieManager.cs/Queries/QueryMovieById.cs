namespace MovieManager.cs.Queries
{
    using System.Threading.Tasks;
    using Contracts.Queries;
    using DM.MovieApi;
    using DM.MovieApi.MovieDb.Movies;
    using Movie = Models.Movie;

    public class QueryMovieById : IQueryForMovieById
    {
        public async Task<Movie> Execute(int movieId)
        {
            var api = MovieDbFactory.Create<IApiMovieRequest>().Value;
            var response = await api.FindByIdAsync(movieId);

            Movie movie = null;

            if (response.Item != null)
            {
                movie = new Movie
                {
                    Title = response.Item.Title,
                    ReleaseDate = response.Item.ReleaseDate,
                    MovieId = response.Item.Id,
                    Rating = response.Item.Popularity
                };
            }

            return movie;
        }
    }
}
