using Nadin.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.Application.Contracts.Persistence
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
        Task<RefreshToken> GetLatestOneAsync(long userId, CancellationToken cancellationToken);
        Task UpdateAsync(RefreshToken lastRefreshToken, CancellationToken cancellationToken);
    }
}
