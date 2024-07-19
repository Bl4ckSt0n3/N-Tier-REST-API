using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using forceget.DataAccess.Models.Dtos;

namespace forceget.Services.Abstract
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Auth(string email, string password);
        Task<ServiceResponse<string>> Logout(string email);
        Task<ServiceResponse<GetUserDto>> Create(CreateUserDto model);
        Task<bool> EmailAlreadyExists(string email);
    }
}