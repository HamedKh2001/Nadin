using MediatR;
using System.Collections.Generic;

namespace Nadin.Application.Features.GroupFeature.Commands.UpdateGroupRole
{
    public class UpdateGroupRoleCommand : IRequest
    {
        public int GroupId { get; set; }
        public List<int> RoleIds { get; set; }
    }
}
