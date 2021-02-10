using Bogus;
using Microsoft.Extensions.DependencyInjection;
using PapoDeDev.TDD.Domain.Core.Entity;
using PapoDeDev.TDD.Infra.Repository;
using System;
using System.Collections.Generic;
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
            context.Set<Developer>().Add(CreateDeveloper(Guid.Parse("0d46be7a-3417-4a06-adff-3e1090bf4ea9")));
            context.Set<Developer>().Add(CreateDeveloper(Guid.Parse("d13a07df-085d-4830-a26d-976fa06c1074")));
            context.Set<Developer>().AddRange(CreateDevelopers(100));

            return context;
        }

        private static Developer CreateDeveloper(Guid id)
        {
            return new Faker<Developer>("pt_BR")
                .RuleFor(o => o.Id, id)
                .RuleFor( o => o.FirstName, f => f.Name.FirstName())
                .RuleFor( o => o.LastName, f => f.Name.LastName())
                .Generate();
        }

        private static IEnumerable<Developer> CreateDevelopers(int count)
        {
            return new Faker<Developer>("pt_BR")
                .RuleFor(o => o.Id, f => f.Random.Guid())
                .RuleFor( o => o.FirstName, f => f.Name.FirstName())
                .RuleFor( o => o.LastName, f => f.Name.LastName())
                .Generate(count);
        }
    }
}
