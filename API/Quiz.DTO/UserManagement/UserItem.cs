using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.UserManagement
{
    public class UserItem
    {
        public string UserId { get; set; }
        public string Fullname { get; set; }
        public DateTime DoB { get; set; }
        public string CCCD { get; set; }
        public string Email { get; set; }
        public float Money { get; set; }
        public bool Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public List<string> Roles { get; set;}
    }
}
