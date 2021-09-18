using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Interfaces;
using ToDo.Domain.Models;
using ToDo.Infrastructure.Context;

namespace ToDo.Infrastructure.Repositories
{
    public class MissionRepository : Repository<Mission>, IMissionRepository
    {
        public MissionRepository(ToDoDbContext context) : base(context)
        {
        }
    }
}
