using Microsoft.EntityFrameworkCore;
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
	}
}
