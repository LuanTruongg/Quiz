using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.UserManagement;
using Quiz.Repository.Model;

namespace Quiz.Service
{
    public interface IUserManagementService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<ApiResult<bool>> RegisterAsync(RegisterRequest request);
        Task<ApiResult<GetProfileResponse>> GetMyProfileAsync(string userId);
        Task<ApiResult<bool>> UserBuyingTestAsync(UserBuyingTestRequest request);
        Task<ApiResult<List<string>>> GetUserStructuresById(string userId);
        Task<ApiResult<PagedResult<UserItem>>> GetListUserAsync(PagingRequest request);
        Task<ApiResult<UserItem>> GetUserByIdAsync(string userId);
        Task<ApiResult<bool>> EditUserAsync(EditUserRequest request, string userId);
        Task<ApiResult<IList<string>>> GetRolesUserAsync(string userId);
        Task<ApiResult<bool>> AssignRolesAsync(string userId, RoleAssignRequest request);
        Task<ApiResult<PagedResult<UserStructureItem>>> GetListUserBoughtTestAsync(GetListUserStructureRequest request);
        Task<ApiResult<bool>> UpdateMoneyAsync(AddMoneyRequest request);
    }
}
