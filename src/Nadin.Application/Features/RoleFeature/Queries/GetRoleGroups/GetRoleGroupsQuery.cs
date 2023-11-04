using MediatR;
using System.Collections.Generic;

namespace Nadin.Application.Features.RoleFeature.Queries.GetRoleGroups
{
    public class GetRoleGroupsQuery : IRequest<List<GetRoleGroupsDto>>
    {
        public int RoleId { get; set; }
    }
}
