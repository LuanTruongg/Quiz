using NuGet.Protocol.Plugins;
using Quiz.DTO.BaseResponse;
using Quiz.DTO.UserManagement;

namespace Quiz.UI.ServicesClient
{
    public interface ILoginServiceClient
    {
        Task<AuthenticationResponse> Authenticate(AuthenticationRequest request);
        Task<string> GetListRoleFromToken(string token);
    }
}
