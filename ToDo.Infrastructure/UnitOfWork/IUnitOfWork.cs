using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Interfaces;

namespace ToDo.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IMissionRepository Mission { get; }
        Task<int> CommitAsync();
        int Commit();
    }
}
