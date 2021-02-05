using Microsoft.EntityFrameworkCore;
using PapoDeDev.TDD.Domain.Core.Interfaces.Repository;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PapoDeDev.TDD.Infra.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RepositoryContext _Context;

        public UnitOfWork(RepositoryContext context) { _Context = context; }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            int result = await _Context.SaveChangesAsync();
            return result;
        }
        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            _Context.ChangeTracker.Entries()
                .Where(e => e.Entity != null).ToList()
                .ForEach(e => e.State = EntityState.Detached);

            return Task.CompletedTask;
        }
    }
}
