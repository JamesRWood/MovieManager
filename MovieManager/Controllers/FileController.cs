namespace MovieManager.Controllers
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Controllers;
    using Contracts.ViewModels;
    using Models;
    using Newtonsoft.Json;

    public class FileController : IFileController
    {
        private readonly ICommonDataViewModel _commonData;
        private const string MovieDataFileName = "MovieLibraryData.json";

        public FileController(ICommonDataViewModel commonData)
        {
            _commonData = commonData;
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

            return movies.OrderBy(x => x.Title).ToList();
        }

        public List<Movie> FindLocalMovieFiles()
        {
            var movies = new ConcurrentBag<Movie>();
            var mainDirectory = ConfigurationManager.AppSettings["Directory"];

            if (mainDirectory == "C:\\")
            {
                return movies.ToList();
            }

            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 4 };

            Parallel.ForEach(Directory.GetFiles(mainDirectory), parallelOptions, file =>
            {
                var fileInfo = new FileInfo(file);
                if (_commonData.MovieFileTypes.Any(x => fileInfo.Extension == x))
                {
                    movies.Add(new Movie
                    {
                        Title = fileInfo.Name.Replace(fileInfo.Extension, ""),
                        FileLocation = fileInfo.FullName
                    });
                }
            });

            Parallel.ForEach(Directory.GetDirectories(mainDirectory), parallelOptions, directory =>
            {
                Parallel.ForEach(Directory.GetFiles(directory), parallelOptions, file =>
                {
                    var fileInfo = new FileInfo(file);
                    if (_commonData.MovieFileTypes.Any(x => fileInfo.Extension == x))
                    {
                        movies.Add(new Movie
                        {
                            Title = fileInfo.Name.Replace(fileInfo.Extension, ""),
                            FileLocation = fileInfo.FullName
                        });
                    }
                });
            });

            return movies.OrderBy(x => x.Title).ToList();
        }

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

        public string RenameFile(string filePath, string newFileName)
        {
            var fileInfo = new FileInfo(filePath);
            var newFilePath = fileInfo.FullName.Replace(fileInfo.Name, newFileName) + fileInfo.Extension;

            File.Move(filePath, newFilePath);
            return newFilePath;
        }
    }
}
