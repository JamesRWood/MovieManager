namespace MovieManager.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Contracts.Controllers;
    using Helpers;
    using Models;
    using MovieManager.Contracts.Queries;

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

        public async Task<Movie> EnrichMovieMatchedByTitle(Movie movie)
        {
            var possibleMatches = await GetPossibleMatchesFromApi(movie.Title);

            if (possibleMatches.Count < 1)
            {
                return movie;
            }
            
            var formattedTitle = FormatMovieTitle(movie.Title);
            if (possibleMatches.Count(x => string.Equals(FormatMovieTitle(x.Title), formattedTitle, StringComparison.InvariantCultureIgnoreCase)) != 1)
            {
                return movie;
            }

            var match = possibleMatches.FirstOrDefault(x => string.Equals(FormatMovieTitle(x.Title), formattedTitle, StringComparison.InvariantCultureIgnoreCase));
            if (match == null)
            {
                return movie;
            }
            
            match.FileLocation = movie.FileLocation;
            match.BackdropColor = match.GetImageMajorityColor();

            if (string.Equals(match.Title, movie.Title))
            {
                return match;
            }

            var newFilePath = _fileController.RenameFile(movie.FileLocation, match.Title);
            match.FileLocation = newFilePath;

            return match;
        }

        public async Task<List<Movie>> GetPossibleMatchesFromApi(string title)
        {
            var matches = await _queryForMoviesByTitle.Execute(title);

            return matches.Where(match => match != null).ToList();
        }

        private string FormatMovieTitle(string input)
        {
            return Regex.Replace(Regex.Replace(input, Core.DateTagRegex, string.Empty).Trim(), Core.PunctuationRegex, string.Empty);
        }
    }
}
