using MediatR;
using Nadin.Application.Features.AuthenticationFeature.Queries.Authenticate;
using Newtonsoft.Json;

namespace Nadin.Application.Features.AuthenticationFeature.Queries.RefreshToken
{
    public class RefreshTokenQuery : IRequest<AuthenticateDto>
    {
        [JsonIgnore]
        public string AccessToken { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
