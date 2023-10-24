using Quiz.DTO.Common;

namespace Quiz.UI.ServicesClient
{
    public interface ITestStructureServiceClient
    {
        Task<List<GetListMajorResponse>> ListMajors(GetListMajorRequest request);
        Task<string> GetNameDepartment(string id);
    }
}
