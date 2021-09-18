﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }
    }
}
