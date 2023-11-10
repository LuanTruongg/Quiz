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
        public string TestSubjectCode { get; set; }
        public string TestStructureId { get; set; }
        public string Name { get; set; }
        public int NumberOfQuestion { get; set; }
        public int Time { get; set; }
        public bool IsFree { get; set; }
        public long Price { get; set; }
    }
}
