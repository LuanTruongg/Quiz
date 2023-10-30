using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.UserTestManagement
{
    public class AddUserTestResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string UserTestId { get; set; }
    }
}
