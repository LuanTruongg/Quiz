using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Model
{
    public class UserStructure
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string TestStructureId { get; set; }
        public TestStructure TestStructure { get; set; }
    }
}
