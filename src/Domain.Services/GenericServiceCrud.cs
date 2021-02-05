using PapoDeDev.TDD.Domain.Core.Entity;
using PapoDeDev.TDD.Domain.Core.Interfaces.Repository;
using PapoDeDev.TDD.Domain.Core.Interfaces.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PapoDeDev.TDD.Domain.Services
{
    public abstract class GenericServiceCrud<TKey, TEntity> : IServiceCrud<TKey, TEntity> where TKey : struct where TEntity : BaseEntity<TKey>
    {
        protected readonly IUnitOfWork _UnitOfWork;
        private readonly IRepositoryCrud<TKey, TEntity> _Repository;

        protected GenericServiceCrud(IRepositoryCrud<TKey, TEntity> repository, IUnitOfWork unitOfWork)
        {
            _Repository = repository;
            _UnitOfWork = unitOfWork;
        }

        public virtual async Task<TEntity> AddAsync(TEntity obj)
        {
            await _Repository.AddAsync(obj);
            await _UnitOfWork.CommitAsync();
            return obj;
        }
        public virtual async Task<TEntity> DeleteAsync(TKey id)
        {
            TEntity entity = await _Repository.DeleteAsync(id);
            if (entity != null) await _UnitOfWork.CommitAsync();
            return entity;
        }
        public virtual Task<TEntity> GetAsync(TKey id) => _Repository.GetAsync(id);
        public virtual IEnumerable<TEntity> GetAll() => _Repository.GetAll();
        public virtual async Task<TEntity> UpdateAsync(TEntity obj)
        {
            TEntity entity = await _Repository.UpdateAsync(obj);
            if (entity != null) await _UnitOfWork.CommitAsync();
            return entity;
        }
    }
}
