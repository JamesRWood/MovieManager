namespace MovieManager
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Input;
    using Autofac;
    using Contracts.Controllers;
    using Contracts.Queries;
    using Contracts.ViewModels;
    using Controllers;
    using DM.MovieApi;
    using DM.MovieApi.MovieDb.Movies;

    public static class AutofacInstaller
    {
        public static IContainer Container { get; set; }

        public static void RegisterComponents()
        {
            var builder = new ContainerBuilder();

            builder.Register(x => MovieDbFactory.Create<IApiMovieRequest>().Value);

            // Controllers
            builder.RegisterType<ApiController>().As<IApiController>().InstancePerLifetimeScope();
            builder.RegisterType<FileController>().As<IFileController>().InstancePerLifetimeScope();

            // Commands
            RegisterType(typeof(ICommand), builder);

            // Queries
            RegisterType(typeof(IQueryBase), builder);

            // ViewModels
            RegisterType(typeof(IViewModel), builder);

            Container = builder.Build();
        }

        private static void RegisterType<T>(T typeToRegister, ContainerBuilder builder) where T : Type
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(t => t.GetTypeInfo().ImplementedInterfaces.Any(i => i == typeToRegister))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}
