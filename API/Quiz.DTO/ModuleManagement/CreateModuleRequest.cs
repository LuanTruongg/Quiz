﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.ModuleManagement
{
	public class CreateModuleRequest
	{
        public string SubjectId { get; set; }
        public int NumberOfMudule { get; set; }
    }
}
