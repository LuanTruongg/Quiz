namespace Quiz.Repository.Model
{
    public class UserAnswer
    {
        public string UserAnswerId { get; set; }
        public string QuestionId { get; set;}
        //public Question Question { get; set;}
        public string UserAnswerQuestion { get; set; }
        public string UserTestId { get; set; }
        public UserTest UserTest { get; set; }
    }
}
