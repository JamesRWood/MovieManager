namespace MovieManager.Controllers
{
    using Contracts.Controllers;
    using MovieManager.Contracts.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Movie = Models.Movie;

    public class ApiController : IApiController
    {
        private readonly IQueryForMoviesByTitle _queryForMoviesByTitle;
        private readonly IFileController _fileController;
        
        public ApiController(
            IQueryForMoviesByTitle queryForMoviesByTitle, 
            IFileController fileController)
        {
            _queryForMoviesByTitle = queryForMoviesByTitle;
            _fileController = fileController;
        }

        public Movie EnrichMovieMatchedByTitle(Movie movie)
        {
            const string dateTagRegex = @"\s\[[0-9]{4}\]|\[[0-9]{4}\]|\s\([0-9]{4}\)|\([0-9]{4}\)|\s[0-9]{4}";
            var possibleMatches = GetPossibleMatchesFromApi(movie.Title);

            if (possibleMatches.Count < 1)
            {
                return movie;
            }

            var formattedTitle = Regex.Replace(movie.Title, dateTagRegex, string.Empty).Trim();
            if (possibleMatches.Count(x => string.Equals(x.Title, formattedTitle, StringComparison.InvariantCultureIgnoreCase)) == 1)
            {
                var match = possibleMatches.FirstOrDefault(x => string.Equals(x.Title, formattedTitle, StringComparison.InvariantCultureIgnoreCase));
                match.FileLocation = movie.FileLocation;

                if (!string.Equals(match.Title, movie.Title))
                {
                    var newFilePath =_fileController.RenameFile(match.FileLocation, formattedTitle);
                    match.FileLocation = newFilePath;
                }

                return match;
            }

            return movie;
        }

        private List<Movie> GetPossibleMatchesFromApi(string title)
        {
            var possibleMatches = new List<Movie>();
            var matches = _queryForMoviesByTitle.Execute(title);

            foreach (var match in matches)
            {
                if (match != null)
                {
                    possibleMatches.Add(match);
                }
            }

            return possibleMatches;
        }













        //    public List<Movie> EnrichMoviesMatchedByTitle(List<Movie> movies)
        //    {
        //        var possibleMatches = GetPossibleMatchesFromApi(movies);

        //        return EnrichMathedMovies(movies, possibleMatches);
        //    }

        //    private List<Movie> GetPossibleMatchesFromApi(List<Movie> movies)
        //    {
        //        var possibleMatches = new ConcurrentBag<Movie>();
        //        Parallel.ForEach(movies, _parallelOptions, m =>
        //        {
        //            var matches = _queryForMoviesByTitle.Execute(m.Title);
        //            if (matches.Count <= 0)
        //            {
        //                return;
        //            }

        //            foreach (var match in matches)
        //            {
        //                if (match != null)
        //                {
        //                    possibleMatches.Add(match);
        //                }
        //            }
        //        });

        //        return possibleMatches.ToList();
        //    }

        //    private List<Movie> EnrichMathedMovies(List<Movie> movies, List<Movie> possibleMatches)
        //    {
        //        var concurrentPossibleMatches = new ConcurrentBag<Movie>(possibleMatches);
        //        var updatedMovieList = new ConcurrentBag<Movie>();
        //        Parallel.ForEach(movies, _parallelOptions, m =>
        //        {
        //            if (concurrentPossibleMatches.Any() && concurrentPossibleMatches.Count(x => x.Title == m.Title) == 1)
        //            {
        //                var titleMatchedRecord = concurrentPossibleMatches.FirstOrDefault(x => x.Title == m.Title);

        //                if (titleMatchedRecord != null)
        //                {
        //                    titleMatchedRecord.FileLocation = m.FileLocation;
        //                    updatedMovieList.Add(titleMatchedRecord);
        //                }
        //                else
        //                {
        //                    updatedMovieList.Add(m);
        //                }
        //            }
        //            else
        //            {
        //                updatedMovieList.Add(m);
        //            }
        //        });

        //        return updatedMovieList.ToList();
        //    }
    }
}
