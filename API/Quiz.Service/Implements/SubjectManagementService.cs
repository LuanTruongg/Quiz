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
		public async Task<ApiResult<bool>> AddSubjectsAsync(AddSubjectRequest request)
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
						return new ApiErrorResult<bool>("MajorId cannot be empty");
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
                return new ApiErrorResult<bool>("SubjectId was already used");
			}
			await _dbContext.Subjects.AddAsync(newSubject);
			await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
		}

        public async Task<ApiResult<bool>> AddTeachersForSubjectAsync(AddTeacherForSubjectRequest request)
        {
			var subjectExisting = _dbContext.Subjects.FirstOrDefault(x => x.SubjectId == request.SubjectId);
			if (subjectExisting == null)
			{
				return new ApiErrorResult<bool>("Môn học không tồn tại");
			}
			foreach(var user in request.User)
			{
				if(user.Select is true)
				{
					var userExisting = _dbContext.UserSubjects
						.Where(x => x.SubjectId == request.SubjectId)
						.FirstOrDefault(x => x.UserId == user.UserId);
					if(userExisting is null)
					{
						var newUserSubject = new UserSubject()
						{
							SubjectId = request.SubjectId,
							UserId = user.UserId,
						};
						try
						{
                            await _dbContext.UserSubjects.AddAsync(newUserSubject);
                            await _dbContext.SaveChangesAsync();
							return new ApiSuccessResult<bool>();
                        }
						catch (Exception ex)
						{
							return new ApiErrorResult<bool>(ex.Message);
						}
					}               
                }
                
            }
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> DeleteSubjectAsync(string id)
        {
            var subjectExisting = _dbContext.Subjects.FirstOrDefault(x => x.SubjectId == id);
			if (subjectExisting == null)
			{
                return new ApiErrorResult<bool>("Học phần không tồn tại");
            }
			_dbContext.Subjects.Remove(subjectExisting);
			_dbContext.SaveChanges();
            return new ApiSuccessResult<bool>();
        }

        public async Task<IEnumerable<GetSubjectResponse>> GetListSubjectsAsync()
		{
			var data = await _dbContext.Subjects.Select(x => new GetSubjectResponse()
			{
				Name = x.Name
			}).ToListAsync();
			return data;
		}

        public async Task<ApiResult<PagedResult<SubjectItem>>> GetListSubjectsPagingAsync(GetListSubjectPagingRequest request)
        {
			IQueryable<SubjectItem> subjectExisting;
            if (request.UserId != null)
            {
                var subjectExisting1 = from us in _dbContext.UserSubjects
                                      join s in _dbContext.Subjects on us.SubjectId equals s.SubjectId
                                      where us.UserId == request.UserId
                                      select new SubjectItem
                                      {
                                          SubjectId = us.SubjectId,
                                          Name = s.Name
                                      };
				subjectExisting = subjectExisting1;
            }
			else
			{
                var subjectExisting2= from s in _dbContext.Subjects
                                      select new SubjectItem
                                      {
                                          SubjectId = s.SubjectId,
                                          Name = s.Name
                                      };
				subjectExisting = subjectExisting2;
            }
            
            if (request.Search != null)
            {
                subjectExisting = subjectExisting.Where(x => x.Name.Contains(request.Search) || x.SubjectId.Contains(request.Search));
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

            var pagedResult = new PagedResult<SubjectItem>()
            {
                TotalRecords = totalRow,
                Page = numberPage,
                PageSize = numberPageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<SubjectItem>>(pagedResult);
        }

        public async Task<ApiResult<SubjectItem>> GetSubjectByIdAsync(string id)
        {
			var subjectExisting = _dbContext.Subjects.FirstOrDefault(x => x.SubjectId == id);
			if (subjectExisting == null)
				return new ApiErrorResult<SubjectItem>($"Subject with id {id} does not exist");
			var result = new SubjectItem()
			{
				SubjectId = subjectExisting.SubjectId,
				Name = subjectExisting.Name,
			};
			return new ApiSuccessResult<SubjectItem>(result);
        }
    }
}
