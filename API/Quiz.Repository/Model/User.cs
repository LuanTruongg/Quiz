using Microsoft.AspNetCore.Identity;

namespace Quiz.Repository.Model
{
    public class User : IdentityUser<string>
    {
        public string Fullname { get; set; }
        public DateTime DoB { get; set; }
        public string CCCD { get; set; }
        public float Money { get; set; }
        public bool Sex { get; set;}
        public string? Address { get; set;}
        public List<UserTest> UserTests { get; set; }
        public List<UserSubject>? UserSubjects { get; set; }
        public List<UserStructure> UserStructures { get; set; }
    }
}
