namespace Quiz.Repository.Model
{
    public class UserTest
    {
        public string UserTestId { get; set; }
        public string UserId { get; set;}
        public User User { get; set; }
        public int CorrectAnswers { get; set; }
        public decimal Score { get; set; }
        //public string UserAnswerId { get; set; }
        //public UserAnswer UserAnswer { get; set; }
        public List<UserAnswer> UserAnswers { get; set; }
        public string TestStructureId { get; set; }
        public TestStructure TestStructure { get; set; }
        public DateTime? TimeSubmit { get; set; }
        public string? DateSubmit { get; set; }
    }
}
