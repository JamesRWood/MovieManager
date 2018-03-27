namespace MovieManager.Contracts.ViewModels
{
    using System.Collections.ObjectModel;
    using Models;

    public interface IDashboardViewModel : IViewModel
    {
        ObservableCollection<Movie> Movies { get; set; }

        Movie SelectedMovie { get; set; }
    }
}
