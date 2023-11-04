using MediatR;

namespace Nadin.Application.Features.ProductFeature.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public int Id { get; set; }
    }
}
