using Quiz.DTO.UserTestManagement;

namespace Quiz.Service
{
    public interface IUserTestManagementService
    {
        Task<AddUserTestResponse> AddUserTestAsync(AddUserTestRequest request);
        Task<GetResultUserTestResponse> GetResultUserTestAsync(GetResultUserTestRequest request);
    }
}
