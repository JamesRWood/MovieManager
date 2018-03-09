namespace MovieManager.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Controllers;
    using DM.MovieApi;
    using DM.MovieApi.MovieDb.Movies;
    using Movie = Models.Movie;

    public class ApiController : IApiController
    {
        public List<Movie> EnrichMoviesMatchedByTitle(List<Movie> movies)
        {
            var api = MovieDbFactory.Create<IApiMovieRequest>().Value;
            var possibleMatches = new List<MovieInfo>();
            movies.ForEach(x => possibleMatches.AddRange(Task.Run(() => api.SearchByTitleAsync(x.Title)).Result.Results.Where(y => string.Equals(y.Title, x.Title))));

            var updatedMovieList = new List<Movie>();

            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 4 };
            Parallel.ForEach(movies, parallelOptions, m =>
            {
                if (possibleMatches.Count(x => x.Title == m.Title) == 1)
                {
                    var titleMatchedRecord = possibleMatches.FirstOrDefault(x => x.Title == m.Title);
                    var fullMovieRecord = Task.Run(() => api.FindByIdAsync(titleMatchedRecord.Id)).Result.Item;

                    updatedMovieList.Add(new Movie
                    {
                        Title = fullMovieRecord.Title,
                        FileLocation = m.FileLocation,
                        ImagePath = fullMovieRecord.PosterPath,
                        MovieId = fullMovieRecord.Id,
                        Rating = fullMovieRecord.Popularity,
                        ReleaseDate = fullMovieRecord.ReleaseDate,
                        Synopsis = fullMovieRecord.Overview,
                        RuntTime = fullMovieRecord.Runtime,
                        TagLine = fullMovieRecord.Tagline
                    });
                }
                else
                {
                    updatedMovieList.Add(m);
                }
            });

            return updatedMovieList;
        }
    }
}
