using Domain.Contracts;
using Presistance.Data;

namespace Presistance.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false) => asNoTracking ? 
            await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync() 
            : await _dbContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> specifications)
        => await ApplySpecifications(specifications).ToListAsync();
        public Task<TEntity?> GetByIdAsync(Specifications<TEntity> specifications)
            => ApplySpecifications(specifications).FirstOrDefaultAsync();
        private IQueryable<TEntity> ApplySpecifications(Specifications<TEntity> specifications)
            => SpecificationEvalutor.GetQuery<TEntity>(_dbContext.Set<TEntity>(), specifications);
    }
}
