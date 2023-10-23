using Microsoft.EntityFrameworkCore;
using Quiz.DTO.QuestionManagement;
using Quiz.DTO.SubjectManagement;
using Quiz.DTO.TestStructureManagement;
using Quiz.Infrastructure.Http;
using Quiz.Repository;
using Quiz.Repository.Model;
using Serilog;

namespace Quiz.Service.Implements
{
	public class TestStructureManagementService : ITestStructureManagementService
	{
		private readonly QuizDbContext _dbContext;
		public TestStructureManagementService(QuizDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<CreateTestStructureResponse> AddTestStructureAsync(CreateTestStructureRequest request)
		{
			var newTestStructure = new TestStructure()
			{
				TestStructureId = Guid.NewGuid().ToString(),
				Name = request.Name,
				Time = request.Time,
				NumberOfQuestions = request.NumberOfQuestion,
				SubjectId = request.SubjectId
			};
			try
			{
				await _dbContext.TestStructures.AddAsync(newTestStructure);
				await _dbContext.SaveChangesAsync();
				return new CreateTestStructureResponse()
				{
					Name = newTestStructure.Name,
					Time = newTestStructure.Time,
					NumberOfQuestion = newTestStructure.NumberOfQuestions,
                    SubjectId = newTestStructure.SubjectId
                };
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
				throw new TestException($"Error: {ex.Message}");
			}
		}

        public async Task<GetListTestStructureResponse> GetListTestStructureAsync(GetListTestStructureRequest request)
        {
			var testStructureExisting = _dbContext.TestStructures.AsQueryable();
            if (request.SubjectId != null)
            {
				testStructureExisting = testStructureExisting.Where(x => x.SubjectId == request.SubjectId);
            }
            int totalRow = testStructureExisting.Count();

            var data = await testStructureExisting.Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new TestStructureItem()
                {
                    TestStructureId = x.TestStructureId,
					Name = x.Name
                }).ToListAsync();

            var numberPage = request.Page <= 0 ? 1 : request.Page;
            var numberPageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            return new GetListTestStructureResponse()
            {
                Results = data,
                Page = numberPage,
                PageSize = numberPageSize,
                Count = data.Count()
            };

        }
    }
}
