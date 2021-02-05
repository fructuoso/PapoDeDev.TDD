using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace PapoDeDev.TDD.Infra.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public void Detach<TEntity>(Func<TEntity, bool> predicate) where TEntity : class
        {
            var entities = Set<TEntity>().Local.Where(predicate);
            foreach (var entity in entities)
            {
                Entry(entity).State = EntityState.Detached;
            }
        }
    }
}
