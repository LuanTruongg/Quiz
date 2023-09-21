namespace Quiz.Repository.Model
{
    public class MajorSubject
    {
        public string MajorId { get; set; }
        public Major Major { get; set; }
        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
