using Microsoft.EntityFrameworkCore;
using Nadin.Application.Contracts.Persistence;
using Nadin.Domain.Entities;
using Nadin.Infrastucture.Persistence;
using SharedKernel.Common;
using SharedKernel.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.Infrastucture.Repositories
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly NadinDbContext _context;

        public UserLoginRepository(NadinDbContext context)
        {
            _context = context;
        }

        public async Task<UserLogin> CreateAsync(UserLogin userLogin, CancellationToken cancellationToken)
        {
            await _context.UserLogins.AddAsync(userLogin, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return userLogin;
        }

        public async Task<PaginatedResult<UserLogin>> GetAsync(int userId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.UserLogins.AsNoTracking().Include(u => u.User).Where(u => u.UserId == userId);
            query = query.OrderBy(q => q.Id);
            return await query.ToPagedListAsync(pageNumber, pageSize, cancellationToken);
        }
    }
}
