using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.UserManagement
{
    public class AssignRolesRequest
    {
        public string UserId { get; set; }
        public List<SelectItems> Roles { get; set; } = new List<SelectItems>();
    }
}
