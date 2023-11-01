using Quiz.DTO.BaseResponse;
using Quiz.DTO.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Service
{
    public interface IUserManagementService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<ApiResult<GetProfileResponse>> GetMyProfileAsync(string userId);
    }
}
