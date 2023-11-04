using AutoMapper;
using Nadin.Application.Contracts.Infrastructure;
using Nadin.Application.Features.ProductFeature.Commands.CreateProduct;
using Nadin.Domain.Entities;

namespace Nadin.Application.Mapping.Resolver.ProductResolver
{
    public class CreateProductCommandResolver : IValueResolver<CreateProductCommand, Product, int>
    {
        private readonly IUserContextService _userContextService;

        public CreateProductCommandResolver(IUserContextService userContextService)
        {
            _userContextService = userContextService;
        }

        public int Resolve(CreateProductCommand source, Product destination, int destMember, ResolutionContext context)
        {
            return _userContextService.CurrentUser.Id;
        }
    }
}
