using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Dto.Mission;
using ToDo.Domain.Models;

namespace ToDo.Application.Interfaces
{
    public interface IMissionService
    {
        Task<int> AddAsync(MissionDto missionDto);
        Task<int> Delete(int id);
        Task<List<MissionAllDto>> GetAllAsync(string userName);
    }
}
