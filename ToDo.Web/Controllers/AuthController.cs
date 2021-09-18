using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Application.Dto.User;
using ToDo.Application.Interfaces;
using ToDo.Application.Helper;
using ToDo.Domain.Models;

namespace ToDo.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserRegisterDto loginUser)
        {
            if (!ModelState.IsValid) return View(loginUser);
            bool result = await _userService.Login(loginUser);
            if (result == true) return RedirectToAction("Index", "Mission");
            ModelState.AddModelError("IdPass", "Kullanıcı adı veya parolanızı kontrol ediniz..");
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto registerUser)
        {
            if (!ModelState.IsValid) return View(registerUser);
            bool result = await _userService.Register(registerUser);
            if (result == true) return RedirectToAction("BeforeEmailConfirm");
            return View(registerUser);
        }

        public IActionResult BeforeEmailConfirm()
        {
            return View();
        }

        public async Task<IActionResult> EmailConfirm(string userId, string token)
        {
            var result = await _userService.ConfirmationEmail(userId, token);
            if (result == true) ViewBag.Status = "Email adresiniz onaylanmıştır.";
            else ViewBag.Status = "Email adresiniz onaylanmamıştır.";
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(PasswordResetDto passwordDto)
        {
            var result = await _userService.ResetPassword(passwordDto);
            if (result == true)
            {
                return RedirectToAction("BeforeEmailConfirm");
            }
            ViewBag.Result = "Böyle bir mail adresi bulunamadı";
            return View(passwordDto);
        }

        public IActionResult ResetPasswordConfirm(string userId, string token)
        {
            ResetPasswordConfirmDto resetDto = new ResetPasswordConfirmDto() { UserId = userId, Token = token };
            return View(resetDto);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm(ResetPasswordConfirmDto resetDto)
        {
            var result = await _userService.ResetPasswordConfirm(resetDto);
            if (result == true) return RedirectToAction("Login");
            ModelState.AddModelError("ResetPass", "Şifreniz değiştirilemedi. Lütfen tekrar deneyiniz!");
            return View();
        }

        public IActionResult FacebookLogin(string returnUrl)
        {
            string RedirectUrl = Url.Action("ExternalResponse", "Auth", new { ReturnUrl = returnUrl });
            var properties = _userService.FacebookLogin(RedirectUrl);
            return new ChallengeResult("Facebook", properties);
        }
        
        public async Task<IActionResult> ExternalResponse(string returnUrl = "/")
        {
            var result = await _userService.ResponseLogin();
            if (result == true) return Redirect(returnUrl);
            return RedirectToAction("Login");
        }
    }
}
