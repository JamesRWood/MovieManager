namespace MovieManager.Contracts
{
    using System.Collections.ObjectModel;
    using Models;

    public interface IDashboardViewModel
    {
        ObservableCollection<Movie> Movies { get; }
    }
}
