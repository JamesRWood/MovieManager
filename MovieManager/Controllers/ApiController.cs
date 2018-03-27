﻿namespace MovieManager.Controllers
{
    using Contracts.Controllers;
    using MovieManager.Contracts.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
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

        public async Task<Movie> EnrichMovieMatchedByTitle(Movie movie)
        {
            const string dateTagRegex = @"\s\[[0-9]{4}\]|\[[0-9]{4}\]|\s\([0-9]{4}\)|\([0-9]{4}\)|\s[0-9]{4}";
            var possibleMatches = await GetPossibleMatchesFromApi(movie.Title);

            if (possibleMatches.Count < 1)
            {
                return movie;
            }

            var formattedTitle = Regex.Replace(movie.Title, dateTagRegex, string.Empty).Trim();
            if (possibleMatches.Count(x => string.Equals(x.Title, formattedTitle, StringComparison.InvariantCultureIgnoreCase)) == 1)
            {
                var match = possibleMatches.FirstOrDefault(x => string.Equals(x.Title, formattedTitle, StringComparison.InvariantCultureIgnoreCase));
                if (match != null)
                {
                    match.FileLocation = movie.FileLocation;

                    if (!string.Equals(match.Title, movie.Title))
                    {
                        var newFilePath = _fileController.RenameFile(movie.FileLocation, formattedTitle);
                        match.FileLocation = newFilePath;
                    }

                    return match;
                }
            }

            return movie;
        }

        private async Task<List<Movie>> GetPossibleMatchesFromApi(string title)
        {
            var possibleMatches = new List<Movie>();
            var matches = await _queryForMoviesByTitle.Execute(title);

            foreach (var match in matches)
            {
                if (match != null)
                {
                    possibleMatches.Add(match);
                }
            }

            return possibleMatches;
        }
    }
}
