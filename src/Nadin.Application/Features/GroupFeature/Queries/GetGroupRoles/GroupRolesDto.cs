using Nadin.Application.Features.RoleFeature.Queries.GetRoles;
using System.Collections.Generic;

namespace Nadin.Application.Features.GroupFeature.Queries.GetGroupRoles
{
    public class GroupRolesDto
    {
        public long Id { get; set; }
        public string Caption { get; set; }
        public List<RoleDto> Roles { get; set; }
    }
}
