using Quiz.DTO.UserAnswerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Service
{
    public interface IUserAnswerManagementService
    {
        Task<AddUserAnswerResponse> AddUserAnswerAsync(List<AddUserAnswerRequest> requests);
    }
}
