using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Models.DataLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AirBnbContext _context;
        private readonly DbSet<T> _db;

        public Repository(AirBnbContext context)
        {
            _context = context;
            _db = context.Set<T>();
        }

        public async Task<IEnumerable<T>> ListAsync(QueryOptions<T>? options = null)
        {
            IQueryable<T> query = _db;

            if (options != null)
                query = query.ApplyOptions(options);

            return await query.ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
            => await _db.FindAsync(id);

        public async Task<T?> GetAsync(QueryOptions<T> options)
            => await _db.ApplyOptions(options).FirstOrDefaultAsync();

        public async Task InsertAsync(T entity)
            => await _db.AddAsync(entity);

        public Task UpdateAsync(T entity)
        {
            _db.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _db.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
