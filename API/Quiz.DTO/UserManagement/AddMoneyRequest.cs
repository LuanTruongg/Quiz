﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.UserManagement
{
    public class AddMoneyRequest
    {
        public string UserId { get; set; }
        public long Money { get; set; }
    }
}
