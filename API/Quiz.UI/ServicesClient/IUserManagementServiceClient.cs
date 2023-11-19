using Microsoft.AspNetCore.Mvc.RazorPages;
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
        Task<ApiResult<PagedResult<UserItem>>> GetListUser(PagingRequest request);
        Task<ApiResult<UserItem>> GetUserById(string userId);
        Task<ApiResult<bool>> EditUser(EditUserRequest request);
        Task<ApiResult<IList<string>>> GetUserRoles(string userId);
        Task<List<RoleItem>> GetRoles();
        Task<ApiResult<bool>> AssignRole(AssignRolesRequest request);
    }
}
