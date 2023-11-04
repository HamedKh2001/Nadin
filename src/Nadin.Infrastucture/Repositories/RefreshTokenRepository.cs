using Microsoft.EntityFrameworkCore;
using Nadin.Application.Contracts.Persistence;
using Nadin.Domain.Entities;
using Nadin.Infrastucture.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.Infrastucture.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly NadinDbContext _context;

        public RefreshTokenRepository(NadinDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return refreshToken;
        }

        public async Task<RefreshToken> GetLatestOneAsync(long userId, CancellationToken cancellationToken)
        {
            return await _context.RefreshTokens.OrderByDescending(o => o.Id).FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);
        }

        public async Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            _context.SetModified(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
