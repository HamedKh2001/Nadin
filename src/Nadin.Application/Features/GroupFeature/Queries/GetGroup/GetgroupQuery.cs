using MediatR;

namespace Nadin.Application.Features.GroupFeature.Queries.GetGroup
{
    public class GetGroupQuery : IRequest<GroupDto>
    {
        public int Id { get; set; }
    }
}
