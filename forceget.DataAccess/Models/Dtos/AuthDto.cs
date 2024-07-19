using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace forceget.DataAccess.Models.Dtos
{
    public class AuthDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}