﻿using Nadin.Application.Features.RoleFeature.Queries.GetRoles;
using System.Collections.Generic;

namespace Nadin.Application.Features.GroupFeature.Queries.GetGroup
{
    public class GroupDto
    {
        public long Id { get; set; }
        public string Caption { get; set; }
        public bool IsPermissionBase { get; set; }

        public List<RoleDto> Roles { get; set; }
    }
}
