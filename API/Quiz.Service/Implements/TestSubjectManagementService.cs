using Microsoft.EntityFrameworkCore;
using Quiz.DTO.TestSubjectManagement;
using Quiz.Infrastructure.Http;
using Quiz.Repository;
using Quiz.Repository.Model;
using Serilog;
using System;
using System.Buffers.Text;

namespace Quiz.Service.Implements
{
    public class TestSubjectManagementService : ITestSubjectManagementService
	{
		private readonly QuizDbContext _dbContext;
		public TestSubjectManagementService(QuizDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<CreateTestSubjectResponse> CreateTestSubjectAsync(CreateTestSubjectRequest request)
		{
			var testSubjectExisting = _dbContext.TestSubjects.FirstOrDefault(x => x.TestSubjectCode == request.TestSubjectCode);
			if (testSubjectExisting != null)
			{
				throw new TestException($"Test Subject Code {request.TestSubjectCode} does exist");
			}
			
			var subjectExisting = _dbContext.Modules.FirstOrDefault(x => x.ModuleId == request.ListModuleId[0]);

			var testStructure = await _dbContext.TestStructures.FindAsync(request.TestStructureId);

			var listQuestionOfSubject = from question in _dbContext.Questions
								  join module in _dbContext.Modules on question.ModuleId equals module.ModuleId
								  join subject in _dbContext.Subjects on module.SubjectId equals subject.SubjectId
								  where subject.SubjectId == subjectExisting.SubjectId
								  select new
								  {
									  question.QuestionId,
									  question.ModuleId
								  };
			if (testStructure.NumberOfQuestions > listQuestionOfSubject.Count())
			{
				throw new TestException($"The number of requested questions must be less than the number of existing questions");
			}
			var questionTotalOfModule = Math.Truncate(testStructure.NumberOfQuestions / (decimal)request.ListModuleId.Count);
			var questionRemainder = testStructure.NumberOfQuestions % request.ListModuleId.Count == 0 ? 0 : testStructure.NumberOfQuestions % request.ListModuleId.Count;

			List<string> listQuestionResult = new List<string>();
			for (int j = 0; j < request.ListModuleId.Count; j++)
			{
				var listQuestionOfModule = await listQuestionOfSubject.Where(x => x.ModuleId == request.ListModuleId[j]).ToListAsync();
				for (int k = 0; k < request.ListNumQuestion[j]; k++)
				{
					Random rand = new Random();
					int randomNumber = rand.Next(0, listQuestionOfModule.Count);
					var questionIdRandom = listQuestionOfModule[randomNumber].QuestionId;
					if (!listQuestionResult.Contains(questionIdRandom.ToString()))
					{
						listQuestionResult.Add(questionIdRandom.ToString());
					}
					else
					{
						k--;
					}
				}
			}
			foreach (var question in listQuestionResult) 
			{
				var newQuestion = new TestSubject()
				{
					TestSubjectId = Guid.NewGuid().ToString(),
					TestSubjectCode = request.TestSubjectCode,
					QuestionId = question,
					TestStructureId = request.TestStructureId,
				};
				try
				{
					await _dbContext.TestSubjects.AddAsync(newQuestion);
					await _dbContext.SaveChangesAsync();
				}
				catch (Exception ex)
				{
					throw new TestException($"{ex.Message}");
				}
			}

			return new CreateTestSubjectResponse()
			{
				message = "Create success"
			};
		}

		public async Task<DeleteTestSubjectResponse> DeleteTestSubject(string testSubjectCode)
		{
			var testSubjectExisting = await _dbContext.TestSubjects.FirstOrDefaultAsync(x => x.TestSubjectCode == testSubjectCode);
			if (testSubjectExisting is null)
			{
				throw new TestException($"Not Found Test with Id {testSubjectCode}");
			}
			var listQuestion = await _dbContext.TestSubjects
									.Where(x => x.TestSubjectCode == testSubjectCode)
									.Select(x => x.TestSubjectId).ToListAsync();

			foreach (var item in listQuestion) {
				var getQuestionInTest = await _dbContext.TestSubjects.FirstOrDefaultAsync(x => x.TestSubjectId == item);
				_dbContext.TestSubjects.Remove(getQuestionInTest);
			}
			_dbContext.SaveChanges();
			return new DeleteTestSubjectResponse()
			{
				message = "Delete success"
			};	
		}

        public async Task<List<GetListQuestionOfTestResponse>> GetListQuestionOfTestAsync(GetListQuestionOfTestRequest request)
        {
			var listAllQuestion = await _dbContext.TestSubjects.
				Where(x => x.TestSubjectCode == request.TestSubjectCode)
				.Select(x => new GetListQuestionOfTestResponse
                {
					QuestionId = x.QuestionId,
					QuestionText = x.Question.QuestionText,
					AnswerA = x.Question.QuestionA,
					AnswerB = x.Question.QuestionB,
					AnswerC = x.Question.QuestionC,
					AnswerD = x.Question.QuestionD
				}).ToListAsync();
			return listAllQuestion;
        }

    }
}
