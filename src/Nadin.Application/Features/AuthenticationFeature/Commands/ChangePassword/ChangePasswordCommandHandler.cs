using MediatR;
using Nadin.Application.Contracts.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.Application.Features.AuthenticationFeature.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IAuthenticationService _authenticationService;

        public ChangePasswordCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            await _authenticationService.ChangePasswordAsync(request, cancellationToken);
            return Unit.Value;
        }
    }
}
