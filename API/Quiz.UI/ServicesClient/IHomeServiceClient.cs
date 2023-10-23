using Quiz.DTO.BaseResponse;
using Quiz.DTO.Common;

namespace Quiz.UI.ServicesClient
{
    public interface IHomeServiceClient
    {
        Task<List<GetListDepartmentResponse>> GetListDepartments();
    }
}
