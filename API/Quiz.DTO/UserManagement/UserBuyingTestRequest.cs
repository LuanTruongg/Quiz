using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.UserManagement
{
    public class UserBuyingTestRequest
    {
        public string UserId { get; set; }
        public string TestStructureId { get; set; }
        public string Price { get; set; }
        public string Money { get; set; }
    }
}
