using Microsoft.Extensions.DependencyInjection;
using PapoDeDev.TDD.Domain.Core.Entity;
using PapoDeDev.TDD.Infra.Repository;
using System;
using System.Linq;

namespace WebAPI.Tests.Setup
{
    public static class RepositoryContextExtensions
    {
        public static IServiceProvider SeedRepositoryContext(this IServiceProvider services)
        {
            RepositoryContext context = services.GetService<RepositoryContext>();

            context.SeedDeveloper();

            context.SaveChanges();

            return services;
        }

        private static RepositoryContext SeedDeveloper(this RepositoryContext context)
        {
            context.Set<Developer>().Add(new Developer() { Id = Guid.Parse("0d46be7a-3417-4a06-adff-3e1090bf4ea9") });
            context.Set<Developer>().Add(new Developer() { Id = Guid.Parse("d13a07df-085d-4830-a26d-976fa06c1074") });
            
            return context;
        }
    }
}
