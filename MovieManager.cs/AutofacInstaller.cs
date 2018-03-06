namespace MovieManager.cs
{
    using Autofac;
    using Contracts;
    using Contracts.Controllers;
    using Contracts.Queries;
    using Controllers;
    using Queries;
    using ViewModels;

    public static class AutofacInstaller
    {
        public static IContainer Container { get; set; }

        public static void RegisterComponents()
        {
            var builder = new ContainerBuilder();

            // Controllers
            builder.RegisterType<MovieController>().As<IMovieController>().InstancePerLifetimeScope();

            // ViewModels
            builder.RegisterType<DashboardViewModel>().As<IDashboardViewModel>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<QueryForMovieById>().As<IQueryForMovieById>().InstancePerLifetimeScope();
            builder.RegisterType<QueryForMoviesesByTitle>().As<IQueryForMoviesByTitle>().InstancePerLifetimeScope();

            Container = builder.Build();
        }
    }
}
