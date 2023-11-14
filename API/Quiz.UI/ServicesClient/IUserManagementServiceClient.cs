using Quiz.DTO.BaseResponse;
using Quiz.DTO.UserManagement;

namespace Quiz.UI.ServicesClient
{
    public interface IUserManagementServiceClient
    {
        Task<ApiResult<bool>> UserBuyingTest(UserBuyingTestRequest request);
        Task<ApiResult<List<string>>> GetListUserStructuresById(string userId);
    }
}
