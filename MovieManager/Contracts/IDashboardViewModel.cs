using System.Collections.ObjectModel;
using MovieManager.Models;

namespace MovieManager.Contracts
{
    public interface IDashboardViewModel
    {
        ObservableCollection<Movie> Movies { get; set; }
    }
}
