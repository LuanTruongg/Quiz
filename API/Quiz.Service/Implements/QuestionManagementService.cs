﻿using Microsoft.EntityFrameworkCore;
using Quiz.DTO.QuestionManagement;
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
		public async Task<AddQuestionResponse> AddQuestionAsync(AddQuestionRequest request)
		{
			var moduleExisting = await _dbContext.Modules.FindAsync(request.ModuleId);
			if (moduleExisting is null) {
				throw new TestException($"Not Found Module with Id: {request.ModuleId}");
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
				QuestionCustom = "test",
				Answer = Base64.Base64Encode(answer),
				Difficult = request.Difficult,
				ModuleId = request.ModuleId
			};
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
			return new AddQuestionResponse()
			{
				QuestionText = result.QuestionText,
				QuestionA = result.QuestionA,
				QuestionB = result.QuestionB,
				QuestionC = result.QuestionC,
				QuestionD = result.QuestionD,
				Answer = result.Answer,
				Difficult = result.Difficult,
				ModuleId = result.ModuleId
			};
		}

		public async Task<GetQuestionListResponse> GetListQuestionAsync(GetQuestionListRequest request)
		{
			//filter
			var subjectExisting = await _dbContext.Subjects.FindAsync(request.SubjectId);
			
			if (subjectExisting is null)
			{
				throw new TestException($"Not Found Subject with Id: {request.SubjectId}");
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
										   select new {
											   question.QuestionText,
											   question.QuestionA,
											   question.QuestionB,
											   question.QuestionC,
											   question.QuestionD,
											   subject.Name
										   };
				
			//paging
			int totalRow = await getListQuestionOfSubject.CountAsync();

			var data = await getListQuestionOfSubject.Skip((request.Page - 1) * request.PageSize)
				.Take(request.PageSize)
				.Select(x => new QuestionItem()
				{
					QuestionText = Base64.Base64Decode(x.QuestionText),
					QuestionA = Base64.Base64Decode(x.QuestionA),
					QuestionB = Base64.Base64Decode(x.QuestionB),
					QuestionC = Base64.Base64Decode(x.QuestionC),
					QuestionD = Base64.Base64Decode(x.QuestionD),
					SubjectName = x.Name
				}).ToListAsync();

			var numberPage = request.Page <= 0 ? 1 : request.Page;
			var numberPageSize = request.PageSize <= 0 ? 10 : request.PageSize;

			return new GetQuestionListResponse()
			{
				Results = data,
				Page = numberPage,
				PageSize = numberPageSize,
				Count = data.Count()
			};
		}

		public async Task<string> DeleteQuestionAsync(string id)
		{
			var questionExisting = await _dbContext.Questions.FindAsync(id);
			if (questionExisting is null)
			{
				throw new TestException($"Not Found Subject with Id: {id}");
			}
			try
			{
				_dbContext.Questions.Remove(questionExisting);
				await _dbContext.SaveChangesAsync();
				return $"Delete success question: {Base64.Base64Decode(questionExisting.QuestionText)} ";
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
				throw new TestException(ex.Message);
			}
		}

		public async Task<EditQuestionResponse> EditQuestionAsync(string id, EditQuestionRequest request)
		{
			var moduleExisting = await _dbContext.Modules.FindAsync(request.ModuleId);
			if (moduleExisting is null)
			{
				throw new TestException($"Not Found Module with Id: {request.ModuleId}");
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
				throw new TestException($"Not Found Question with Id: {id}");
			}
			editQuestion.QuestionText = Base64.Base64Encode(request.QuestionText);
			editQuestion.QuestionA = Base64.Base64Encode(request.QuestionA);
			editQuestion.QuestionB = Base64.Base64Encode(request.QuestionB);
			editQuestion.QuestionC = Base64.Base64Encode(request.QuestionC);
			editQuestion.QuestionD = Base64.Base64Encode(request.QuestionD);
			editQuestion.Answer = Base64.Base64Encode(answer);
			editQuestion.Difficult = request.Difficult;
			editQuestion.ModuleId = request.ModuleId;
			try
			{
				_dbContext.Questions.Update(editQuestion);
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
			}
			 _dbContext.SaveChanges();
			var result = _dbContext.Questions.Where(x => x.QuestionId == editQuestion.QuestionId).FirstOrDefault();
			if (result is null)
			{
				throw new TestException($"Not Found Question with Id: {id}");
			}
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
			return new EditQuestionResponse()
			{
				QuestionText = result.QuestionText,
				QuestionA = result.QuestionA,
				QuestionB = result.QuestionB,
				QuestionC = result.QuestionC,
				QuestionD = result.QuestionD,
				Answer = result.Answer,
				Difficult = result.Difficult,
				ModuleId = result.ModuleId
			};
		}
	}
}
