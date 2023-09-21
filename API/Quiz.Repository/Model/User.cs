using Microsoft.AspNetCore.Identity;

namespace Quiz.Repository.Model
{
    public class User : IdentityUser<string>
    {
        public string UserId { get; set; }
        public string Fullname { get; set; }
        public DateTime DoB { get; set; }
        public string CCCD { get; set; }
        public float Money { get; set; }
        public bool Sex { get; set;}
        public string? Address { get; set;}
        public List<UserTest> UserTests { get; set; }
    }
}
