﻿namespace MovieManager.Contracts
{
    using System.Collections.Generic;
    using Models;

    public interface IDashboardViewModel
    {
        IList<Movie> Movies { get; }
    }
}