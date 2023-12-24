namespace Quiz.DTO.SubjectManagement
{
	public class AddSubjectRequest
	{
		public string SubjectId { get; set; }
		public string Name { get; set; }
		public bool General { get; set; } = false;
        public string MajorId { get; set; }
        public int Module { get; set; }
    }
}
