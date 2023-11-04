using MediatR;
using Nadin.Application.Features.ProductFeature.Queries.GetProduct;
using SharedKernel.Common;

namespace Nadin.Application.Features.ProductFeature.Queries.GetProducts
{
    public class GetProductsQuery : PaginationQuery, IRequest<PaginatedList<ProductDto>>
    {
    }
}
