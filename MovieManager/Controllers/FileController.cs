namespace MovieManager.Controllers
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Controllers;
    using Models;

    public class FileController : IFileController
    {
        private readonly ICommonDataViewModel _commonData;

        public FileController(ICommonDataViewModel commonData)
        {
            _commonData = commonData;
        }

        public IList<Movie> FindLocalMovieFiles()
        {
            var movies = new List<Movie>();
            var mainDirectory = ConfigurationManager.AppSettings["Directory"];

            if (mainDirectory == "C:\\")
            {
                return movies;
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

            return movies;
        }
    }
}
