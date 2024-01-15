using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.SubjectManagement
{
    public class DeleteTeacherForSubjectRequest
    {
        public string UserId { get; set; }
        public string SubjectId { get; set; }
    }
}
