using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Model
{
    public class Major
    {
        public string MajorId { get; set; }
        public string Name { get; set; }
        public string DepartmentId { get; set; }
        public Department Department { get; set; }
        public List<MajorSubject> MajorSubjects { get; set; }
    }
}
