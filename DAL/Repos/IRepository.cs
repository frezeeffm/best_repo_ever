using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetChildren(int id);
        Task Create(T item);
        Task Update(T item);
        Task Delete(int id);
        Task<T> Find(int id);
    }
}