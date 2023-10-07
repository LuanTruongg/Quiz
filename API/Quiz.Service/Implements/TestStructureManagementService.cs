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
				NumberOfQuestions = request.NumberOfQuestion
			};
			try
			{
				await _dbContext.TestStructures.AddAsync(newTestStructure);
				await _dbContext.SaveChangesAsync();
				return new CreateTestStructureResponse()
				{
					Name = newTestStructure.Name,
					Time = newTestStructure.Time,
					NumberOfQuestion = newTestStructure.NumberOfQuestions
				};
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
				throw new TestException($"Error: {ex.Message}");
			}
		}
	}
}
