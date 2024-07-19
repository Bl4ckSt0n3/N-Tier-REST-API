using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using forceget.Services.Abstract;
using forceget.DataAccess.Models.Dtos;
using forceget.DataAccess.Models.Entities;
using forceget.DataAccess.Models.DbContexts;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace forceget.Services.Services
{
    public class AuthService: IAuthService
    {
         private readonly UsersDbContext _userDbContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(UsersDbContext userDbContext, IConfiguration configuration, IMapper mapper)
        {
            _userDbContext = userDbContext;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<string>> Auth(string email, string password)
        {
            var response = new ServiceResponse<string>();

            try 
            {
                var user = await _userDbContext.Users.FirstOrDefaultAsync(usr => usr.Email.Equals(email));

                // check the token, if there is token in the db then there should be some active devices.
                
                if (user?.Token != null || user?.Token != "") 
                {
                    response.Message = "You are already logged in! You should log out from the other devices first.";
                    response.Data = "";
                    response.Success = false;
                }

                if (user != null && VerifyPassword(password, user.PasswordHash, user.PasswordSalt) && CheckPasswordLength(password))
                {
                    var token = CreateToken(user);
                    response.Data = token;
                    response.Message = "OK";
                    response.Success = true;

                    user.Token = token;
                    _userDbContext.Users.Update(user);
                    await _userDbContext.SaveChangesAsync();
                }
                else if (user != null && !VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                {
                    response.Data = "";
                    response.Message = "Wrong Password!";
                    response.Success = false;
                }
                else 
                {
                    response.Data = "no token";
                    response.Message = "failed!";
                    response.Success = false;
                }

            }
            catch(Exception e)
            {
                response.Data = null;
                response.Message = e.Message;
                response.Success = false;
            }

            return response;

        }

        public async Task<ServiceResponse<string>> Logout(string email)
        {
            var response = new ServiceResponse<string>();

            try 
            {
                User user = await _userDbContext.Users.FirstAsync(x => x.Email.Equals(email));
                user.Token = string.Empty;
                
                _userDbContext.Users.Update(user);
                await _userDbContext.SaveChangesAsync();

                response.Data = "Logged out";
                response.Success = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Data = ex.Message;
                response.Success = false;
                response.Message = "Failed";
            }

            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> Create(CreateUserDto model)
        {
            var response = new ServiceResponse<GetUserDto>();

            if (await EmailAlreadyExists(model.Email))
            {
                response.Data =  _mapper.Map<GetUserDto>(model);
                response.Success = false;
                response.Message = "Email already used!";
            }
            else 
            {
                try 
                {
                    var user = _mapper.Map<User>(model);

                    CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

                    _userDbContext.Users.Add(user);
                    await _userDbContext.SaveChangesAsync();

                    response.Data = _mapper.Map<GetUserDto>(user);
                    response.Message = "Success";
                    response.Success = true;
                }
                catch (Exception ex) 
                {
                    response.Data = null;
                    response.Message = "Error";
                    response.Success = false;
                }
            }

            return response;
        }

        // create jwt
        private string CreateToken(User user) 
        {
            // create dynamic claim type list
            var claims = new List<Claim> 
            {
                //new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim("Name", user.Name),
                new Claim(ClaimTypes.Surname, user.SecondName),
                new Claim("Email", user.Email)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Token:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1), //AddMinutes(30)
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }

        private static bool CheckPasswordLength(string password)
        {
            return password.Length <= 16 ; // is >= 8 and <= 16
        }

        private static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            if (CheckPasswordLength(password))
            {
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            else 
            {
                return false;
            }
    
        }

        
        public async Task<bool> EmailAlreadyExists(string email)
        {
            var alreadExists = await _userDbContext.Users.AnyAsync(u => u.Email == email);
            if (alreadExists)
            {
                return true;
            }
            return false;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            passwordSalt = hmac.Key;
        }

    }
}