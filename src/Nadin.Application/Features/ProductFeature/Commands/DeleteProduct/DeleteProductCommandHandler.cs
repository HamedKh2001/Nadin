using MediatR;
using Nadin.Application.Contracts.Persistence;

namespace Nadin.Application.Features.ProductFeature.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id, cancellationToken);
            await _productRepository.DeleteProductAsync(product, cancellationToken);
            return Unit.Value;
        }
    }
}
