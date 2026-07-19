using Microsoft.EntityFrameworkCore;
using NSE.Catalog.API.Models;
using NSE.Core.Data;

namespace NSE.Catalog.API.Data.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly CatalogContext _context;

        public IUnityOfWork unityOfWork => _context;

        public ProductRepository(CatalogContext catalogContext)
        {
            _context = catalogContext;
        }

        public async Task AddAsync(Product entity, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(entity, cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context
                .Products
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context
                .Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public Task UpdateAsync(Product entity, CancellationToken cancellationToken = default)
        {
            _context.Products.Update(entity);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
