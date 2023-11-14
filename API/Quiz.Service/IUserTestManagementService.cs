using Quiz.DTO.BaseResponse;
using Quiz.DTO.UserTestManagement;
using Quiz.Repository.Model;

namespace Quiz.Service
{
    public interface IUserTestManagementService
    {
        Task<AddUserTestResponse> AddUserTestAsync(AddUserTestRequest request);
        Task<GetResultUserTestResponse> GetResultUserTestAsync(GetResultUserTestRequest request);
        Task<ApiResult<List<GetUserTestResponse>>> GetListResultUserTestAsync(string userId);
    }
}
