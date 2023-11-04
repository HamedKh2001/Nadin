using MediatR;

namespace Nadin.Application.Features.ProductFeature.Queries.GetProduct
{
    public class GetProductQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
}
