using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Model
{
    public class Subject
    {
        public string SubjectId { get; set; }
        public string Name { get; set; }
        public bool General { get; set; }
        public List<MajorSubject> MajorSubjects { get; set; }
        public List<Module> Modules { get; set; }
        public List<UserSubject>? UserSubjects { get; set; }
    }
}
