using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.UserTestManagement
{
    public class GetListResultUserTestRequest
    {
        public string? Search { get; set; }
        public string TestStructureId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool? IsAscSorting { get; set; }
    }
}
