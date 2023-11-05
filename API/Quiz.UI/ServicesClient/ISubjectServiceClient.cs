using Quiz.DTO.BaseResponse;
using Quiz.DTO.SubjectManagement;

namespace Quiz.UI.ServicesClient
{
    public interface ISubjectServiceClient
    {
        Task<GetListSubjectPagingResponse> GetListSubjectPaging(PagingRequest request);
    }
}
