﻿namespace MovieManager
{
    using Autofac;
    using Contracts;
    using Contracts.Controllers;
    using Contracts.Queries;
    using Controllers;
    using MovieManager.Commands;
    using MovieManager.Contracts.Commands;
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

            Container = builder.Build();
        }
    }
}
