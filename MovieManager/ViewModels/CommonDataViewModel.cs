namespace MovieManager.ViewModels
{
    using System.Collections.Generic;
    using MovieManager.Contracts;
    using MovieManager.Models;

    public class CommonDataViewModel : ICommonDataViewModel
    {
        public List<string> MovieFileTypes => new List<string> { ".mp4", ".avi" };

        public List<Movie> LocalMovieFiles { get; set; }
    }
}
