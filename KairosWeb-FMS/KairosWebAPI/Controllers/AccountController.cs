using KairosWebAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using KairosWebAPI.Models.Dto;
using KairosWebAPI.Services.KairosService;
using KairosWebAPI.Services.TokenService;

namespace KairosWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IKairosService _kairosService;

        public AccountController(ITokenService tokenService,IKairosService kairosService)
        {
            this._tokenService = tokenService;
            this._kairosService = kairosService;
        }
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDto.UserId) || string.IsNullOrEmpty(loginDto.Password))
                    return BadRequest("Username and/or Password not specified");
                if (await _kairosService.ValidateUser(loginDto))
                {
                    var token = await _tokenService.GenerateToken(new AppUser()
                    {
                        UserId = loginDto.UserId,
                        Password = loginDto.Password
                    });
                    return Ok(ServiceResponse<string>.ReturnResultWith200(token));
                }
            }
            catch
            {
                return BadRequest("An error occurred in generating the token");
            }
            return Unauthorized();
        }

    }
}

