using Dapper;
using Dapper.FluentMap.Mapping;

namespace DAL.Models.Mappings
{
    public class CategoryMapping : EntityMap<Category>
    {
        public CategoryMapping()
        {
            Map(p => p.Id).ToColumn("Id");
            Map(p => p.Name).ToColumn("Name");
            Map(p => p.ParentId).ToColumn("ParentId");
        }
    }
}