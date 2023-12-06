using Microsoft.AspNetCore.Mvc.RazorPages;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.UserTestManagement;

namespace Quiz.UI.ServicesClient
{
    public interface IUserTestManagementServiceClient
    {
        Task<ApiResult<PagedResult<GetUserTestResponse>>> GetListUserTestManagement(GetListResultUserTestRequest request);
    }
}
