using Microsoft.EntityFrameworkCore;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.QuestionManagement;
using Quiz.DTO.SubjectManagement;
using Quiz.DTO.TestStructureManagement;
using Quiz.Infrastructure.Http;
using Quiz.Repository;
using Quiz.Repository.Model;
using Quiz.Service.Extensions;
using Serilog;
using System.Reflection;

namespace Quiz.Service.Implements
{
    public class QuestionManagementService : IQuestionManagementService
	{
		private readonly QuizDbContext _dbContext;
		public Base64 Base64 { get; set; }
		public QuestionManagementService(QuizDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<ApiResult<bool>> AddQuestionAsync(AddQuestionRequest request)
		{
			var moduleExisting = await _dbContext.Modules.FindAsync(request.ModuleId);
			if (moduleExisting is null) {
				return new ApiErrorResult<bool>($"Not Found Module with Id: {request.ModuleId}");
			}
			var answer = string.Empty;
			switch (request.Answer)
			{
				case 'A':
					answer = request.QuestionA;
					break;
				case 'B':
					answer = request.QuestionB;
					break;
				case 'C':
					answer = request.QuestionC;
					break;
				case 'D':
					answer = request.QuestionD;
					break;
			}
			var newQuestion = new Question()
			{
				QuestionId = Guid.NewGuid().ToString(),
				QuestionText = Base64.Base64Encode(request.QuestionText),
				QuestionA = Base64.Base64Encode(request.QuestionA),
				QuestionB = Base64.Base64Encode(request.QuestionB),
				QuestionC = Base64.Base64Encode(request.QuestionC),
				QuestionD = Base64.Base64Encode(request.QuestionD),
				QuestionCustom = request.QuestionCustom is null? "no custom" : request.QuestionCustom,
				Answer = Base64.Base64Encode(answer),
				Difficult = request.Difficult,
				ModuleId = request.ModuleId,
				
			};
			if (request.Image != null)
			{
				newQuestion.Image = request.Image;
			}
            if (request.Audio != null)
            {
                newQuestion.Audio = request.Audio;
            }
            try
			{
				await _dbContext.Questions.AddAsync(newQuestion);
			} catch (Exception ex)
			{
				Log.Error(ex.Message);
			}
			await _dbContext.SaveChangesAsync();
			var result = _dbContext.Questions.Where(x => x.QuestionId == newQuestion.QuestionId).FirstOrDefault();
			//return new AddQuestionResponse()
			//{
			//	QuestionText = newQuestion.QuestionText,
			//	QuestionA = newQuestion.QuestionA,
			//	QuestionB = newQuestion.QuestionB,
			//	QuestionC = newQuestion.QuestionC,
			//	QuestionD = newQuestion.QuestionD,
			//	Answer = newQuestion.Answer,
			//	ModuleId = newQuestion.ModuleId
			//};
			return new ApiSuccessResult<bool>();
		}

		public async Task<ApiResult<PagedResult<QuestionItem>>> GetListQuestionAsync(GetListQuestionRequest request)
		{
			//filter
			var subjectExisting = await _dbContext.Subjects.FindAsync(request.SubjectId);
			
			if (subjectExisting is null)
			{
				return new ApiErrorResult<PagedResult<QuestionItem>>($"Not Found Subject with Id: {request.SubjectId}");
			}
			var questionAll = _dbContext.Questions.AsQueryable();

			if (!string.IsNullOrEmpty(request.Search))
			{
				var seacrhKeyTitleEncode = Base64.Base64Encode(request.Search).ToString();
				questionAll = questionAll.Where(x => x.QuestionText.Contains(seacrhKeyTitleEncode));
			}
			var getListQuestionOfSubject = from question in questionAll
										   join module in _dbContext.Modules on question.ModuleId equals module.ModuleId
										   join subject in _dbContext.Subjects on module.SubjectId equals subject.SubjectId
										   where subject.SubjectId == request.SubjectId
										   select new {
												QuestionId = question.QuestionId,
												QuestionText = question.QuestionText,
												QuestionA = question.QuestionA,
												QuestionB = question.QuestionB,
												QuestionC = question.QuestionC,
												QuestionD = question.QuestionD,
												Answer = question.Answer,
												Difficult = question.Difficult,
												Name = subject.Name,
												ModuleId = module.ModuleId,
												ModuleName = module.Name
                                           };
				
			//paging
			int totalRow = await getListQuestionOfSubject.CountAsync();

			var data = await getListQuestionOfSubject.Skip((request.Page - 1) * request.PageSize)
				.Take(request.PageSize)
				.Select(x => new QuestionItem()
				{
					QuestionId = x.QuestionId,
					QuestionText = Base64.Base64Decode(x.QuestionText),
					QuestionA = Base64.Base64Decode(x.QuestionA),
					QuestionB = Base64.Base64Decode(x.QuestionB),
					QuestionC = Base64.Base64Decode(x.QuestionC),
					QuestionD = Base64.Base64Decode(x.QuestionD),
					Difficult = x.Difficult,
					Answer = Base64.Base64Decode(x.Answer),
					SubjectName = x.Name,
					ModuleId = x.ModuleId,
					ModuleName = x.ModuleName
				})
                .OrderBy(x => x.ModuleId) 
				.ToListAsync();

			var numberPage = request.Page <= 0 ? 1 : request.Page;
			var numberPageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var pagedResult = new PagedResult<QuestionItem>()
            {
                TotalRecords = totalRow,
                Page = numberPage,
                PageSize = numberPageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<QuestionItem>>(pagedResult);
        }

		public async Task<ApiResult<bool>> DeleteQuestionAsync(string id)
		{
			var questionExisting = await _dbContext.Questions.FindAsync(id);
			if (questionExisting is null)
			{
				return new ApiErrorResult<bool>($"Not Found Subject with Id: {id}");
			}
			try
			{
				_dbContext.Questions.Remove(questionExisting);
				await _dbContext.SaveChangesAsync();
				return new ApiSuccessResult<bool>();
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
                return new ApiErrorResult<bool>(ex.Message);
			}
		}

		public async Task<ApiResult<bool>> EditQuestionAsync(string id, EditQuestionRequest request)
		{
			var moduleExisting = await _dbContext.Modules.FindAsync(request.ModuleId);
			if (moduleExisting is null)
			{
				return new ApiErrorResult<bool>($"Not Found Module with Id: {request.ModuleId}");
			}
			var answer = string.Empty;
			switch (request.Answer)
			{
				case 'A':
					answer = request.QuestionA;
					break;
				case 'B':
					answer = request.QuestionB;
					break;
				case 'C':
					answer = request.QuestionC;
					break;
				case 'D':
					answer = request.QuestionD;
					break;
			}
			var editQuestion = await _dbContext.Questions.FindAsync(id);
			if (editQuestion is null)
			{
                return new ApiErrorResult<bool>($"Not Found Question with Id: {id}");
			}
			editQuestion.QuestionText = Base64.Base64Encode(request.QuestionText);
			editQuestion.QuestionA = Base64.Base64Encode(request.QuestionA);
			editQuestion.QuestionB = Base64.Base64Encode(request.QuestionB);
			editQuestion.QuestionC = Base64.Base64Encode(request.QuestionC);
			editQuestion.QuestionD = Base64.Base64Encode(request.QuestionD);
			editQuestion.Answer = Base64.Base64Encode(answer);
			editQuestion.Difficult = request.Difficult;
			editQuestion.ModuleId = request.ModuleId;
            if (request.Image != null)
            {
                editQuestion.Image = request.Image;
            }
            if (request.Audio != null)
            {
                editQuestion.Audio = request.Audio;
            }
            try
			{
				_dbContext.Questions.Update(editQuestion);
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
			}
			 _dbContext.SaveChanges();
			var findQuestion = _dbContext.Questions.Where(x => x.QuestionId == editQuestion.QuestionId).FirstOrDefault();
			if (findQuestion is null)
			{
                return new ApiErrorResult<bool>($"Not Found Question with Id: {id}");
			}
			var result = new EditQuestionResponse()
			{
				QuestionText = findQuestion.QuestionText,
				QuestionA = findQuestion.QuestionA,
				QuestionB = findQuestion.QuestionB,
				QuestionC = findQuestion.QuestionC,
				QuestionD = findQuestion.QuestionD,
				Answer = findQuestion.Answer,
				Difficult = findQuestion.Difficult,
				ModuleId = findQuestion.ModuleId
			};
			return new ApiSuccessResult<bool>();
		}

        public async Task<ApiResult<GetQuestionResponse>> GetQuestionByIdAsync(string id)
        {
            var questionExisting = _dbContext.Questions.FirstOrDefault(x => x.QuestionId == id);
            if (questionExisting is null)
            {
                return new ApiErrorResult<GetQuestionResponse>($"Not Found question with Id: {id}");
            }
			var moduleExisting = _dbContext.Modules.FirstOrDefault(x => x.ModuleId == questionExisting.ModuleId);
			var result = new GetQuestionResponse()
			{
				QuestionId = questionExisting.QuestionId,
				QuestionText = Base64.Base64Decode(questionExisting.QuestionText),
				QuestionA = Base64.Base64Decode(questionExisting.QuestionA),
				QuestionB = Base64.Base64Decode(questionExisting.QuestionB),
				QuestionC = Base64.Base64Decode(questionExisting.QuestionC),
				QuestionD = Base64.Base64Decode(questionExisting.QuestionD),
				Answer = Base64.Base64Decode(questionExisting.Answer),
				Difficult = questionExisting.Difficult,
				ModuleId = moduleExisting.ModuleId,
				ModuleName = moduleExisting.Name,
				Image = questionExisting.Image,
				Audio = questionExisting.Audio
			};
            return new ApiSuccessResult<GetQuestionResponse>(result);
        }

        public async Task<ApiResult<string>> AddQuestionReturnIdAsync(AddQuestionRequest request)
        {
            var moduleExisting = await _dbContext.Modules.FindAsync(request.ModuleId);
            if (moduleExisting is null)
            {
                return new ApiErrorResult<string>($"Not Found Module with Id: {request.ModuleId}");
            }
            var answer = string.Empty;
            switch (request.Answer)
            {
                case 'A':
                    answer = request.QuestionA;
                    break;
                case 'B':
                    answer = request.QuestionB;
                    break;
                case 'C':
                    answer = request.QuestionC;
                    break;
                case 'D':
                    answer = request.QuestionD;
                    break;
            }
            var newQuestion = new Question()
            {
                QuestionId = Guid.NewGuid().ToString(),
                QuestionText = Base64.Base64Encode(request.QuestionText),
                QuestionA = Base64.Base64Encode(request.QuestionA),
                QuestionB = Base64.Base64Encode(request.QuestionB),
                QuestionC = Base64.Base64Encode(request.QuestionC),
                QuestionD = Base64.Base64Encode(request.QuestionD),
                QuestionCustom = request.QuestionCustom is null ? "no custom" : request.QuestionCustom,
                Answer = Base64.Base64Encode(answer),
                Difficult = request.Difficult,
                ModuleId = request.ModuleId,

            };
            if (request.Image != null)
            {
                newQuestion.Image = request.Image;
            }
            if (request.Audio != null)
            {
                newQuestion.Audio = request.Audio;
            }
            try
            {
                await _dbContext.Questions.AddAsync(newQuestion);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            await _dbContext.SaveChangesAsync();
            var result = _dbContext.Questions.Where(x => x.QuestionId == newQuestion.QuestionId).FirstOrDefault();
            return new ApiSuccessResult<string>(result.QuestionId);
        }
    }
}
