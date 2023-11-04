using AutoMapper;
using MediatR;
using Nadin.Application.Contracts.Persistence;
using Nadin.Domain.Entities;
using SharedKernel.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.Application.Features.GroupFeature.Queries.GetGroup
{
    public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, GroupDto>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GetGroupQueryHandler(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<GroupDto> Handle(GetGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetAsync(request.Id, cancellationToken);
            if (group is null)
                throw new NotFoundException(nameof(Group), request.Id);

            return _mapper.Map<GroupDto>(group);
        }
    }
}
