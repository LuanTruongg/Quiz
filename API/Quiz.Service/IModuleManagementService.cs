using Quiz.DTO.BaseResponse;
using Quiz.DTO.ModuleManagement;

namespace Quiz.Service
{
	public interface IModuleManagementService
	{
		Task<ApiResult<List<GetListModuleResponse>>> GetListModuleAsync(string subjectId);
        Task<ApiResult<List<int>>> GetListTotalQuestionOfModuleAsync(string subjectId);
        Task<GetListModuleResponse> GetModuleByIdAsync(string moduleId);
        Task<ApiResult<bool>> CreateListModuleAsync(CreateModuleRequest request);
		Task<GetListModuleResponse> EditModuleAsync(string id, EditModuleRequest request);
	}
}
