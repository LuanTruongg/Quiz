using Quiz.DTO.SubjectManagement;
using Quiz.DTO.BaseResponse;
namespace Quiz.Service
{
	public interface ISubjectManagementService
	{
		Task<IEnumerable<GetSubjectResponse>> GetListSubjectsAsync();
		Task<AddSubjectResponse> AddSubjectsAsync(AddSubjectRequest request);
		Task<GetListSubjectPagingResponse> GetListSubjectsPagingAsync(PagingRequest request);
	}
}
