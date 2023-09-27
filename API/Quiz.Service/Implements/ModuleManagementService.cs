using Microsoft.EntityFrameworkCore;
using Quiz.DTO.ModuleManagement;
using Quiz.Infrastructure.Http;
using Quiz.Repository;
using Quiz.Repository.Model;

namespace Quiz.Service.Implements
{
	public class ModuleManagementService : IModuleManagementService
	{
		private readonly QuizDbContext _dbContext;
		public ModuleManagementService(QuizDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<CreateModuleResponse>> CreateListModuleAsync(CreateModuleRequest request)
		{
			var subjectExisting = await _dbContext.Subjects.FindAsync(request.SubjectId);
			if(subjectExisting is null) {
				throw new TestException($"Not Found Subject with id {request.SubjectId}");
			}
			for(int i = 1; i <= request.NumberOfMudule; i++)
			{
				var module = new Module()
				{
					ModuleId = request.SubjectId + $"C{i}",
					Name = $"Chương {i}",
					SubjectId = request.SubjectId 
				};
				await _dbContext.Modules.AddAsync(module);
				await _dbContext.SaveChangesAsync();
			}
			var listModuleOfSubject = await _dbContext.Modules
				.Where(x => x.SubjectId == request.SubjectId)
				.Select(x => new CreateModuleResponse()
				{
					ModuleName = x.Name ,
				}).ToListAsync();
			if(listModuleOfSubject is null)
			{
				throw new TestException("Not Found");
			}
			return listModuleOfSubject;
		}

		public async Task<IEnumerable<GetModuleResponse>> GetListModuleAsync(GetModuleRequest request)
		{
			var subjectExisting = await _dbContext.Subjects.FindAsync(request.SubjectId);
			if(subjectExisting is null)
			{
				throw new TestException("Not Found");
			}
			var data = await _dbContext.Modules
				.Where(x => x.SubjectId == request.SubjectId)
				.Select(x => new GetModuleResponse()
				{
					Name = x.Name,
					SubjectName = x.Subject.Name
				}).ToListAsync();
			return data;
		}
	}
}
