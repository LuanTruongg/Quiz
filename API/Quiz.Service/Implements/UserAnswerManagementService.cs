using Quiz.DTO.UserAnswerManagement;
using Quiz.Repository;
using Quiz.Repository.Model;

namespace Quiz.Service.Implements
{
    public class UserAnswerManagementService : IUserAnswerManagementService
    {
        private readonly QuizDbContext _dbContext;
        public UserAnswerManagementService(QuizDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AddUserAnswerResponse> AddUserAnswerAsync(List<AddUserAnswerRequest> requests)
        {
            foreach (var request in requests)
            {
                var newUserAnswer = new UserAnswer()
                {
                    QuestionId = request.QuestionId,
                    UserAnswerQuestion = request.UserAnswerQuestion,
                    UserTestId = request.UserTestId,
                };
                await _dbContext.UserAnswers.AddAsync(newUserAnswer);
            }
            try
            {
                await _dbContext.SaveChangesAsync();
                return new AddUserAnswerResponse()
                {
                    IsSuccess = true,
                    Message = null,
                    UserTestId = requests[0].UserTestId
                };
            } catch (Exception ex)
            {
                return new AddUserAnswerResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    UserTestId = null
                };
            }
            
        }
    }
}
