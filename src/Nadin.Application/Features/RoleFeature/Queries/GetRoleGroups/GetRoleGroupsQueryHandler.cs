using AutoMapper;
using MediatR;
using Nadin.Application.Contracts.Persistence;
using Nadin.Domain.Entities;
using SharedKernel.Exceptions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.Application.Features.RoleFeature.Queries.GetRoleGroups
{
    public class GetRoleGroupsQueryHandler : IRequestHandler<GetRoleGroupsQuery, List<GetRoleGroupsDto>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetRoleGroupsQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<GetRoleGroupsDto>> Handle(GetRoleGroupsQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetWithGroupsAsync(request.RoleId, cancellationToken);
            if (role is null)
                throw new NotFoundException(nameof(Role), request.RoleId);

            return _mapper.Map<List<GetRoleGroupsDto>>(role.Groups);
        }
    }
}
