using Microsoft.EntityFrameworkCore;
using Nadin.Application.Contracts.Persistence;
using Nadin.Domain.Entities;
using Nadin.Infrastucture.Persistence;
using SharedKernel.Common;
using SharedKernel.Extensions;

namespace Nadin.Infrastucture.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly NadinDbContext _context;

        public ProductRepository(NadinDbContext context)
        {
            _context = context;
        }

        public async Task CreateProductAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteProductAsync(Product product, CancellationToken cancellationToken)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Product> GetProductByIdAsync(int productId, CancellationToken cancellationToken)
        {
            return await _context.Products.FindAsync(productId, cancellationToken);
        }

        public async Task<PaginatedResult<Product>> GetAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Products.AsNoTracking();

            return await query.ToPagedListAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task IsValidProduct(Product product, CancellationToken cancellationToken)
        {
            _context.SetModified(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> IsValidProductAsync(string email, DateTime date, CancellationToken cancellationToken)
        {
            return await _context.Products.AnyAsync(p => p.Email == email && p.Date == date, cancellationToken);
        }

        public async Task UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            _context.SetModified(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
