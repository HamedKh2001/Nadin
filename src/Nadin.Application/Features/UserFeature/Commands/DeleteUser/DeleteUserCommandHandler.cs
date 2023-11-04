using MediatR;
using Nadin.Application.Contracts.Persistence;
using SharedKernel.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.Application.Features.UserFeature.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Id, cancellationToken);
            if (user is null)
                throw new NotFoundException(nameof(user), request.Id);

            await _userRepository.DeleteAsync(user, cancellationToken);

            return Unit.Value;
        }
    }
}
