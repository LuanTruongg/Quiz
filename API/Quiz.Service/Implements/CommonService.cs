using Microsoft.EntityFrameworkCore;
using Quiz.DTO.Common;
using Quiz.Repository;
using Quiz.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Service.Implements
{
    public class CommonService : ICommonService
    {
        private readonly QuizDbContext _dbContext;
        public CommonService(QuizDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<GetListDepartmentResponse>> GetListDepartmentAsync()
        {
            var departmentExisting = await _dbContext.Departments.Select(x => new GetListDepartmentResponse
            {
                DepartmentId = x.DepartmentId,
                Name = x.Name
            }).ToListAsync();
            return departmentExisting;
        }

        public async Task<IEnumerable<GetListMajorResponse>> GetListMajorAsync(string departmentId)
        {
            var majorExisting = _dbContext.Majors.AsQueryable();
            if (departmentId != null)
            {
                majorExisting = majorExisting.Where(x => x.DepartmentId == departmentId);
            }

            var result = await majorExisting.Select(x => new GetListMajorResponse
            {
                MajorId = x.MajorId,
                Name = x.Name
            }).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<GetListSubjectResponse>> GetListSubjectAsync(string majorId)
        {
            var subjectExisting = _dbContext.Subjects.AsQueryable();
            if (majorId == null)
            {
                var result = await subjectExisting.Select(x => new GetListSubjectResponse
                {
                    SubjectId = x.SubjectId,
                    Name = x.Name
                }).ToListAsync();
                return result;
            }
            var subjectExistingFilter = from subject in subjectExisting
                                        join subjectMajor in _dbContext.MajorSubjects on subject.SubjectId equals subjectMajor.SubjectId
                                        where subjectMajor.MajorId == majorId
                                        select new GetListSubjectResponse()
                                        {
                                            SubjectId = subject.SubjectId,
                                            Name = subject.Name
                                        };
            var result2 = await subjectExistingFilter.ToListAsync();
            return result2;
        }
    }
}
