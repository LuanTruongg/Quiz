
namespace Quiz.Repository.Model
{
    public class TestSubject
    {
        public string TestSubjectId { get; set; }
		public string TestSubjectCode{ get; set; }
		public string TestStructureId { get; set; }
        public TestStructure TestStructure { get; set;}
        public string QuestionId { get; set; }
        public Question Question { get; set; }
	}
}
