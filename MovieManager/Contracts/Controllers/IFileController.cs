﻿namespace MovieManager.Contracts.Controllers
{
    using System.Collections.Generic;
    using Models;

    public interface IFileController
    {
        List<Movie> FindLocalMovieFiles();
    }
}
