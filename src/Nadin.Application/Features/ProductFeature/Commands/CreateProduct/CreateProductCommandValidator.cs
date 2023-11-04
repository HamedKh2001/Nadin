using FluentValidation;
using Nadin.Application.Contracts.Persistence;
using System.Text.RegularExpressions;

namespace Nadin.Application.Features.ProductFeature.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(p => p.Email)
              .NotNull().NotEmpty().WithMessage("{Email} is required.")
              .MaximumLength(50).WithMessage("{Email} must not exceed 50 characters.")
              .Must(BeValidEmail).WithMessage("The Email must be Email format.");

            RuleFor(p => p)
                .MustAsync(BeValidProduct).WithMessage("Email OR ProductDate Exists");
        }

        private bool BeValidEmail(string email)
        {
            return Regex.IsMatch(email, "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
        }

        private async Task<bool> BeValidProduct(CreateProductCommand command, CancellationToken cancellationToken)
        {
            return !await _productRepository.IsValidProductAsync(command.Email, command.Date, cancellationToken);
        }
    }
}
