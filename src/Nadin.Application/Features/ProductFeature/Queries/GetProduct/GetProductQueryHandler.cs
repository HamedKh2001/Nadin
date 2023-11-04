using AutoMapper;
using MediatR;
using Nadin.Application.Contracts.Persistence;

namespace Nadin.Application.Features.ProductFeature.Queries.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<ProductDto>(product);
        }
    }
}
