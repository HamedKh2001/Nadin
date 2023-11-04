using MediatR;
using Nadin.Application.Contracts.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.Application.Features.AuthenticationFeature.Commands.UpdateUserGroup
{
    public class UpdateUserGroupCommandHandler : IRequestHandler<UpdateUserGroupCommand>
    {
        private readonly IAuthenticationService _authenticationService;

        public UpdateUserGroupCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Unit> Handle(UpdateUserGroupCommand request, CancellationToken cancellationToken)
        {
            await _authenticationService.UpdateUserGroupAsync(request, cancellationToken);
            return Unit.Value;
        }
    }
}
