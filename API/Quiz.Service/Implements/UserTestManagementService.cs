using Quiz.DTO.UserTestManagement;
using Quiz.Repository;
using Quiz.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Service.Implements
{
    public class UserTestManagementService : IUserTestManagementService
    {
        private readonly QuizDbContext _dbContext;
        public UserTestManagementService(QuizDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AddUserTestResponse> AddUserTestAsync(AddUserTestRequest request)
        {
            var newUserTest = new UserTest()
            {
                UserId = request.UserId,
                TestStructureId = request.TestStructureId,
                UserTestId = request.UserTestId,
                Score = request.Score,
                CorrectAnswers = request.CorrectAnswers,
            };
            try
            {
                await _dbContext.UserTests.AddAsync(newUserTest);
                await _dbContext.SaveChangesAsync();
                return new AddUserTestResponse()
                {
                    IsSuccess = true,
                    Message = null,
                    UserTestId = newUserTest.UserTestId.ToString()
                };
            } catch (Exception ex)
            {
                return new AddUserTestResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    UserTestId = null
                };
            }
        }
    }
}
