namespace MovieManager
{
    using System.Windows;
    using Autofac;
    using Autofac.Core;
    using Commands;
    using Commands.RelayCommands;
    using Contracts.Commands;
    using Contracts.Commands.RelayCommands;
    using Contracts.Controllers;
    using Contracts.Queries;
    using Contracts.ViewModels;
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
            builder.RegisterType<CommonDataViewModel>().As<ICommonDataViewModel>().SingleInstance();

            // Controllers
            builder.RegisterType<ApiController>().As<IApiController>().InstancePerLifetimeScope();
            builder.RegisterType<FileController>().As<IFileController>().InstancePerLifetimeScope();

            // Commands
            builder.RegisterType<ScanForLocalMovieFilesCommand>().As<IScanForLocalMovieFilesCommand>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<QueryForMovieById>().As<IQueryForMovieById>().InstancePerLifetimeScope();
            builder.RegisterType<QueryForMoviesByTitle>().As<IQueryForMoviesByTitle>().InstancePerLifetimeScope();

            // ViewModels
            builder.RegisterType<DashboardViewModel>().As<IDashboardViewModel>().InstancePerLifetimeScope();
            builder.RegisterType<FindMovieDetailsViewModel>().As<IFindMovieDetailsViewModel>().InstancePerLifetimeScope();

            // Open window commands
            builder.RegisterType<OpenWindowCommand>().As<IOpenWindowCommand>().InstancePerLifetimeScope();

            Container = builder.Build();
        }
    }
}
