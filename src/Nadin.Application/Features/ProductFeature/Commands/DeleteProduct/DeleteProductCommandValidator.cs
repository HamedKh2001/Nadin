using FluentValidation;
using Nadin.Application.Contracts.Infrastructure;
using Nadin.Application.Contracts.Persistence;

namespace Nadin.Application.Features.ProductFeature.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserContextService _userContextService;

        public DeleteProductCommandValidator(IProductRepository productRepository, IUserContextService userContextService)
        {
            _productRepository = productRepository;
            _userContextService = userContextService;

            RuleFor(p => p.Id)
                .NotNull().NotEmpty().GreaterThan(0).WithMessage("{Id} is required.");

            RuleFor(p => p).MustAsync(HasAccess).WithMessage("User Has Not Permission For This Item");
        }

        private async Task<bool> HasAccess(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(command.Id, cancellationToken);
            var userId = _userContextService.CurrentUser.Id;
            return product.UserId == userId ? true : false;
        }
    }
}
