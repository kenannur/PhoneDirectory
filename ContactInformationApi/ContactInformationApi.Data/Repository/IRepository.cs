using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContactInformationApi.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task<TEntity> GetAsync(object id, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
