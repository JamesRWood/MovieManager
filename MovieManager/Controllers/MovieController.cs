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
        private readonly List<string> _movieFileTypes = new List<string> { ".mp4", ".avi" };

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
            jsonData.ForEach(x => movies.Add(x));

            return movies;
        }

        //public IList<Movie> GetAllLocalMovies()
        //{
        //    var movies = new List<Movie>();
        //    var mainDirectory = ConfigurationManager.AppSettings["Directory"];

        //    if (mainDirectory == "c:")
        //    {
        //        return movies;
        //    }

        //    var rootFiles = Directory.GetFiles(mainDirectory);
        //    foreach (var file in rootFiles)
        //    {
        //        var fileInfo = new FileInfo(file);
        //        if (_movieFileTypes.Any(x => fileInfo.Extension == x))
        //        {
        //            movies.Add(new Movie
        //            {
        //                Title = fileInfo.Name.Replace(fileInfo.Extension, ""),
        //                FileLocation = fileInfo.FullName
        //            });
        //        }
        //    }

        //    foreach (var directory in Directory.GetDirectories(mainDirectory))
        //    {
        //        foreach (var file in Directory.GetFiles(directory))
        //        {
        //            var fileInfo = new FileInfo(file);
        //            if (_movieFileTypes.Any(x => fileInfo.Extension == x))
        //            {
        //                movies.Add(new Movie
        //                {
        //                    Title = fileInfo.Name.Replace(fileInfo.Extension, ""),
        //                    FileLocation = fileInfo.FullName
        //                });
        //            }
        //        }
        //    }

        //    return movies;
        //}

        public void StoreMovieData(IList<Movie> movies)
        {
            var mainDirectory = ConfigurationManager.AppSettings["Directory"];
            if (mainDirectory == "c:")
            {
                return;
            }

            var movieLibraryFilePath = Path.Combine(mainDirectory, MovieDataFileName);
            using (var file = File.CreateText(movieLibraryFilePath))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, movies);
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

        //private Movie EnrichMovieModel(Movie movie)
        //{
        //    var mainDirectory = ConfigurationManager.AppSettings["Directory"];
        //    if (mainDirectory == "c:")
        //    {
        //        return movie;
        //    }

        //    var serializer = new JsonSerializer();
        //    var movieLibraryFilePath = Path.Combine(mainDirectory, MovieDataFileName);
        //    var jsonData = serializer.Deserialize<List<Movie>>(new JsonTextReader(new StreamReader(movieLibraryFilePath)));

        //    var jsonMovieRecord = jsonData?.FirstOrDefault(x => x.Title == movie.Title);
        //    if (jsonMovieRecord == null)
        //    {
        //        return _queryForMoviesByTitle.Execute(movie.Title).FirstOrDefault();
        //    }

        //    return movie;
        //}
    }
}
