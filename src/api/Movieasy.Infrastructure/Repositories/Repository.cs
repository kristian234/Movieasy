﻿using Microsoft.EntityFrameworkCore;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Infrastructure.Repositories
{
    internal abstract class Repository<T>
        where T : Entity
    {
        protected readonly ApplicationDbContext DbContext;

        protected Repository(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public async Task<T?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await DbContext
                .Set<T>()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public virtual async Task AddAsync(T entity)
        {
            await DbContext.AddAsync(entity);
        }

    }
}
