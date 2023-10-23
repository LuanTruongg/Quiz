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
        Task<IEnumerable<GetListMajorResponse>> GetListMajorAsync();
        Task<IEnumerable<GetListDepartmentResponse>> GetListDepartmentAsync();
    }
}
