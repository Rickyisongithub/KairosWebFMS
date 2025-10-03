using KairosWebAPI.Models.Dto;
using KairosWebAPI.Models.ResponseResults;
using RestSharp;

namespace KairosWebAPI.Services.KairosService
{
    public class KairosService : IKairosService
    {
        private readonly RestClient _restClient;
        private readonly string _apiKey;
        private readonly string _authorizationKey;

        public KairosService(IConfiguration  config) {
            var configuration = config;
            _restClient = new RestClient(configuration["kairos:API_Url"] ?? string.Empty);
            _apiKey = configuration["kairos:API_KEY"]?? string.Empty;
            _authorizationKey = configuration["kairos:AUTHORIZATION_KEY"] ?? string.Empty;

        }
        public async Task<bool> ValidateUser(LoginDto loginDto) 
        {
            if (string.IsNullOrEmpty(_apiKey) || string.IsNullOrEmpty(_authorizationKey)) return false;
            
            var request = new RestRequest("Ice.BO.UserFileSvc/ValidatePassword")
            {
                Timeout = -1
            };
            request.AddHeader("Authorization", "Basic "+_authorizationKey);
            request.AddHeader("X-API-Key", _apiKey);
            request.AddHeader("Content-Type", "application/json");
            var data = new
            {
                userID = loginDto.UserId,
                password = loginDto.Password
            };
            request.AddBody(data);

            var response = await _restClient.ExecutePostAsync<KValidateUserResponse>(request);
            return response is { IsSuccessful: true, Data.ReturnObj: true };
        }
    }
}
