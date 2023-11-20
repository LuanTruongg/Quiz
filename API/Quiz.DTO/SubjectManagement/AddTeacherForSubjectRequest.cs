using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.SubjectManagement
{
    public class AddTeacherForSubjectRequest
    {
        public string SubjectId { get; set; }
        public List<SelectUser> User { get; set; }
    }
    public class SelectUser
    {
        public bool Select { get; set; }
        public string UserId { get; set; }
    }
}
