using Quiz.DTO.BaseResponse;
using Quiz.DTO.Common;

namespace Quiz.UI.ServicesClient
{
    public interface IHomeServiceClient
    {
        Task<List<GetListDepartmentResponse>> GetListDepartments();
        Task<List<GetListMajorResponse>> GetListMajor(string departmentId);
        Task<List<GetListMajorResponse>> GetListAllMajor();
    }
}
