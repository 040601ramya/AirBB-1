using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirBB.Models.DataLayer.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> ListAsync(QueryOptions<T>? options = null);
        Task<T?> GetAsync(int id);
        Task<T?> GetAsync(QueryOptions<T> options);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();
    }
}
