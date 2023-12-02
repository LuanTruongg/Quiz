using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quiz.DTO.Common;
using Quiz.DTO.UserManagement;
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
        private readonly RoleManager<IdentityRole<string>> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly QuizDbContext _dbContext;
        public CommonService(QuizDbContext dbContext, RoleManager<IdentityRole<string>> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<List<GetListMajorResponse>> GetListAllMajorAsync()
        {
            var majorExisting = _dbContext.Majors.AsQueryable();

            var result = await majorExisting.Select(x => new GetListMajorResponse
            {
                MajorId = x.MajorId,
                Name = x.Name
            }).ToListAsync();
            return result;
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

        public async Task<List<GetTeacherItem>> GetListTeacherAsync()
        {
            var data = from u in _dbContext.Users
                       join ur in _dbContext.UserRoles on u.Id equals ur.UserId
                       join r in _dbContext.Roles on ur.RoleId equals r.Id
                       where r.Name == "Teacher"
                       select new GetTeacherItem()
                       {
                           UserId = ur.UserId,
                           FullName = u.Fullname,
                           Email = u.Email,
                           Sex = u.Sex
                       };
            List<GetTeacherItem> getListTeacher = new List<GetTeacherItem>(data);
            return getListTeacher;
        }

        public async Task<List<GetTeacherItem>> GetListTeacherOfSubjectAsync(string subjectId)
        {
            var data = from u in _dbContext.Users
                       join us in _dbContext.UserSubjects on u.Id equals us.UserId
                       where us.SubjectId == subjectId
                       select new GetTeacherItem()
                       {
                           UserId = us.UserId,
                           FullName = u.Fullname,
                           Email = u.Email,
                           Sex = u.Sex,
                       };
            List<GetTeacherItem> getListTeacher = new List<GetTeacherItem>(data);
            return getListTeacher;
        }

        public Task<List<RoleItem>> GetRolesAsync()
        {
            var rolesExisting = _roleManager.Roles.AsQueryable();
            var listRole = rolesExisting.Select(x => new RoleItem()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return listRole;
        }

        public async Task<string> GetTestSubjectCode(string testStructureId)
        {
            var testSubjectCode = _dbContext.TestSubjects
                .Select(x => new
                {
                    TestStructureId = x.TestStructureId,
                    TestSubjectCode = x.TestSubjectCode
                })
                .FirstOrDefault(x => x.TestStructureId == testStructureId);
            return testSubjectCode.TestSubjectCode;
        }
    }
}
