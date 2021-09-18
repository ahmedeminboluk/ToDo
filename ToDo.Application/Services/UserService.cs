using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ToDo.Application.Dto.User;
using ToDo.Application.Interfaces;
using ToDo.Domain.Models;

namespace ToDo.Application.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserService(SignInManager<User> signInManager, UserManager<User> userManager, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> Login(UserRegisterDto loginUser)
         {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);
            var result = await _signInManager.PasswordSignInAsync(user, loginUser.Password, loginUser.RememberMe, false);
            if (result.Succeeded) return true;
            return false;
        }

        public async Task<bool> Register(UserRegisterDto registerUser)
        {
            var user = _mapper.Map<User>(registerUser);
            IdentityResult result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var regisUser = await _userManager.FindByEmailAsync(user.Email);
                string codeHtmlVersion = HttpUtility.UrlEncode(confirmationToken);
                string link = $"https://localhost:44363/auth/EmailConfirm?userid={regisUser.Id}&token={codeHtmlVersion}";
                Helper.EmailConfirmation.SendEmail(link, registerUser.Email);

                return true; 
            }
            return false;
        }

        public async Task<bool> ConfirmationEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            IdentityResult resut = await _userManager.ConfirmEmailAsync(user, token);
            if (resut.Succeeded) return true;
            return false;
        }

        public async Task<bool> ResetPassword(PasswordResetDto passwordResetDto)
        {
            var user = await _userManager.FindByEmailAsync(passwordResetDto.Email);
            if(user != null)
            {
                string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                string token = HttpUtility.UrlEncode(passwordResetToken);
                string link = $"https://localhost:44363/auth/ResetPasswordConfirm?userid={user.Id}&token={token}";
                Helper.PasswordReset.PasswordResetEmail(link, user.Email);

                return true;
            }
            return false;
        }

        public async Task<bool> ResetPasswordConfirm(ResetPasswordConfirmDto resetDto)
        {
            var user = await _userManager.FindByIdAsync(resetDto.UserId);
            if (user != null)
            {
               var result = await _userManager.ResetPasswordAsync(user, resetDto.Token, resetDto.PasswordNew);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    return true;
                }
            }
            return false;
        }

        public AuthenticationProperties FacebookLogin(string redirectUrl)
        {
            AuthenticationProperties properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);
            return properties;
        }

        public async Task<bool> ResponseLogin()
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return false;
            }
            else
            {
                var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);

                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    var user = new User();

                    user.Email = info.Principal.FindFirst(ClaimTypes.Email).Value;
                    user.FullName = info.Principal.FindFirst(ClaimTypes.Name).Value;
                    user.EmailConfirmed = true;
                    string ExternalUserId = info.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;

                    if (info.Principal.HasClaim(x => x.Type == ClaimTypes.Name))
                    {
                        string userName = info.Principal.FindFirst(ClaimTypes.Name).Value;

                        userName = userName.Replace(' ', '-').ToLower() + ExternalUserId.Substring(0, 5).ToString();

                        user.UserName = userName;
                    }
                    else
                    {
                        user.UserName = info.Principal.FindFirst(ClaimTypes.Email).Value;
                    }

                    var user2 = await _userManager.FindByEmailAsync(user.Email);

                    if (user2 == null)
                    {
                        IdentityResult createResult = await _userManager.CreateAsync(user);

                        if (createResult.Succeeded)
                        {
                            IdentityResult loginResult = await _userManager.AddLoginAsync(user, info);

                            if (loginResult.Succeeded)
                            {
                                //     await signInManager.SignInAsync(user, true);

                                await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);

                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        IdentityResult loginResult = await _userManager.AddLoginAsync(user2, info);

                        await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);

                        return true;
                    }
                }
            }
        }
    }
}
