using Microsoft.AspNetCore.Mvc.RazorPages;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.Common;
using Quiz.DTO.ModuleManagement;
using Quiz.DTO.SubjectManagement;
using Quiz.DTO.UserManagement;
using Quiz.Repository.Model;

namespace Quiz.UI.ServicesClient
{
    public interface ISubjectServiceClient
    {
        Task<ApiResult<PagedResult<SubjectItem>>> GetListSubjectPaging(GetListSubjectPagingRequest request);
        Task<ApiResult<PagedResult<SubjectItem>>> GetListAllSubjectPaging(GetListSubjectPagingRequest request);
        Task<ApiResult<List<GetListModuleResponse>>> GetListModuleOfSubject(string subjectId);
        Task<ApiResult<SubjectItem>> GetSubjectById(string id);
        Task<List<GetTeacherItem>> GetListTeacherOfSubject(string subjectId);
        Task<List<GetTeacherItem>> GetListTeacher();
        Task<ApiResult<bool>> AddTeacherForSubject(AddTeacherForSubjectRequest request);
        Task<ApiResult<PagedResult<UserStructureItem>>> GetListUserBoughtTest(GetListUserStructureRequest request);
        Task<ApiResult<bool>> AddSubject(AddSubjectRequest request);
        Task<ApiResult<bool>> DeleteSubject(string id);
        Task<ApiResult<List<int>>> GetListTotalQuestionOfModule(string subjectId);
        Task<ApiResult<bool>> CreateModule(CreateModuleRequest request);
    }
}
