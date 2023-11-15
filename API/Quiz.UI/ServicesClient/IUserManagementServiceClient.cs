using Quiz.DTO.BaseResponse;
using Quiz.DTO.UserManagement;
using Quiz.DTO.UserTestManagement;

namespace Quiz.UI.ServicesClient
{
    public interface IUserManagementServiceClient
    {
        Task<ApiResult<bool>> UserBuyingTest(UserBuyingTestRequest request);
        Task<ApiResult<List<string>>> GetListUserStructuresById(string userId);
        Task<ApiResult<List<GetUserTestResponse>>> GetListUserResultById(string userId);
    }
}
