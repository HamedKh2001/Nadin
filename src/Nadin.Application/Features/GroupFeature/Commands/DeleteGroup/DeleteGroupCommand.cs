using MediatR;

namespace Nadin.Application.Features.GroupFeature.Commands.DeleteGroup
{
    public class DeleteGroupCommand : IRequest
    {
        public int Id { get; set; }
    }
}
