using Quiz.DTO.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.SubjectManagement
{
    public class GetListSubjectPagingRequest
    {
        public string? UserId { get; set; }
        public string? Search { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool? IsAscSorting { get; set; }
    }

}
