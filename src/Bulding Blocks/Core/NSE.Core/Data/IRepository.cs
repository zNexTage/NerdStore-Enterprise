using NSE.Core.Domain;

namespace NSE.Core.Data
{


    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        public Task AddAsync(T entity, CancellationToken cancellationToken = default);
        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    }
}
