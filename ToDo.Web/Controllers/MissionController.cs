using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Application.Dto.Mission;
using ToDo.Application.Interfaces;

namespace ToDo.Web.Controllers
{
    [Authorize]
    public class MissionController : Controller
    {
        private readonly IMissionService _missionService;

        public MissionController(IMissionService missionService)
        {
            _missionService = missionService;
        }

        public async Task<IActionResult> Index()
        {
            var userName = HttpContext.User.Identity.Name;
            var listMission = await _missionService.GetAllAsync(userName);
            return View(listMission);
        }

        public IActionResult AddMission()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMission(MissionDto missionDto)
        {
            var result = await _missionService.AddAsync(missionDto);
            if (result > 0) return RedirectToAction("Index");
            return View(missionDto);
        }

        public async Task<IActionResult> DeleteMission(int id)
        {
            var result = await _missionService.Delete(id);
            if (result > 0) return RedirectToAction("Index");
            return View();
        }
    }
}
