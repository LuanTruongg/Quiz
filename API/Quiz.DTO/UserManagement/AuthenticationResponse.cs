using Quiz.Infrastructure.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.UserManagement
{
    public class AuthenticationResponse : Response
    {
        public bool IsAuthSuccessful { get; set; }

        public bool IsActive { get; set; }

        public bool IsFirstLogin { get; set; }

        public string ErrorMessage { get; set; }

        public string Token { get; set; }

        public bool Is2StepVerificationRequired { get; set; }

        public string Provider { get; set; }
    }
}
