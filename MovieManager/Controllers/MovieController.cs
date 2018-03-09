namespace MovieManager.Controllers
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using Contracts.Controllers;
    using Contracts.Queries;
    using Models;
    using Newtonsoft.Json;

    public class MovieController : IMovieController
    {
        private readonly IQueryForMoviesByTitle _queryForMoviesByTitle;
        private readonly IQueryForMovieById _queryForMovieById;

        private const string MovieDataFileName = "MovieLibraryData.json";

        public MovieController(
            IQueryForMoviesByTitle queryForMoviesByTitle, 
            IQueryForMovieById queryForMovieById)
        {
            _queryForMoviesByTitle = queryForMoviesByTitle;
            _queryForMovieById = queryForMovieById;
        }

        public IList<Movie> GetMovieDataFromLocalLibraryFile()
        {
            var movies = new List<Movie>();

            var mainDirectory = ConfigurationManager.AppSettings["Directory"];
            if (mainDirectory == "c:")
            {
                return movies;
            }

            var serializer = new JsonSerializer();
            var movieLibraryFilePath = Path.Combine(mainDirectory, MovieDataFileName);

            if (!File.Exists(movieLibraryFilePath))
            {
                return movies;
            }

            var jsonData = serializer.Deserialize<List<Movie>>(new JsonTextReader(new StreamReader(movieLibraryFilePath)));
            jsonData?.ForEach(x => movies.Add(x));

            return movies;
        }

        public void StoreMovieData(IList<Movie> movies)
        {
            var mainDirectory = ConfigurationManager.AppSettings["Directory"];
            if (mainDirectory == "c:")
            {
                return;
            }

            var movieLibraryFilePath = Path.Combine(mainDirectory, MovieDataFileName);
            if (!File.Exists(movieLibraryFilePath))
            {
                using (var file = File.CreateText(movieLibraryFilePath))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(file, movies);
                }
            }
            else
            {
                var storedMovieData = GetMovieDataFromLocalLibraryFile();
                foreach (var mov in movies)
                {
                    var matchedRecord = storedMovieData.FirstOrDefault(x => x.Title == mov.Title);
                    if (matchedRecord?.MovieId == null)
                    {
                        
                    }
                }
            }
        }

        public IList<Movie> SearchForMoviesByTitle(string title)
        {
            return _queryForMoviesByTitle.Execute(title);
        }

        public Movie GetMovieById(int id)
        {
            return _queryForMovieById.Execute(id);
        }
    }
}
