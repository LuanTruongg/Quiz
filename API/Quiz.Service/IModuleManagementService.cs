using Quiz.DTO.ModuleManagement;

namespace Quiz.Service
{
	public interface IModuleManagementService
	{
		Task<IEnumerable<GetListModuleResponse>> GetListModuleAsync(GetListModuleRequest request);
		Task<GetListModuleResponse> GetModuleByIdAsync(string moduleId);
		Task<IEnumerable<CreateModuleResponse>> CreateListModuleAsync(CreateModuleRequest request);
		Task<GetListModuleResponse> EditModuleAsync(string id, EditModuleRequest request);
	}
}
