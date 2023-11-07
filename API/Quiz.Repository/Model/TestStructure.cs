using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Model
{
    public class TestStructure
    {
        public string TestStructureId { get; set; }
        public string Name { get; set; }
        public int Time { get; set; }
        public int NumberOfQuestions { get; set; }
        public string SubjectId { get; set; }
        public List<TestSubject> TestSubjects { get; set; }
        public List<UserTest> UserTest { get; set; }
    }
}
