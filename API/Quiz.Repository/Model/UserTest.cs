namespace Quiz.Repository.Model
{
    public class UserTest
    {
        public string UserTestId { get; set; }
        public string TestSubjectId { get; set; }
        public TestSubject TestSubject { get; set; }
        public string UserId { get; set;}
        public User User { get; set; }
        public int CorrectAnswers { get; set; }
        public decimal Score { get; set; }
        //public string UserAnswerId { get; set; }
        //public UserAnswer UserAnswer { get; set; }
        public List<UserAnswer> UserAnswers { get; set; }
    }
}
