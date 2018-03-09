namespace MovieManager.Contracts
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using MovieManager.Models;

    public interface IDashboardViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Movie> Movies { get; set; }
    }
}
