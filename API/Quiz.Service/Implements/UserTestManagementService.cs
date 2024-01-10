using Microsoft.EntityFrameworkCore;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.UserManagement;
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
                TimeSubmit = DateTime.Now,
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

        public async Task<ApiResult<List<GetUserTestResponse>>> GetListResultUserTestAsync(string userId)
        {
            var listUserTest = from ut in _dbContext.UserTests
                               join ts in _dbContext.TestStructures on ut.TestStructureId equals ts.TestStructureId
                               where ut.UserId == userId
                               select new GetUserTestResponse()
                               {
                                   UserTestId = ut.UserTestId,
                                   TestStructureId = ut.TestStructureId,
                                   CorrectAnswers = ut.CorrectAnswers,
                                   NumberOfQuestions = ts.NumberOfQuestions,
                                   Score = ut.Score,
                                   TestStructureName = ts.Name,
                                   Time = ts.Time
                               };
            var listResult = new List<GetUserTestResponse>(listUserTest);
            return new ApiSuccessResult<List<GetUserTestResponse>>(listResult);
        }

        public async Task<ApiResult<PagedResult<GetUserTestResponse>>> GetListResultUserTestManagementAsync(GetListResultUserTestRequest request)
        {
            var listUserTest = from ut in _dbContext.UserTests
                               join ts in _dbContext.TestStructures on ut.TestStructureId equals ts.TestStructureId
                               join u in _dbContext.Users on ut.UserId equals u.Id
                               where ut.TestStructureId == ts.TestStructureId && ts.TestStructureId == request.TestStructureId
                               select new GetUserTestResponse()
                               {
                                   UserId = ut.UserId,
                                   FullName = u.Fullname,
                                   TestStructureId = ts.TestStructureId,
                                   CorrectAnswers = ut.CorrectAnswers,
                                   NumberOfQuestions = ts.NumberOfQuestions,
                                   Score = ut.Score,
                                   TimeSubmit = ut.TimeSubmit.Value
                               };

            if (request.Search != null)
            {
                listUserTest = listUserTest
                    .Where(x => x.FullName.Contains(request.Search));
            }

            int totalRow = listUserTest.Count();

            var data = await listUserTest.Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var numberPage = request.Page <= 0 ? 1 : request.Page;
            var numberPageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var result = new PagedResult<GetUserTestResponse>()
            {
                TotalRecords = totalRow,
                Page = numberPage,
                PageSize = numberPageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<GetUserTestResponse>>(result);
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
