using Microsoft.EntityFrameworkCore;
using Quiz.DTO.ModuleManagement;
using Quiz.Infrastructure.Http;
using Quiz.Repository;

namespace Quiz.Service.Implements
{
	public class ModuleManagementService : IModuleManagementService
	{
		private readonly QuizDbContext _dbContext;
		public ModuleManagementService(QuizDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IEnumerable<GetModuleResponse>> GetListModuleAsync(GetModuleRequest request)
		{
			var subjectExisting = await _dbContext.Subjects.FindAsync(request.SubjectId);
			if(subjectExisting is null)
			{
				throw new TestException("Not Found");
			}
			var data = await _dbContext.Modules
				.Where(x => x.SubjectId == request.SubjectId).Select(x => new GetModuleResponse()
				{
					Name = x.Name,
					SubjectName = x.Subject.Name
				}).ToListAsync();
			return data;
		}
	}
}
