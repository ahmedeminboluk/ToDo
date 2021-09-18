using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Dto.User;

namespace ToDo.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> Register(UserRegisterDto user);
        Task<bool> Login(UserRegisterDto user);
        Task<bool> ConfirmationEmail(string userId, string token);
        Task<bool> ResetPassword(PasswordResetDto passwordResetDto);
        Task<bool> ResetPasswordConfirm(ResetPasswordConfirmDto resetDto);
        AuthenticationProperties FacebookLogin(string redirectUrl);
        Task<bool> ResponseLogin();
    }
}
