using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Model
{
    public class UserSubject
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
