using System.Data;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
}