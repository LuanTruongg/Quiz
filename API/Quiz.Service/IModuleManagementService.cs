using Quiz.DTO.ModuleManagement;

namespace Quiz.Service
{
	public interface IModuleManagementService
	{
		Task<IEnumerable<GetModuleResponse>> GetListModuleAsync(GetListModuleRequest request);
		Task<GetModuleResponse> GetModuleByIdAsync(string moduleId);
		Task<IEnumerable<CreateModuleResponse>> CreateListModuleAsync(CreateModuleRequest request);
		Task<GetModuleResponse> EditModuleAsync(string id, EditModuleRequest request);
	}
}
