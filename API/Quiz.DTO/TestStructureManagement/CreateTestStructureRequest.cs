﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.TestStructureManagement
{
	public class CreateTestStructureRequest
	{
		public string Name { get; set; }
		public int Time { get; set; }
		public int NumberOfQuestion { get; set; }
        public string SubjectId { get; set; }
    }
}
