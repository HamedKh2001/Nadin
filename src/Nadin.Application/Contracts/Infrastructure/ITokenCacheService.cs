using System.Threading.Tasks;

namespace Nadin.Application.Contracts.Infrastructure
{
    public interface ITokenCacheService
    {
        Task AddOrUpdateAsync(long userId, string token);
        Task RemoveAsync(long userId);
    }
}
