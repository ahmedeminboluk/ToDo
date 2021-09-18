using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        TEntity Update(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter);
    }
}
