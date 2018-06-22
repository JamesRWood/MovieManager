namespace MovieManager.Controllers
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Controllers;
    using Models;
    using Newtonsoft.Json;

    public class FileController : IFileController
    {
        private const string DefaultDirectory = "C:\\";
        private const string MovieDataFileName = "MovieLibraryData.json";
        private string _mainDirectory => ConfigurationManager.AppSettings["Directory"];

        public List<Movie> GetMovieDataFromLocalLibraryFile()
        {
            if (string.Equals(_mainDirectory,  DefaultDirectory))
            {
                return new List<Movie>();
            }

            var movieLibraryFilePath = Path.Combine(_mainDirectory, MovieDataFileName);
            if (!File.Exists(movieLibraryFilePath))
            {
                return new List<Movie>();
            }

            var serializer = new JsonSerializer();
            return serializer.Deserialize<List<Movie>>(new JsonTextReader(new StreamReader(movieLibraryFilePath))).OrderBy(x => x.Title).ToList();
        }

        public List<Movie> FindLocalMovieFiles()
        {
            if (string.Equals(_mainDirectory, DefaultDirectory))
            {
                return new List<Movie>();
            }

            var movies = new ConcurrentBag<Movie>();
            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 4 };
            Parallel.ForEach(Directory.GetFiles(_mainDirectory), parallelOptions, file =>
            {
                var fileInfo = new FileInfo(file);
                if (Core.MovieFileTypes.Any(x => fileInfo.Extension == x))
                {
                    movies.Add(new Movie
                    {
                        Title = fileInfo.Name.Replace(fileInfo.Extension, string.Empty),
                        FileLocation = fileInfo.FullName
                    });
                }
            });

            Parallel.ForEach(Directory.GetDirectories(_mainDirectory), parallelOptions, directory =>
            {
                Parallel.ForEach(Directory.GetFiles(directory), parallelOptions, file =>
                {
                    var fileInfo = new FileInfo(file);
                    if (Core.MovieFileTypes.Any(x => fileInfo.Extension == x))
                    {
                        movies.Add(new Movie
                        {
                            Title = fileInfo.Name.Replace(fileInfo.Extension,string.Empty),
                            FileLocation = fileInfo.FullName
                        });
                    }
                });
            });

            return movies.OrderBy(x => x.Title).ToList();
        }

        public void StoreMovieData(IList<Movie> movies)
        {
            if (string.Equals(_mainDirectory, DefaultDirectory))
            {
                return;
            }

            var movieLibraryFilePath = Path.Combine(_mainDirectory, MovieDataFileName);
            using (var file = File.CreateText(movieLibraryFilePath))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, movies);
            }
        }

        public string RenameFile(string filePath, string newFileName)
        {
            var fileInfo = new FileInfo(filePath);
            var newFilePath = fileInfo.FullName.Replace(fileInfo.Name, newFileName.Replace(":", string.Empty)) + fileInfo.Extension;

            File.Move(filePath, newFilePath);
            return newFilePath;
        }
    }
}
