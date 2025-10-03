using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using KairosWebAPI.Models.Dto;
using KairosWebAPI.Services.KairosService;

namespace KairosWebAPI.AuthorizationFilter
{
    public class KAuthorizationFilter : IAuthorizationFilter
    {
        public KAuthorizationFilter(IKairosService kairosService)
        {
            KairosService = kairosService;
        }

        public IKairosService KairosService { get; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Retrieve the token from the request headers or query parameters
            string token = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            // Validate the token and check for specific claims
            token = token.Replace("Bearer ", "");
            if (!IsValidToken(token).Result)
            {

                context.Result = new UnauthorizedResult(); // Return 401 Unauthorized
            }
        }
        private async Task<bool> IsValidToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Retrieve the userId and email claims from the token
            string? userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
            string? password = jwtToken.Claims.FirstOrDefault(c => c.Type == "Password")?.Value;

           if(!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(password)) {
                return await KairosService.ValidateUser(new LoginDto()
                {
                    UserId = userId,
                    Password = password
                });
            }
           return false;
        }
    }
}
