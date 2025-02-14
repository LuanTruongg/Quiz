﻿using Quiz.DTO.BaseResponse;
using Quiz.DTO.Common;
using Quiz.DTO.UserManagement;
using Quiz.Repository.Model;
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
        Task<List<GetListMajorResponse>> GetListAllMajorAsync();
        Task<IEnumerable<GetListDepartmentResponse>> GetListDepartmentAsync();
        Task<IEnumerable<GetListSubjectResponse>> GetListSubjectAsync(string majorId);
        Task<string> GetTestSubjectCode(string testStructureId);
        Task<List<RoleItem>> GetRolesAsync();
        Task<List<GetTeacherItem>> GetListTeacherAsync();
        Task<List<GetTeacherItem>> GetListTeacherOfSubjectAsync(string subjectId);
        Task<ApiResult<Major>> GetMajorAsync(string majorId);
    }
}
