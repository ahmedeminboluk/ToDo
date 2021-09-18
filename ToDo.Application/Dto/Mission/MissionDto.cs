using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Dto.Mission
{
    public class MissionDto
    {
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Status { get; set; }
        public string UserName { get; set; }
    }
}
