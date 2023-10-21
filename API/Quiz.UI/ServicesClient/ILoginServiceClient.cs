using NuGet.Protocol.Plugins;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.UserManagement;

namespace Quiz.UI.ServicesClient
{
    public interface ILoginServiceClient
    {
        Task<ApiResult<string>> Authenticate(AuthenticationRequest request);
    }
}
