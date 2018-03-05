namespace MovieManager.cs.Contracts
{
    using System.Collections.Generic;
    using Models;

    public interface IDashboardViewModel
    {
        ICollection<Movie> Movies { get; }
    }
}
