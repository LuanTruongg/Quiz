using Quiz.DTO.BaseResponse;
using Quiz.DTO.UserManagement;
using Quiz.Repository.Model;

namespace Quiz.Service
{
    public interface IUserManagementService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<ApiResult<GetProfileResponse>> GetMyProfileAsync(string userId);
        Task<ApiResult<bool>> UserBuyingTestAsync(UserBuyingTestRequest request);
        Task<ApiResult<List<string>>> GetUserStructuresById(string userId);
    }
}
