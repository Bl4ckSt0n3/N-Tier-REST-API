using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using forceget.DataAccess.Models.Dtos;
using forceget.Services.Abstract;


namespace forceget.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Authentication([FromBody] AuthDto request)
        {
            var response = await _authService.Auth(request.Email, request.Password);
            return response != null ? Ok(response) : BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("Logout")]
        public async Task<ActionResult<ServiceResponse<string>>> Logout([FromBody] LogoutDto request)
        {
            var response = await _authService.Logout(request.Email);
            return response != null ? Ok(response) : BadRequest();
        }
        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<ActionResult<ServiceResponse<string>>> SignUp([FromBody] CreateUserDto request)
        {
            var response = await _authService.Create(request);
            return response != null ? Ok(response) : BadRequest();
        }
    }
}