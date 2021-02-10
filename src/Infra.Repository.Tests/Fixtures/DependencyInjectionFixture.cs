using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PapoDeDev.TDD.Domain.Core.Entity;
using PapoDeDev.TDD.Domain.Core.Interfaces.Repository;
using PapoDeDev.TDD.Infra.Repository;
using System;

namespace PapoDeDev.TDD.Infra.Repository.Tests.Fixtures
{
    public class DependencyInjectionFixture
    {
        public readonly IServiceProvider _ServiceProvider;

        public DependencyInjectionFixture()
        {
            var services = new ServiceCollection();

            services.AddDbContext<RepositoryContext>(options => options.UseInMemoryDatabase(databaseName: "TEMP"));
            services.AddTransient(typeof(GenericRepositoryCrud<,>));
            services.AddScoped<UnitOfWork>();
            services.AddScoped<IUnitOfWork>(o => o.GetService<UnitOfWork>());

            _ServiceProvider = services.BuildServiceProvider();
            SeedRepositoryContext(_ServiceProvider);
        }

        private static void SeedRepositoryContext(IServiceProvider services)
        {
            RepositoryContext context = services.GetService<RepositoryContext>();

            CreateDeveloper(context);
            CreateDeveloper(context);

            context.SaveChanges();
        }

        private static void CreateDeveloper(DbContext context)
        {
            Developer developer = new Developer() { FirstName = "Victor", LastName = "Fructuoso" };
            context.Set<Developer>().Add(developer);
        }
    }
}