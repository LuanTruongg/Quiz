using Quiz.DTO.BaseResponse;
using Quiz.DTO.ModuleManagement;
using Quiz.DTO.SubjectManagement;
using Quiz.Repository.Model;

namespace Quiz.UI.ServicesClient
{
    public interface ISubjectServiceClient
    {
        Task<ApiResult<PagedResult<SubjectItem>>> GetListSubjectPaging(GetListSubjectPagingRequest request);
        Task<List<GetListModuleResponse>> GetListModuleOfSubject(string subjectId);
    }
}
