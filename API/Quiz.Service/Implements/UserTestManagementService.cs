using Microsoft.EntityFrameworkCore;
using Quiz.DTO.UserTestManagement;
using Quiz.Repository;
using Quiz.Repository.Model;
using Quiz.Service.Extensions;
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

        public async Task<GetResultUserTestResponse> GetResultUserTestAsync(GetResultUserTestRequest request)
        {
            var userTestExisting = _dbContext.UserTests
                .FirstOrDefault(x => x.UserTestId == request.UserTestId);

            var testStructureExisting = _dbContext.TestStructures
                .FirstOrDefault(x => x.TestStructureId == userTestExisting.TestStructureId);

            var listUserAnswer = await _dbContext.UserAnswers
                .Where(x => x.UserTestId == request.UserTestId)
                .ToListAsync();

            var correctAnswers = 0;
            foreach (var answer in listUserAnswer)
            {
                var getQuestion = _dbContext.Questions.Where(x => x.QuestionId == answer.QuestionId).FirstOrDefault();
                if(answer.UserAnswerQuestion == getQuestion.Answer)
                {
                    correctAnswers++;
                }
            }
            var score = (double)10 / testStructureExisting.NumberOfQuestions * correctAnswers;


            userTestExisting.Score = (decimal)RoundToNearestQuarter(score);
            userTestExisting.CorrectAnswers = correctAnswers;

            _dbContext.Update(userTestExisting);
            _dbContext.SaveChanges();

            return new GetResultUserTestResponse()
            {
                CorrectAnswers = correctAnswers,
                Score = (decimal)RoundToNearestQuarter(score)
            };
        }

        private double RoundToNearestQuarter(double number)
        {
            return Math.Round(number * 4) / 4.0;
        }
    }
}
