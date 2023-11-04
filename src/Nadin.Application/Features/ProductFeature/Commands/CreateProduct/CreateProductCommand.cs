using MediatR;

namespace Nadin.Application.Features.ProductFeature.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }
}
