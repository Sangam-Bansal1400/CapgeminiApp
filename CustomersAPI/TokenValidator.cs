
using System.Net.Http.Headers;
using CapgAppLibrary;

namespace CustomersAPI
{
    public class TokenValidator
    {
        private readonly ILogger<TokenValidator> _logger;
        public TokenValidator(ILogger<TokenValidator> logger)
        {
            _logger = logger;
        }
        public async Task<User> Validate(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:7002");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("/api/accounts");
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Validation successeded");
                    var user = await response.Content.ReadFromJsonAsync<User>();
                    return user;
                }
                else
                {
                    _logger.LogError("Validation faild");
                }

            }
            return null;
        }
    }
}
