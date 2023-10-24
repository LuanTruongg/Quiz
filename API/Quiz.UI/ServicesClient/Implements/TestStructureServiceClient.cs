using Quiz.DTO.Common;
using Quiz.UI.Controllers;

namespace Quiz.UI.ServicesClient.Implements
{
    public class TestStructureServiceClient : ITestStructureServiceClient
    {
        private readonly IHomeServiceClient _homeServiceClient;

        public TestStructureServiceClient(IHomeServiceClient homeServiceClient)
        {
            _homeServiceClient = homeServiceClient;
        }
        public async Task<string> GetNameDepartment(string id)
        {
            var listDepartment = await _homeServiceClient.GetListDepartments();
            var nameDepartmentCurrent = listDepartment.Where(x => x.DepartmentId == id).FirstOrDefault().Name.ToString();
            return nameDepartmentCurrent;
        }

        public Task<List<GetListMajorResponse>> ListMajors(GetListMajorRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
