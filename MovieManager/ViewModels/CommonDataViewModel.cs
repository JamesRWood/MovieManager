namespace MovieManager.ViewModels
{
    using MovieManager.Contracts;
    using System.Collections.Generic;

    public class CommonDataViewModel : ICommonDataViewModel
    {
        public List<string> MovieFileTypes => new List<string> { ".mp4", ".avi" };
    }
}
