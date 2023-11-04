using MediatR;

namespace Nadin.Application.Features.GroupFeature.Queries.GetGroupRoles
{
    public class GetGroupRolesQuery : IRequest<GroupRolesDto>
    {
        public int GroupId { get; set; }
    }
}
