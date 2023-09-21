using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Model
{
    public class Department
    {
        public string DepartmentId { get; set; }
        public string Name { get; set; }
        public List<Major> Majors { get; set; }
    }
}
