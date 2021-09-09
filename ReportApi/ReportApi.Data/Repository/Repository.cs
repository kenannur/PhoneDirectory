using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportApi.Data.Context;

namespace ReportApi.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly AppDbContext Context;
        public Repository(AppDbContext context) => Context = context;

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ThrowIfNull(entity);

            await Context.AddAsync(entity, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ThrowIfNull(entity);

            Context.Remove(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity> GetAsync(object id, CancellationToken cancellationToken = default)
        {
            return await Context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Set<TEntity>().ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ThrowIfNull(entity);

            Context.Update(entity);
            await Context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        private static void ThrowIfNull(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException($"{typeof(TEntity).Name} must not be null");
            }
        }
    }
}
