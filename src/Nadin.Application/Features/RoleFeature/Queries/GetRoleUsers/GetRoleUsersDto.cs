using Nadin.Application.Features.UserFeature.Queries.GetUser;
using System.Collections.Generic;

namespace Nadin.Application.Features.RoleFeature.Queries.GetRoleUsers
{
    public class GetRoleUsersDto
    {
        public long GroupId { get; set; }
        public string GroupCaption { get; set; }
        public List<UserDto> Users { get; set; }
    }
}
