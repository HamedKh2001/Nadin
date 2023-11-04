using AutoMapper;
using MediatR;
using Nadin.Application.Contracts.Persistence;
using Nadin.Domain.Entities;

namespace Nadin.Application.Features.ProductFeature.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id, cancellationToken);
            var productToUpdate = _mapper.Map<Product>(product);
            await _productRepository.UpdateProductAsync(productToUpdate, cancellationToken);
            return Unit.Value;
        }
    }
}
