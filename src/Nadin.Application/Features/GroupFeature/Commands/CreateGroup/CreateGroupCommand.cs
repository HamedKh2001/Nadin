using MediatR;
using Nadin.Application.Features.GroupFeature.Queries.GetGroup;

namespace Nadin.Application.Features.GroupFeature.Commands.CreateGroup
{
    public class CreateGroupCommand : IRequest<GroupDto>
    {
        public string Caption { get; set; }
        public bool IsPermissionBase { get; set; }
    }
}
