using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace forceget.DataAccess.Models.Dtos
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}