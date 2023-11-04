using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Nadin.Application.Contracts.Persistence;
using Nadin.Domain.Entities;
using SharedKernel.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.Application.Features.UserFeature.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository, ILogger<UpdateUserCommandHandler> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _userRepository.GetAsync(request.Id, cancellationToken);
            if (userToUpdate == null)
                throw new NotFoundException(nameof(User), request.Id);

            userToUpdate = _mapper.Map<User>(request);

            await _userRepository.UpdateAsync(userToUpdate, cancellationToken);

            return Unit.Value;
        }
    }
}
