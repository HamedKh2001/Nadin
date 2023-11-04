using Nadin.Domain.Entities;
using SharedKernel.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.Application.Contracts.Persistence
{
    public interface IUserLoginRepository
    {
        Task<UserLogin> CreateAsync(UserLogin userLogin, CancellationToken cancellationToken);
        Task<PaginatedResult<UserLogin>> GetAsync(int userId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
