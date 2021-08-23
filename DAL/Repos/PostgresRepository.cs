using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DAL.Models;
using Dapper;

namespace DAL.Repos
{
    public class PostgresRepository : IRepository<Category>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public PostgresRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            using (IDbConnection db = await _connectionFactory.CreateConnectionAsync().ConfigureAwait(false))
            {
                return await db.QueryAsync<Category>("SELECT * FROM category").ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Category>> GetChildren(int id)
        {
            using (IDbConnection db = await _connectionFactory.CreateConnectionAsync().ConfigureAwait(false))
            {
                return await db.QueryAsync<Category>(@"SELECT * FROM category WHERE ParentId = @ParentId",
                    new {ParentId = id}).ConfigureAwait(false);
            }
        }

        public async Task Create(Category category)
        {
            using (IDbConnection db = await _connectionFactory.CreateConnectionAsync().ConfigureAwait(false))
            {
                await db.ExecuteAsync(@"INSERT INTO category (Name,ParentId) VALUES(@Name, @ParentId)", new { Name = category.Name, ParentId = category.ParentId ?? 0}).ConfigureAwait(false);
            }
        }

        public async Task Update(Category category)
        {
            using (IDbConnection db = await _connectionFactory.CreateConnectionAsync().ConfigureAwait(false))
            {
                await db.ExecuteAsync("UPDATE category SET Name = @Name, ParentId = @ParentId WHERE Id = @Id",
                    new { ParentId = category.ParentId ?? 0, Name = category.Name, Id = category.Id}).ConfigureAwait(false);
            }
        }

        public async Task Delete(int id)
        {
            using (IDbConnection db = await _connectionFactory.CreateConnectionAsync().ConfigureAwait(false))
            {
                await db.ExecuteAsync("DELETE FROM Category WHERE Id = @Id", new {Id = id}).ConfigureAwait(false);
            }
        }

        public async Task<Category> Find(int id)
        {
            using (IDbConnection db = await _connectionFactory.CreateConnectionAsync().ConfigureAwait(false))
            {
                return await db.QueryFirstOrDefaultAsync<Category>("SELECT * FROM Category WHERE Id = @Id", new { Id = id}).ConfigureAwait(false);
            }
        }
    }
}