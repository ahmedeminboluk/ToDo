using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Dto.Mission;
using ToDo.Application.Interfaces;
using ToDo.Domain.Models;
using ToDo.Infrastructure.UnitOfWork;

namespace ToDo.Application.Services
{
    public class MissionService : IMissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public MissionService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<int> AddAsync(MissionDto missionDto)
        {
            var mission = _mapper.Map<Mission>(missionDto);
            mission.CreatedAt = DateTime.Now;
            mission.Status = false;
            var user = await _userManager.FindByNameAsync(missionDto.UserName);
            mission.User = user;
            await _unitOfWork.Mission.AddAsync(mission);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<int> Delete(int id)
        {
            var result = (await _unitOfWork.Mission.GetAsync(x => x.Id == id)).FirstOrDefault();
            _unitOfWork.Mission.Delete(result);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<List<MissionAllDto>> GetAllAsync(string userName)
        {
            var result = await _unitOfWork.Mission.GetAsync(x => x.User.UserName == userName);
            return _mapper.Map<List<MissionAllDto>>(result);
        }

    }
}
