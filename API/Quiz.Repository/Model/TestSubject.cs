using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Model
{
    public class TestSubject
    {
        public string TestSubjectId { get; set; }
        public string TestStructureId { get; set; }
        public TestStructure TestStructure { get; set;}
        public string QuestionId { get; set; }
        public Question Question { get; set; }
        public List<UserTest> UserTest { get; set; }
    }
}
