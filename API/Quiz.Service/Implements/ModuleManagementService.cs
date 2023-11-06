using Microsoft.EntityFrameworkCore;
using Quiz.DTO.ModuleManagement;
using Quiz.Infrastructure.Http;
using Quiz.Repository;
using Quiz.Repository.Model;
using Serilog;

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

		public async Task<GetListModuleResponse> EditModuleAsync(string id, EditModuleRequest request)
		{
			var moduleExisting = await _dbContext.Modules.FindAsync(id);
			if (moduleExisting is null)
			{
				throw new TestException("Module Not Found");
			}
			var subject = await _dbContext.Subjects.FindAsync(moduleExisting.SubjectId);
			if (subject is null)
			{
				throw new TestException("Subject Not Found");
			}
			try
			{
				moduleExisting.Name = request.ModuleName;
				_dbContext.Modules.Update(moduleExisting);
				
			}catch (Exception ex)
			{
				Log.Error(ex.Message, ex);
			}
			await _dbContext.SaveChangesAsync();
			return new GetListModuleResponse()
			{
				Name = moduleExisting.Name,
				SubjectName = subject.Name
			};
		}

		public async Task<IEnumerable<GetListModuleResponse>> GetListModuleAsync(GetListModuleRequest request)
		{
			var subjectExisting = await _dbContext.Subjects.FindAsync(request.SubjectId);
			if(subjectExisting is null)
			{
				throw new TestException("Not Found");
			}
			var data = await _dbContext.Modules
				.Where(x => x.SubjectId == request.SubjectId)
				.Select(x => new GetListModuleResponse()
				{
					ModuleId = x.ModuleId,
					Name = x.Name,
					SubjectName = x.Subject.Name
				}).ToListAsync();
			return data;
		}

		public async Task<GetListModuleResponse> GetModuleByIdAsync(string moduleId)
		{
			var moduleExisting = await _dbContext.Modules.FindAsync(moduleId);
			if (moduleExisting is null)
			{
				throw new TestException("Not Found");
			}
			var data = await _dbContext.Modules
				.Where(x => x.ModuleId == moduleId)
				.Select(x => new GetListModuleResponse()
				{
					Name = x.Name,
					SubjectName = x.Subject.Name
				}).FirstOrDefaultAsync();
			return data;
		}
	}
}
