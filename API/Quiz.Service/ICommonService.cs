using Quiz.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Service
{
    public interface ICommonService
    {
        Task<IEnumerable<GetListMajorResponse>> GetListMajorAsync(string departmentId);
        Task<IEnumerable<GetListDepartmentResponse>> GetListDepartmentAsync();
        Task<IEnumerable<GetListSubjectResponse>> GetListSubjectAsync(string majorId);
    }
}
