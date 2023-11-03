using Microsoft.EntityFrameworkCore;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.SubjectManagement;
using Quiz.Infrastructure.Http;
using Quiz.Repository;
using Quiz.Repository.Model;

namespace Quiz.Service.Implements
{
	public class SubjectManagementService : ISubjectManagementService
	{
		private readonly QuizDbContext _dbContext;
		public SubjectManagementService(QuizDbContext dbContext)
        {
            _dbContext = dbContext;
        }
		public async Task<AddSubjectResponse> AddSubjectsAsync(AddSubjectRequest request)
		{
			var subjectExisting = await _dbContext.Subjects.FindAsync(request.SubjectId);
			var newSubject = new Subject();
			var addMajorSubject = new MajorSubject();
			if (subjectExisting == null) { 
				if (request.General)
				{
					newSubject = new Subject 
					{ 
						SubjectId = request.SubjectId,
						Name = request.Name,
						General = true,
					};
					await _dbContext.Subjects.AddAsync(newSubject);
				}
				else
				{
					if (request.MajorId is null) {
						throw new TestException("MajorId cannot be empty");
					}
					newSubject = new Subject
					{
						SubjectId = request.SubjectId,
						Name = request.Name,
						General = false,
					};
					addMajorSubject = new MajorSubject()
					{
						MajorId = request.MajorId,
						SubjectId = request.SubjectId,
					};
					await _dbContext.MajorSubjects.AddAsync(addMajorSubject);
				}
			}
			else
			{
				throw new TestException("SubjectId was already used");
			}
			await _dbContext.Subjects.AddAsync(newSubject);
			await _dbContext.SaveChangesAsync();
			var result = new AddSubjectResponse()
			{
				SubjectId = newSubject.SubjectId,
				Name = newSubject.Name,
				General = newSubject.General,
				MajorId = addMajorSubject.MajorId
			};
			return result;
		}
		public async Task<IEnumerable<GetSubjectResponse>> GetListSubjectsAsync()
		{
			var data = await _dbContext.Subjects.Select(x => new GetSubjectResponse()
			{
				Name = x.Name
			}).ToListAsync();
			return data;
		}

        public async Task<GetListSubjectPagingResponse> GetListSubjectsPagingAsync(PagingRequest request)
        {
            var subjectExisting = _dbContext.Subjects.AsQueryable();
            if (request.Search != null)
            {
                subjectExisting = subjectExisting.Where(x => x.Name.Contains(request.Search));
            }

            int totalRow = subjectExisting.Count();

            var data = await subjectExisting.Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new SubjectItem()
                {
                    SubjectId = x.SubjectId,
					Name = x.Name,
                }).ToListAsync();

            var numberPage = request.Page <= 0 ? 1 : request.Page;
            var numberPageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            return new GetListSubjectPagingResponse()
            {
                Results = data,
                Page = numberPage,
                PageSize = numberPageSize,
                Count = data.Count()
            };
        }
    }
}
