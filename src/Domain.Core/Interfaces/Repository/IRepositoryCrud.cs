using PapoDeDev.TDD.Domain.Core.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PapoDeDev.TDD.Domain.Core.Interfaces.Repository
{
    public interface IRepositoryCrud<TKey, TEntity> where TKey : struct where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> AddAsync(TEntity obj);
        Task<TEntity> DeleteAsync(TKey id);
        Task<TEntity> GetAsync(TKey id);
        IEnumerable<TEntity> GetAll();
        Task<TEntity> UpdateAsync(TEntity obj);
    }
}
