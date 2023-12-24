using Microsoft.EntityFrameworkCore;
using Quiz.DTO.BaseResponse;
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

		public async Task<ApiResult<bool>> CreateListModuleAsync(CreateModuleRequest request)
		{
			var subjectExisting = await _dbContext.Subjects.FindAsync(request.SubjectId);
			if(subjectExisting is null) {
				return new ApiErrorResult<bool>($"Not Found Subject with id {request.SubjectId}");
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
                return new ApiErrorResult<bool>("Not Found");
			}
			return new ApiSuccessResult<bool>();
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

		public async Task<ApiResult<List<GetListModuleResponse>>> GetListModuleAsync(string subjectId)
		{
			var subjectExisting = await _dbContext.Subjects.FindAsync(subjectId);
			if(subjectExisting is null)
			{
				return new ApiErrorResult<List<GetListModuleResponse>>("Not Found module");
			}
			var data = await _dbContext.Modules
				.Where(x => x.SubjectId == subjectId)
				.Select(x => new GetListModuleResponse()
				{
					ModuleId = x.ModuleId,
					Name = x.Name,
					SubjectName = x.Subject.Name
				}).ToListAsync();
			return new ApiSuccessResult<List<GetListModuleResponse>>(data);
		}

        public async Task<ApiResult<List<int>>> GetListTotalQuestionOfModuleAsync(string subjectId)
        {
            var subjectExisting = await _dbContext.Subjects.FindAsync(subjectId);
            if (subjectExisting is null)
            {
                return new ApiErrorResult<List<int>>("Not Found module");
            }
            var listModule = await _dbContext.Modules.Where(x => x.SubjectId == subjectId).ToListAsync();

			List<int> listTotal = new List<int>();
			foreach(var item in listModule)
			{
				var totalQuestion = await _dbContext.Questions.Where(x => x.ModuleId == item.ModuleId).CountAsync();
                listTotal.Add(totalQuestion);
			}
            return new ApiSuccessResult<List<int>>(listTotal);
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
