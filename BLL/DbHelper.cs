using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repos;
using Microsoft.Extensions.Configuration;

namespace Logic
{
    public class DbHelper
    {
        private readonly IRepository<Category> _repository;
        public DbHelper(IConfiguration configuration)
        {
            _repository = new PostgresRepository(new PostgresConnectionFactory(configuration.GetConnectionString("PostgresConnection")));
        }

        public async Task UpdateCategory(Category category)
        {
            await _repository.Update(category).ConfigureAwait(false);
        }

        public async Task Create(Category category)
        {
            await _repository.Create(category).ConfigureAwait(false);
        }

        public async Task<List<Node>> GetCategoryTree(int? parentid = null)
        {
            if (parentid.HasValue)
            {
                var cat = await _repository.Find(parentid.Value).ConfigureAwait(false);
                return (cat is null) ? 
                    null :
                    new List<Node>
                    {
                        new Node
                        {
                            Name = cat.Name,
                            Children = await GetChildren(cat.Id).ConfigureAwait(false),
                            Id = cat.Id
                        }
                    };
            }

            return await GetChildren(parentid);
        }

        private async Task<List<Node>> GetChildren(int? parentid)
        {
            var mainCategories = await _repository.GetChildren(parentid?? 0).ConfigureAwait(false);
            
            if (!mainCategories.Any())
                return null;
            
            return mainCategories
                .Select(async x => new Node
                {
                    Name = x.Name,
                    Id = x.Id,
                    Children = await GetChildren(x.Id).ConfigureAwait(false)
                })
                .Select(t => t.Result)
                .ToList();
        }
    }
}