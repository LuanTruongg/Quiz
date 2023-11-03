using Quiz.DTO.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.SubjectManagement
{
    public class GetListSubjectPagingResponse : PagingListResponse<SubjectItem>
    {

    }
    public class SubjectItem
    {
        public string SubjectId { get; set; }
        public string Name { get; set; }
    }
}
