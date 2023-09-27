using Quiz.DTO.ModuleManagement;

namespace Quiz.Service
{
	public interface IModuleManagementService
	{
		Task<IEnumerable<GetModuleResponse>> GetListModuleAsync(GetModuleRequest request);
		Task<IEnumerable<CreateModuleResponse>> CreateListModuleAsync(CreateModuleRequest request);
	}
}
