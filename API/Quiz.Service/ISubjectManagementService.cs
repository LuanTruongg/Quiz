using Quiz.DTO.SubjectManagement;

namespace Quiz.Service
{
	public interface ISubjectManagementService
	{
		Task<IEnumerable<GetSubjectResponse>> GetListSubjectsAsync();
		Task<AddSubjectResponse> AddSubjectsAsync(AddSubjectRequest request);
	}
}
