using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Model
{
    public class Module
    {
        public string ModuleId { get; set; }
        public string Name { get; set; }
        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
        public List<Question> Questions { get;}
    }
}
