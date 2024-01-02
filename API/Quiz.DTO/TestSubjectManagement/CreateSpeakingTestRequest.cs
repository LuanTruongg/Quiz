using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.TestSubjectManagement
{
    public class CreateSpeakingTestRequest
    {
        public string Name { get; set; }
        public int Time { get; set; }
        public int NumberOfQuestion { get; set; }
        public string SubjectId { get; set; }
        public string SubjectName { get; set; }
        public bool IsFree { get; set; }
        public long Price { get; set; }
        public string htmlEditor { get; set; }
    }
}
