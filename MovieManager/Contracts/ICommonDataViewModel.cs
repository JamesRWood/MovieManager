namespace MovieManager.Contracts
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Models;

    public interface ICommonDataViewModel : INotifyPropertyChanged
    {
        List<string> MovieFileTypes { get; }

        List<Movie> CommonDataMovies { get; set; }
    }
}
