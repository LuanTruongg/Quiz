using Quiz.DTO.SubjectManagement;
using Quiz.DTO.BaseResponse;
namespace Quiz.Service
{
	public interface ISubjectManagementService
	{
		Task<IEnumerable<GetSubjectResponse>> GetListSubjectsAsync();
		Task<ApiResult<bool>> AddSubjectsAsync(AddSubjectRequest request);
		Task<ApiResult<PagedResult<SubjectItem>>> GetListSubjectsPagingAsync(GetListSubjectPagingRequest request);
        Task<ApiResult<SubjectItem>> GetSubjectByIdAsync(string id);
        Task<ApiResult<bool>> AddTeachersForSubjectAsync(AddTeacherForSubjectRequest request);
        Task<ApiResult<bool>> DeleteSubjectAsync(string id);
        Task<ApiResult<bool>> DeleteTeachersForSubjectAsync(DeleteTeacherForSubjectRequest request);
    }
}
