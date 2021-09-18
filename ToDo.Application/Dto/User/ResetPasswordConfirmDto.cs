using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Dto.User
{
    public class ResetPasswordConfirmDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string PasswordNew { get; set; }
    }
}
