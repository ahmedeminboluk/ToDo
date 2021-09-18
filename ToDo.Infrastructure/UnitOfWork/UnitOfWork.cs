using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Interfaces;
using ToDo.Infrastructure.Context;

namespace ToDo.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IMissionRepository Mission { get; }

        private readonly ToDoDbContext _context;

        public UnitOfWork(IMissionRepository mission, ToDoDbContext context)
        {
            Mission = mission;
            _context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}
