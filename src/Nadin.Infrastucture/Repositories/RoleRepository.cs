﻿using Microsoft.EntityFrameworkCore;
using Nadin.Application.Contracts.Persistence;
using Nadin.Domain.Entities;
using Nadin.Infrastucture.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.Infrastucture.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly NadinDbContext _context;

        public RoleRepository(NadinDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAsync(CancellationToken cancellationToken)
        {
            return await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Role> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Roles.FindAsync(id, cancellationToken);
        }

        public async Task<List<Role>> GetByRoleIdsAsync(List<int> ids, CancellationToken cancellationToken)
        {
            return await _context.Roles.AsNoTracking().Where(r => ids.Contains(r.Id)).Distinct().ToListAsync(cancellationToken);
        }

        public async Task<Role> GetWithGroupsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Roles.AsNoTracking().Include(r => r.Groups).FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<Role> GetWithUsersAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Roles.AsNoTracking().Include(r => r.Groups).ThenInclude(g => g.Users).FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<bool> IsUniqueDisplayTitleAsync(int id, string displayTitle, CancellationToken cancellationToken)
        {
            return await _context.Roles.AnyAsync(r => r.DisplayTitle == displayTitle && r.Id != id, cancellationToken) == false;
        }

        public async Task UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            _context.SetModified(role);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
