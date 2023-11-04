using FluentValidation;
using Nadin.Application.Contracts.Infrastructure;
using Nadin.Application.Contracts.Persistence;
using System.Text.RegularExpressions;

namespace Nadin.Application.Features.ProductFeature.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserContextService _userContextService;

        public UpdateProductCommandValidator(IProductRepository productRepository, IUserContextService userContextService)
        {
            _productRepository = productRepository;
            _userContextService = userContextService;

            RuleFor(p => p.Email)
              .NotNull().NotEmpty().WithMessage("{Email} is required.")
              .MaximumLength(50).WithMessage("{Email} must not exceed 50 characters.")
              .Must(BeValidEmail).WithMessage("The Email must be Email format.");

            RuleFor(p => p)
                .MustAsync(BeValidProduct).WithMessage("Email OR ProductDate Exists");


            RuleFor(p => p.Id)
                .NotNull().NotEmpty().GreaterThan(0).WithMessage("{Id} is required.");

            RuleFor(p => p).MustAsync(HasAccess).WithMessage("User Has Not Permission For This Item");
        }

        private bool BeValidEmail(string email)
        {
            return Regex.IsMatch(email, "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$\r\n");
        }

        private async Task<bool> BeValidProduct(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            return !await _productRepository.IsValidProductAsync(command.Email, command.Date, cancellationToken);
        }

        private async Task<bool> HasAccess(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(command.Id, cancellationToken);
            var userId = _userContextService.CurrentUser.Id;
            return product.UserId == userId ? true : false;
        }
    }
}
