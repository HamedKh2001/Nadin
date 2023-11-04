using MediatR;
using System.Collections.Generic;

namespace Nadin.Application.Features.AuthenticationFeature.Commands.UpdateUserGroup
{
    public class UpdateUserGroupCommand : IRequest
    {
        public int UserId { get; set; }
        public List<int> GroupIds { get; set; }
    }
}
