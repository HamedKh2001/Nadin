using MediatR;
using Nadin.Application.Features.GroupFeature.Queries.GetGroup;
using SharedKernel.Common;

namespace Nadin.Application.Features.GroupFeature.Queries.GetGroups
{
    public class GetGroupsQuery : PaginationQuery, IRequest<PaginatedList<GroupDto>>
    {
        public string Caption { get; set; }
    }
}
