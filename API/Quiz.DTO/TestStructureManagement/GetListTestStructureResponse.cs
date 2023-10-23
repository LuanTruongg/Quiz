using Quiz.DTO.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.TestStructureManagement
{
    public class GetListTestStructureResponse : PagingListResponse<TestStructureItem>
    {

    }
    public class TestStructureItem
    {
        public string TestStructureId { get; set; }
        public string Name { get; set; }
    }
}
