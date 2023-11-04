using Newtonsoft.Json;

namespace Nadin.Application.Features.AuthenticationFeature.Queries.Authenticate
{
    public class AuthenticateDto
    {
        [JsonIgnore]
        public string Token { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
