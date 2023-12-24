using Mango.Common.Dtos;
using Mango.Services.AuthAPI.Models.Dtos;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController(IAuthService authService) : ControllerBase
    {
        readonly ResponseDto responseDto = new ();

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
        {
            var message = await authService.Register(registrationRequestDto);
            responseDto.IsSuccess = string.IsNullOrEmpty(message);
            responseDto.Message = message;
            return responseDto.IsSuccess ? Ok(responseDto) : BadRequest(responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var loginResponseDto = await authService.Login(loginRequestDto);
            responseDto.Result = loginResponseDto;
            if (string.IsNullOrEmpty(loginResponseDto.Token))
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "User not found or password do not match";
                return BadRequest(responseDto);
            }
            return Ok(responseDto);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole(string email, string roleName)
        {
            responseDto.IsSuccess = await authService.AssignRole(email, roleName);
            return Ok(responseDto);
        }

    }
}
