namespace MovieManager.Queries
{
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Queries;
    using DM.MovieApi;
    using DM.MovieApi.MovieDb.Movies;
    using Movie = Models.Movie;

    public class QueryForMovieById : IQueryForMovieById
    {
        public Movie Execute(int movieId)
        {
            var api = MovieDbFactory.Create<IApiMovieRequest>().Value;
            var response = Task.Run(() => api.FindByIdAsync(movieId));

            Movie movie = null;
            if (response.Result.Item != null)
            {
                movie = new Movie
                {
                    Title = response.Result.Item.Title,
                    ReleaseDate = response.Result.Item.ReleaseDate,
                    MovieId = response.Result.Item.Id,
                    Rating = response.Result.Item.VoteAverage,
                    Synopsis = response.Result.Item.Overview,
                    ImagePath = response.Result.Item.PosterPath,
                    TagLine = response.Result.Item.Tagline,
                    RunTime = response.Result.Item.Runtime,
                    Genres = response.Result.Item.Genres.OrderBy(x => x.Name).ToList()
                };
            }

            return movie;
        }
    }
}
