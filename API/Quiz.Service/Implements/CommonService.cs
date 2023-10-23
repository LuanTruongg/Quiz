using Microsoft.EntityFrameworkCore;
using Quiz.DTO.Common;
using Quiz.Repository;
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
                Name = x.Name
            }).ToListAsync();
            return departmentExisting;
        }

        public async Task<IEnumerable<GetListMajorResponse>> GetListMajorAsync()
        {
            var majorExisting = await _dbContext.Majors.Select(x => new GetListMajorResponse
            {
                Name = x.Name
            }).ToListAsync();
            return majorExisting;
        }
    }
}
