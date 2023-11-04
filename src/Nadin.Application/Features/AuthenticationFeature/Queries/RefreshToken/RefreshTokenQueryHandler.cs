using MediatR;
using Nadin.Application.Contracts.Infrastructure;
using Nadin.Application.Features.AuthenticationFeature.Queries.Authenticate;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.Application.Features.AuthenticationFeature.Queries.RefreshToken
{
    public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, AuthenticateDto>
    {
        private readonly IAuthenticationService _authenticationService;

        public RefreshTokenQueryHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<AuthenticateDto> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            return await _authenticationService.RefreshTokenAsync(request, cancellationToken);
        }
    }
}
