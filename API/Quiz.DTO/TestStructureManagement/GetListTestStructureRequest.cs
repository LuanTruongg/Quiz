﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.TestStructureManagement
{
    public class GetListTestStructureRequest
    {
        public string? Search { get; set; }
        public string? SubjectId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool? IsAscSorting { get; set; }
    }
}
