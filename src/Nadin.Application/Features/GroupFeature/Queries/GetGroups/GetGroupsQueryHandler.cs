using AutoMapper;
using MediatR;
using Nadin.Application.Contracts.Persistence;
using Nadin.Application.Features.GroupFeature.Queries.GetGroup;
using SharedKernel.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.Application.Features.GroupFeature.Queries.GetGroups
{
    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, PaginatedList<GroupDto>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GetGroupsQueryHandler(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<GroupDto>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups = await _groupRepository.GetAsync(request.Caption, request.PageNumber, request.PageSize, cancellationToken);

            return _mapper.Map<PaginatedList<GroupDto>>(groups);
        }
    }
}
