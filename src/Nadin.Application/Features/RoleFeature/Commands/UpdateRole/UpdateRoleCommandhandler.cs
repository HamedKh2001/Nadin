using AutoMapper;
using MediatR;
using Nadin.Application.Contracts.Persistence;
using Nadin.Domain.Entities;
using SharedKernel.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.Application.Features.RoleFeature.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var roleToUpdate = await _roleRepository.GetAsync(request.Id, cancellationToken);
            if (roleToUpdate is null)
                throw new NotFoundException(nameof(Role), request.Id);

            roleToUpdate = _mapper.Map<Role>(request);
            await _roleRepository.UpdateAsync(roleToUpdate, cancellationToken);


            return Unit.Value;
        }
    }
}
