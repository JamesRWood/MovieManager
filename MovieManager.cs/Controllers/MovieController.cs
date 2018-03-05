namespace MovieManager.cs.Controllers
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using Contracts.Controllers;
    using Models;

    public class MovieController : IMovieController
    {
        private readonly List<string> _movieFileTypes = new List<string> { ".mp4", ".avi" };

        public ICollection<Movie> GetAllMovies()
        {
            var movies = new List<Movie>();
            var mainDirectory = ConfigurationManager.AppSettings["Directory"];

            if (mainDirectory == "c:")
            {
                return movies;
            }

            foreach (var directory in Directory.GetDirectories(mainDirectory))
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    var fileInfo = new FileInfo(file);
                    if (_movieFileTypes.Any(x => fileInfo.Extension == x))
                    {
                        movies.Add(new Movie
                        {
                            Title = fileInfo.Name,
                            FileLocation = fileInfo.FullName
                        });
                    }
                }
            }

            return movies;
        }
    }
}
