using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.SubjectManagement
{
	public class AddSubjectResponse
	{
		public string SubjectId { get; set; }
		public string Name { get; set; }
		public bool General { get; set; }
		public string MajorId { get; set; }
	}
}
