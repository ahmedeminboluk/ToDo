using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Interfaces;
using ToDo.Infrastructure.Context;

namespace ToDo.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ToDoDbContext _context = null;
        private readonly DbSet<TEntity> _entity;

        protected Repository(ToDoDbContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entity.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _entity.Remove(entity);
        }

        public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entity.Where(filter).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
