using MovieManager.Commands;
using MovieManager.Contracts.Commands;

namespace MovieManager
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

            // Commondata class
            builder.RegisterType<CommonDataViewModel>().As<ICommonDataViewModel>().InstancePerLifetimeScope();

            // Controllers
            builder.RegisterType<FileController>().As<IFileController>().InstancePerLifetimeScope();
            builder.RegisterType<MovieController>().As<IMovieController>().InstancePerLifetimeScope();

            // Commands
            builder.RegisterType<ScanForLocalMovieFilesCommand>().As<IScanForLocalMovieFilesCommand>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<QueryForMovieById>().As<IQueryForMovieById>().InstancePerLifetimeScope();
            builder.RegisterType<QueryForMoviesesByTitle>().As<IQueryForMoviesByTitle>().InstancePerLifetimeScope();

            // ViewModels
            builder.RegisterType<DashboardViewModel>().As<IDashboardViewModel>().InstancePerLifetimeScope();

            Container = builder.Build();
        }
    }
}
