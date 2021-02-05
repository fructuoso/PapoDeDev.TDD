using Microsoft.EntityFrameworkCore;
using PapoDeDev.TDD.Domain.Core.Entity;
using PapoDeDev.TDD.Domain.Core.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PapoDeDev.TDD.Infra.Repository
{
    public class GenericRepositoryCrud<TKey, TEntity> : GenericRepository<TEntity>, IRepositoryCrud<TKey, TEntity> where TKey : struct where TEntity : BaseEntity<TKey>
    {
        public GenericRepositoryCrud(RepositoryContext context) : base(context) { }

        public async Task<TEntity> AddAsync(TEntity obj)
        {
            await _DbSet.AddAsync(obj);
            return obj;
        }
        public async Task<TEntity> DeleteAsync(TKey id)
        {
            var obj = await _DbSet.FirstOrDefaultAsync(o => o.Id.Equals(id));
            if (obj != null) _DbSet.Remove(obj);
            return obj;
        }
        public async Task<TEntity> GetAsync(TKey id) => await _QueryableReadOnly.FirstOrDefaultAsync(o => o.Id.Equals(id));
        public IEnumerable<TEntity> GetAll() => _QueryableReadOnly;
        public async Task<TEntity> UpdateAsync(TEntity obj)
        {
            if (!await _QueryableReadOnly.AnyAsync(o => o.Id.Equals(obj.Id))) return null;

            _Context.Detach<TEntity>(o => o.Id.Equals(obj.Id));
            _Context.Update(obj);

            return obj;
        }
    }
}