using Nadin.Domain.Entities;
using SharedKernel.Common;

namespace Nadin.Application.Contracts.Persistence
{
    public interface IProductRepository
    {
        Task CreateProductAsync(Product product, CancellationToken cancellationToken);
        Task DeleteProductAsync(Product product, CancellationToken cancellationToken);
        Task<Product> GetProductByIdAsync(int productId, CancellationToken cancellationToken);
        Task<PaginatedResult<Product>> GetAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task UpdateProductAsync(Product product, CancellationToken cancellationToken);
        Task<bool> IsValidProductAsync(string email, DateTime date, CancellationToken cancellationToken);
    }
}
